using System.Globalization;
using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Bicycle.Utils;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public class QuoteFormHandler : BaseFormHandler, IQuoteFormHandler
    {
        public QuoteFormHandler(IReferenceDataService referenceDataService, IPolicies policyApi, IBicyclePolicies bicyclePolicyApi) : base(referenceDataService, policyApi, bicyclePolicyApi)
        {
        }

        public async Task<PolicyDTO> UpdateSumInsured(long? policyId, decimal sumInsured)
        {
            PolicyDTO policy = await GetPolicy(policyId);

            policy.Option.AgreedValue = sumInsured.ToString(CultureInfo.InvariantCulture);

            policy = await PolicyApi.SetPolicyOptionWithHttpMessagesAsync(policy.Id, policy.Option, Jwt.CreateAuthorizationHeader(policy.Id)).Data();

            return policy;
        }
    }
}