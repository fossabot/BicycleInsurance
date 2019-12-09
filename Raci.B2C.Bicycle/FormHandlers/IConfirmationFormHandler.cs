using System.Threading.Tasks;
using Raci.B2C.Bicycle.Models;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public interface IConfirmationFormHandler : IBaseFormHandler
    {
        Task<string> GetGoogleDataLayer(long? policyId);
    }
}
