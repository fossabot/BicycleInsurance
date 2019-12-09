using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Bicycle.Utils;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public class PolicyDetailFormHandler : BaseFormHandler, IPolicyDetailFormHandler
    {
        public PolicyDetailFormHandler(IReferenceDataService referenceDataService, IPolicies policyApi, IBicyclePolicies bicyclePolicyApi) : base(referenceDataService, policyApi, bicyclePolicyApi)
        {
        }

        public async Task<PolicyDTO> UpdateContactDetails(long? policyId, BicycleQuote model)
        {
            AddressDTO address = new AddressDTO()
            {
                AddressLine1 = model.PolicyDetail.Contact.Address,
                PostCode = model.PolicyDetail.Contact.PostCode,
                Suburb = model.PolicyDetail.Contact.Suburb
            };

            PolicyContactDTO dto = new PolicyContactDTO()
            {
                Address =  address,
                DateOfBirth = model.PolicyDetail.Contact.DateOfBirth.ToDateTime(),
                EmailAddress = model.PolicyDetail.Contact.ContactEmail,
                FirstName = model.PolicyDetail.Contact.FirstName,
                LastName = model.PolicyDetail.Contact.LastName,
                PhoneNumber = model.PolicyDetail.Contact.ContactNumber
            };

            return await PolicyApi.SetContactWithHttpMessagesAsync(policyId.GetValueOrDefault(), dto, Jwt.CreateAuthorizationHeader(policyId)).Data();
        }
    }
}