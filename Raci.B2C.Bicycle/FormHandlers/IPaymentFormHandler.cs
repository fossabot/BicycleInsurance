using System.Threading.Tasks;
using Braintree;
using Raci.B2C.Bicycle.Models;

namespace Raci.B2C.Bicycle.FormHandlers
{
    public interface IPaymentFormHandler : IBaseFormHandler
    {
        string GetBraintreeClientToken();
        Task<Result<Transaction>> ProcessPayment(long? policyId, string clientNonce);

        Task IssuePolicy(long? policyId, Result<Transaction> transaction);
    }
}
