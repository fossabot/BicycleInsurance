using System.Web.Mvc;
using Raci.B2C.Common;

namespace Raci.B2C.Bicycle.Controllers
{
    public static class SessionExtensions
    {
        public static long? GetPolicyId(this Controller controller, bool required = true)
        {
            long? policyId = controller.Session["PolicyId"] as long?;

            if ((policyId == null) && (required))
            {
                throw new SessionTimeoutException();
            }

            return policyId;
        }

        public static void SetPolicyId(this Controller controller, long? policyId)
        {
            if (policyId == null)
            {
                controller.Session.Remove("PolicyId");
            }
            else
            {
                controller.Session["PolicyId"] = policyId.Value;
            }
        }
    }
}