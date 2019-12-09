using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public interface IPolicyDetailFormHandler : IBaseFormHandler
    {
        Task<PolicyDTO> UpdateContactDetails(long? policyId, BicycleQuote model);
    }
}
