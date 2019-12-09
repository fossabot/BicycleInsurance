using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsQuery.Utility;
using Newtonsoft.Json;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Service;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public class ConfirmationFormHandler : BaseFormHandler, IConfirmationFormHandler
    {
        public ConfirmationFormHandler(IReferenceDataService referenceDataService, IPolicies policyApi, IBicyclePolicies bicyclePolicyApi) : base(referenceDataService, policyApi, bicyclePolicyApi)
        {
        }

        public async Task<string> GetGoogleDataLayer(long? policyId)
        {
            PolicyDTO policy = await GetPolicy(policyId);

            try
            {
                Dictionary<string, string> transactionProducts = new Dictionary<string, string>();
                transactionProducts.Add("sku", "bicycleinsurance");
                transactionProducts.Add("name", "bicycle-insurance");
                transactionProducts.Add("category", "insurance");
                transactionProducts.Add("price", policy.Option.AnnualPremium.ToString());
                transactionProducts.Add("quantity", "1");

                double? gst = policy.Option.AnnualPremium - (policy.Option.AnnualPremium / 1.1);

                object DataLayerObject = new
                {
                    transactionId = policy.Payment.TransactionId,
                    transactionAffiliation = "RAC Insurance",
                    transactionTotal = policy.Option.AnnualPremium.ToString(),
                    transactionTax = gst.ToString(),
                    transactionShipping = 0,
                    transactionProducts,
                    @event = "bike.insurance.purchase"
                };
                return JsonConvert.SerializeObject(DataLayerObject);

            }
            catch (Exception)
            {
                //todo google analytics is busted do something here.
                return null;
            }
        }
    }
}