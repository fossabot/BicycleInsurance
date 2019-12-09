using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi.Models;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public interface IQuoteFormHandler : IBaseFormHandler
    {
        Task<PolicyDTO> UpdateSumInsured(long? policyId, decimal sumInsured);
    }
}
