using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.FormHandlers;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Common.DynamicContent;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Controllers;
using Raci.B2C.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Raci.B2C.Bicycle.Controllers
{
    public partial class QuestionController : PolicyController<BicycleQuote>
    {
        private readonly IQuestionFormHandler _questionFormHandler;

        public QuestionController(IQuestionFormHandler questionFormHandler, IContentProvider contentProvider) : base(contentProvider)
        {
            _questionFormHandler = questionFormHandler;
        }

        [NoCache]
        [HttpGet]
        [ValidatePolicy(RedirectIssuedPolicyToPayment = true)]
        public virtual ActionResult Index()
        {
            BicycleQuote model = new BicycleQuote();
            //
            // TODO: Need to remove this for now but we can probably make this better
            //
            // SendTestEmail();
            this.SetPolicyId(null);
            ViewData.SetReferenceData(_questionFormHandler.CreateReferenceData());

            return View(model);
        }

        [NoCache]
        [HttpPost]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> Index(FormCollection formValues)
        {
            BicycleQuote quote = new BicycleQuote();
            TryUpdateModel(quote, formValues);

            IEnumerable<ErrorInfo> errors = ViewModelValidator.Validate(quote.Question, "Question");
            ModelStateUtil.AddModelStateErrors(ViewData.ModelState, errors, null, x => true, GetQuestionError);

            if (!ModelState.IsValid)
            {
                ViewData.SetReferenceData(_questionFormHandler.CreateReferenceData());

                return ValidationError(quote);
            }

            // do this after validation, no point creating a policy if the form is invalid.
            long? policyId = this.GetPolicyId(false);
            PolicyDTO policy;

            if (policyId == null)
            {
                policy = await _questionFormHandler.CreatePolicy(quote.Question);
                this.SetPolicyId(policy.Id);
            }
            else
            {
                policy = await _questionFormHandler.UpdatePolicy(policyId, quote.Question);
            }

            return NavigateToAction(LocalMVC.Quote.Index());
        }

        //private void SendTestEmail()
        //{
        //    IEmailService emailService = UnityConfig.GetConfiguredContainer().Resolve<IEmailService>();

        //    PolicyDTO policy = new PolicyDTO
        //    {
        //        Contact = new PolicyContactDTO
        //        {
        //            FirstName = "Paul",
        //            LastName = "O'Donnell",
        //            EmailAddress = "paul.odonnell@satalyst.com"
        //        }
        //    };

        //    emailService.SendQuoteSurvey(policy);
        //}
    }
}
