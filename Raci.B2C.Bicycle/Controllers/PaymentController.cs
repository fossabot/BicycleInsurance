using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Braintree;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.FormHandlers;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Common.DynamicContent;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Controllers;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Mvc;
using Braintree = Raci.B2C.Bicycle.Models.Braintree;

namespace Raci.B2C.Bicycle.Controllers
{
    public partial class PaymentController : PolicyController<BicycleQuote>
    {
        private readonly IPaymentFormHandler _paymentFormHandler;
        private readonly IPolicies _policyApi;

        public PaymentController(IPaymentFormHandler paymentFormHandler, IPolicies policyApi, IContentProvider contentProvider) : base(contentProvider)
        {
            _paymentFormHandler = paymentFormHandler;
            _policyApi = policyApi;
        }

        [NoCache]
        [HttpGet]
        [ValidatePolicy(RedirectIssuedPolicyToPayment = true)]
        public virtual async Task<ActionResult> Index()
        {
            try
            {
                BicycleQuote model = new BicycleQuote();

                ViewData.SetReferenceData(_paymentFormHandler.CreateReferenceData());
                await _paymentFormHandler.UpdateModelFromDto(this.GetPolicyId(), model);

                Models.Braintree braintreeModel = new  Models.Braintree
                {
                    ClientToken = _paymentFormHandler.GetBraintreeClientToken()
                };

                model.PolicyPayment.PaymentDetails.BrainTree = braintreeModel;

                return View(model);
            }
            catch (Exception x)
            {
                throw x;
            }
        }

        [NoCache]
        [HttpPost]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(true)]
        [AcceptParameter(Name = ViewModelBase.SUBMIT_ACTION, Value = MvcUtil.ACTION_SUBMIT)]
        public virtual async Task<ActionResult> Index(FormCollection formValues)
        {
            BicycleQuote model = new BicycleQuote();
            TryUpdateModel(model,formValues);

            long? policyId = this.GetPolicyId();

            Result<Transaction> transaction = await _paymentFormHandler.ProcessPayment(policyId, model.PolicyPayment.PaymentDetails.BrainTree.payment_method_nonce);

            if (transaction != null && transaction.IsSuccess())
            {
                model.PolicyPayment.PaymentDetails.BrainTree.PaymentFailed = false;
                await _paymentFormHandler.IssuePolicy(policyId, transaction);
            }
            else
            {
                model.PolicyPayment.PaymentDetails.BrainTree.PaymentFailed = true;
            }

            // validate model
            IEnumerable<ErrorInfo> errors = ViewModelValidator.Validate(model.PolicyPayment, "PolicyPayment");
            ModelStateUtil.AddModelStateErrors(ViewData.ModelState, errors, null, x => true, GetQuestionError);

            if (ModelState.IsValid) // continue if payment succeeded
            {
                return NavigateToAction(LocalMVC.Confirmation.Index());
            }
            else
            {
                await _paymentFormHandler.UpdateModelFromDto(this.GetPolicyId(), model);
                ViewData.SetReferenceData(_paymentFormHandler.CreateReferenceData());

                model.PolicyPayment.PaymentDetails.BrainTree.ClientToken = _paymentFormHandler.GetBraintreeClientToken();

                return ValidationError(model);
            }
        }

        [NoCache]
        [HttpPost]
        [ActionName("Index")]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AcceptParameter(Name = ViewModelBase.SUBMIT_ACTION, Value = MvcUtil.ACTION_BACK)]
        public virtual ActionResult Back(FormCollection formValues)
        {
            return NavigateToAction(LocalMVC.PolicyDetail.Index());
        }
    }
}