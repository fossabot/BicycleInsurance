using System;
using System.Configuration;
using System.Threading.Tasks;
using Braintree;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Bicycle.Utils;
using Environment = Braintree.Environment;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public class PaymentFormHandler : BaseFormHandler, IPaymentFormHandler
    {
        private readonly BraintreeGateway _brainTreeGateway;
        private readonly IEmailService _emailService;

        private readonly string _publicKey = ConfigurationManager.AppSettings["BrainTree.PublicKey"];
        private readonly string _privateKey = ConfigurationManager.AppSettings["BrainTree.PrivateKey"];
        private readonly string _merchantId = ConfigurationManager.AppSettings["BrainTree.MerchantId"];

        public PaymentFormHandler(IReferenceDataService referenceDataService, IPolicies policyApi, IBicyclePolicies bicyclePolicyApi, IEmailService emailService) : base(referenceDataService, policyApi, bicyclePolicyApi)
        {
            _emailService = emailService;

            Environment environment = GetBraintreeMode(ConfigurationManager.AppSettings["Braintree.Environment"]);

            if (environment == null)
            {
                throw new ArgumentException("Cannot determine Braintree Environment from value: " + ConfigurationManager.AppSettings["Braintree.Environment"]);
            }

            _brainTreeGateway = new BraintreeGateway
            {
                Environment = environment,
                PublicKey = _publicKey,
                PrivateKey = _privateKey,
                MerchantId = _merchantId
            };
        }

        public string GetBraintreeClientToken()
        {
            return _brainTreeGateway.ClientToken.generate();
        }

        private Result<Address> CreateBrainTreeBillingAddress(string customerId, PolicyDTO policy)
        {

            return _brainTreeGateway.Address.Create(customerId, new AddressRequest
            {
                FirstName = policy.Contact.FirstName,
                LastName = policy.Contact.LastName,
                StreetAddress = policy.Contact.Address.AddressLine1,
                PostalCode = policy.Contact.Address.PostCode,
                Locality = policy.Contact.Address.Suburb,
                Region = policy.Contact.Address.Suburb
            });
        }

        private Result<Customer> CreateBraintreeCustomer(string paymentTypeNonce, PolicyDTO policy)
        {
            var results = _brainTreeGateway.Customer.Create(new CustomerRequest()
            {
                PaymentMethodNonce = paymentTypeNonce,
                Email = policy.Contact.EmailAddress,
                FirstName = policy.Contact.FirstName,
                LastName = policy.Contact.LastName,
                Phone = policy.Contact.PhoneNumber,
            });

            return results;
        }

        public async Task<Result<Transaction>> ProcessPayment(long? policyId, string clientNonce)
        {
            // todo: missing credit card verification here.
            PolicyDTO policy = await GetPolicy(policyId);

            var createCustomerResult = CreateBraintreeCustomer(clientNonce, policy);

            if (createCustomerResult.IsSuccess())
            {
                string customerId = createCustomerResult.Target.Id;

                CreateBrainTreeBillingAddress(customerId, policy);

                Result<Transaction> createPaymentResult = CreateBraintreeTransaction(policy, customerId);

                if (createPaymentResult.IsSuccess())
                {
                    return createPaymentResult;
                }
            }

            return null;
        }

        private Result<Transaction> CreateBraintreeTransaction(PolicyDTO policy, string customerId)
        {
            Result<Transaction> createPaymentResult;
            var paymentRequest = new TransactionRequest()
            {
                Amount = (decimal)policy.Option.AnnualPremium.Value,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                    AddBillingAddressToPaymentMethod = true,
                    //StoreInVault = false, // prevents storing of paymentmethod in the vault regardless of outcome.
                },
                CustomerId = customerId
            };

            createPaymentResult = _brainTreeGateway.Transaction.Sale(paymentRequest);
            return createPaymentResult;
        }

        public async Task IssuePolicy(long? policyId, Result<Transaction> transaction)
        {
            await PolicyApi.MarkAsPaidWithHttpMessagesAsync(transaction.Target.Id, Jwt.CreateAuthorizationHeader(policyId));

            PolicyDTO policy = await GetPolicy(policyId);

            if (policy.IsPolicy.GetValueOrDefault(false))
            {
                // TODO : this has been disabled as we are having issues with RAC GIT getting secure email set up. Jamo will
                // TODO : query the database directly to get the report extract for the underwriters.
                // _emailService.SendPolicyDetails(policy);

                //
                // Send the confirmation email to the customer
                //
                await _emailService.SendPolicyConfirmation(policy);
            }
        }

        private static Environment GetBraintreeMode(string config)
        {
            if (string.Equals(config, Environment.SANDBOX.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                return Environment.SANDBOX;
            }
            else if (string.Equals(config, Environment.DEVELOPMENT.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                return Environment.DEVELOPMENT;
            }
            else if (string.Equals(config, Environment.QA.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                return Environment.QA;
            }
            else if (string.Equals(config, Environment.PRODUCTION.EnvironmentName, StringComparison.OrdinalIgnoreCase))
            {
                return Environment.PRODUCTION;
            }

            return null;
        }



    }
}