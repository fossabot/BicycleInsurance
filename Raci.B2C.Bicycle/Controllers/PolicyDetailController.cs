using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.FormHandlers;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Common.DynamicContent;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Controllers;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Mvc;

namespace Raci.B2C.Bicycle.Controllers
{
    public partial class PolicyDetailController : PolicyController<BicycleQuote>
    {
        private readonly IPolicyDetailFormHandler _policyDetailFormHandler;

        public PolicyDetailController(IPolicyDetailFormHandler policyDetailFormHandler, IContentProvider contentProvider) : base(contentProvider)
        {
            _policyDetailFormHandler = policyDetailFormHandler;
        }

        [NoCache]
        [HttpGet]
        [ValidatePolicy(RedirectIssuedPolicyToPayment = true)]
        public virtual async Task<ActionResult> Index()
        {
            BicycleQuote model = new BicycleQuote();
            PolicyDTO policy = await _policyDetailFormHandler.GetPolicy(this.GetPolicyId());

            _policyDetailFormHandler.UpdateModelFromDto(policy, model);
            SetReferenceData(policy, model);

            return View(model);
        }

        [NoCache]
        [HttpPost]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AcceptParameter(Name = ViewModelBase.SUBMIT_ACTION, Value = MvcUtil.ACTION_SUBMIT)]
        public virtual async Task<ActionResult> Index(FormCollection formValues)
        {
            BicycleQuote quote = new BicycleQuote();

            TryUpdateModel(quote, formValues);

            IEnumerable<ErrorInfo> errors = ViewModelValidator.Validate(quote.PolicyDetail, "PolicyDetail");
            ModelStateUtil.AddModelStateErrors(ViewData.ModelState, errors, null, x => true, GetQuestionError);

            if (!ModelState.IsValid)
            {
                PolicyDTO policy = await _policyDetailFormHandler.GetPolicy(this.GetPolicyId());
                SetReferenceData(policy, quote);

                return ValidationError(quote);
            }

            await _policyDetailFormHandler.UpdateContactDetails(this.GetPolicyId(), quote);
            return NavigateToAction(LocalMVC.Payment.Index());
        }

        [NoCache]
        [HttpPost]
        [ActionName("Index")]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AcceptParameter(Name = ViewModelBase.SUBMIT_ACTION, Value = MvcUtil.ACTION_BACK)]
        public virtual ActionResult Back(FormCollection formValues)
        {
            return NavigateToAction(LocalMVC.Quote.Index());
        }

        private void SetReferenceData(PolicyDTO policy, BicycleQuote model)
        {
            ViewData.SetReferenceData(_policyDetailFormHandler.CreateReferenceData());

            model.PolicyDetail.BikeDetails.Value = $"{Convert.ToDecimal(policy?.Option?.AgreedValue):C0}";
            model.PolicyDetail.BikeDetails.Make = policy?.Detail?.Make;
            model.PolicyDetail.BikeDetails.Model = policy?.Detail?.Model;
            model.PolicyDetail.BikeDetails.Usage = policy?.Detail?.Type;
            model.PolicyDetail.BikeDetails.Year = policy?.Detail?.Year;
        }
    }
}