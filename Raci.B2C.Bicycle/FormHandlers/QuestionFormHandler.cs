using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Bicycle.Utils;
using BicyclePolicyDetailDTO = Raci.B2C.Bicycle.ClientApi.Models.BicyclePolicyDetailDTO;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public class QuestionFormHandler : BaseFormHandler, IQuestionFormHandler
    {
        public QuestionFormHandler(IReferenceDataService referenceDataService, IPolicies policyApi, IBicyclePolicies bicyclePolicyApi) : base(referenceDataService, policyApi, bicyclePolicyApi)
        {
        }

        public async Task<PolicyDTO> CreatePolicy(BicycleQuoteQuestion bicycleQuoteQuestion)
        {
            BicyclePolicyDetailDTO details = MapToDto(bicycleQuoteQuestion);
            PolicyDTO policy = await BicyclePolicyApi.CreateWithHttpMessagesAsync(details, Jwt.CreateAuthorizationHeader(null)).Data();

            policy = await UpdateContact(policy, bicycleQuoteQuestion.CustomerDetails.EmailAddress);

            return policy;
        }

        public async Task<PolicyDTO> UpdatePolicy(long? policyId, BicycleQuoteQuestion bicycleQuoteQuestion)
        {
            BicyclePolicyDetailDTO details = MapToDto(bicycleQuoteQuestion);

            PolicyDTO policy = await BicyclePolicyApi.SetDetailsWithHttpMessagesAsync(policyId.GetValueOrDefault(), details, Jwt.CreateAuthorizationHeader(policyId)).Data();
            policy = await UpdateContact(policy, bicycleQuoteQuestion.CustomerDetails.EmailAddress);

            return policy;
        }

        protected BicyclePolicyDetailDTO MapToDto(BicycleQuoteQuestion bicycleQuoteQuestion)
        {
            BicyclePolicyDetailDTO detail = new BicyclePolicyDetailDTO
            {
                Make = bicycleQuoteQuestion.BikeDetails.Make,
                Model = bicycleQuoteQuestion.BikeDetails.Model,
                Type = bicycleQuoteQuestion.BikeDetails.Type,
                Year = bicycleQuoteQuestion.BikeDetails.Year
            };

            return detail;
        }
    }
}