using System.Threading.Tasks;
using Raci.B2C.Bicycle.ClientApi.Models;

namespace Raci.B2C.Bicycle.Service
{
    public interface IEmailService
    {
        Task SendPolicyConfirmation(PolicyDTO policy);
        bool SendPolicyDetails(PolicyDTO policy);

        Task<string> SendQuoteSurvey(PolicyDTO policy);
    }
}