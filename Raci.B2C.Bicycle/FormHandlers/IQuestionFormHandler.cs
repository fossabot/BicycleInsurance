using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.Models;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public interface IQuestionFormHandler : IBaseFormHandler
    {
        Task<PolicyDTO> CreatePolicy(BicycleQuoteQuestion bicycleQuoteQuestion);
        Task<PolicyDTO> UpdatePolicy(long? policyId, BicycleQuoteQuestion bicycleQuoteQuestion);
    }
}
