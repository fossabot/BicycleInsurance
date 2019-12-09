using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Mvc;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Bicycle.Utils;
using Raci.B2C.Model.PolicyModel;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Models.Common;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public abstract class BaseFormHandler : IBaseFormHandler
    {
        protected readonly IReferenceDataService ReferenceDataService;
        protected readonly IPolicies PolicyApi;
        protected readonly IBicyclePolicies BicyclePolicyApi;

        protected BaseFormHandler(IReferenceDataService referenceDataService, IPolicies policyApi, IBicyclePolicies bicyclePolicyApi)
        {
            ReferenceDataService = referenceDataService;
            PolicyApi = policyApi;
            BicyclePolicyApi = bicyclePolicyApi;
        }

        public BaseReferenceData CreateReferenceData()
        {
            BaseReferenceData referenceData = new BaseReferenceData();

            referenceData.BindOptions<BicycleQuote>(m => m.Question.BikeDetails.Type, ReferenceDataService.GetBicycleTypes());
            referenceData.BindOptions<BicycleQuote>(v => v.PolicyDetail.Contact.DateOfBirth, DateReferenceData.CreateRestrictedAgeDate());

            List<SelectListItem> paymentMethods = new List<SelectListItem>{new SelectListItem {Value = "1", Text = "Credit Card"}};
            List<SelectListItem> paymentFrequency = new List<SelectListItem>{new SelectListItem {Value = "1", Text = "Annual"}};

            referenceData.BindOptions<BicycleQuote>(p => p.PolicyPayment.PaymentDetails.PaymentMethod, paymentMethods);

            referenceData.BindOptions<BicycleQuote>(p => p.PolicyPayment.PaymentDetails.PaymentFrequency, paymentFrequency);

            return referenceData;
        }

        public async Task UpdateModelFromDto(long? policyId, BicycleQuote model)
        {
            PolicyDTO dto = await GetPolicy(policyId);
            UpdateModelFromDto(dto, model);
        }

        public void UpdateModelFromDto(PolicyDTO policy, BicycleQuote model)
        {
            model.PolicyNumber = policy.PolicyNumber;

            if (policy.Contact != null)
            {
                model.PolicyDetail.Contact.FirstName = policy.Contact.FirstName;
                model.PolicyDetail.Contact.LastName = policy.Contact.LastName;
                model.PolicyDetail.Contact.DateOfBirth = policy.Contact.DateOfBirth.HasValue ? new DateData(policy.Contact.DateOfBirth.Value) : null;
                model.PolicyDetail.Contact.ContactEmail = policy.Contact.EmailAddress;
                model.PolicyDetail.Contact.ContactNumber = policy.Contact.PhoneNumber;

                if (policy.Contact.Address != null)
                {
                    model.PolicyDetail.Contact.Address = policy.Contact.Address.AddressLine1;
                    model.PolicyDetail.Contact.Suburb = policy.Contact.Address.Suburb;
                    model.PolicyDetail.Contact.PostCode = policy.Contact.Address.PostCode;
                }
            }

            BicycleQuoteQuoteOption option = new BicycleQuoteQuoteOption
            {
                Description = "Bike Insurance",
                IsSelected = true,
                PolicyStartDate = DateUtil.CurrentDateTime.ToString("dd MMMM yyyy")
            };

            if (policy.Option != null)
            {
                //
                // values are mandatory in the database, therefore we can make the assumption that they are set.
                // they are also set as [Required] in the policy, so no idea why they are defined as nullable types.
                //
                option.AnnualPremium = (decimal)policy.Option.AnnualPremium.GetValueOrDefault();
                option.Excess = (decimal)policy.Option.Excess.GetValueOrDefault();
                option.Code = policy.Option.Code;
                option.SumInsured.Value = (int)Convert.ToDecimal(policy.Option.AgreedValue);


                model.PolicyPayment.PaymentDetails.PaymentMethod = 1;
                model.PolicyPayment.PaymentDetails.PaymentFrequency = 1;
                model.PolicyPayment.PaymentDetails.Excess = $"{policy.Option.Excess.GetValueOrDefault():C2}";
                model.PolicyPayment.PaymentDetails.Premium = $"{policy.Option.AnnualPremium.GetValueOrDefault():C2}";

                model.PolicyPayment.PaymentDetails.PolicyStartDate = DateUtil.CurrentDateTime.ToString("dd MMMM yyyy");
            }

            model.Quote.Options = new List<BicycleQuoteQuoteOption> { option };

            if (policy.Payment != null)
            {
                // TODO : it's been paid, do we need to do anything here?!?
            }
        }

        public async Task<PolicyDTO> GetPolicy(long? policyId)
        {
            PolicyDTO policy = await PolicyApi.GetPolicyWithHttpMessagesAsync(policyId.GetValueOrDefault(), Jwt.CreateAuthorizationHeader(policyId)).Data();
            return policy;
        }

        protected async Task<PolicyDTO> UpdateContact(PolicyDTO policy, string emailAddress)
        {
            if (policy.Contact == null)
            {
                policy.Contact = new PolicyContactDTO();
            }

            policy.Contact.EmailAddress = emailAddress;
            return await PolicyApi.SetContactWithHttpMessagesAsync(policy.Id, policy.Contact, Jwt.CreateAuthorizationHeader(policy.Id)).Data();
        }
    }
}