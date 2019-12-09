using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Web.FormHandlers;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public interface IBaseFormHandler : IFormHandler
    {
        BaseReferenceData CreateReferenceData();
        Task<PolicyDTO> GetPolicy(long? policyId);
        Task UpdateModelFromDto(long? policyId, BicycleQuote model);
        void UpdateModelFromDto(PolicyDTO policy, BicycleQuote model);
    }
}