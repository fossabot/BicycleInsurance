using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Raci.B2C.Bicycle.ClientApi.Models;
using Raci.B2C.Bicycle.FormHandlers;
using Raci.B2C.Bicycle.Models;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Common.DynamicContent;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Controllers;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Mvc;
using Raci.B2C.Web.Util;

namespace Raci.B2C.Bicycle.Controllers
{
    public partial class QuoteController : PolicyController<BicycleQuote>
    {
        private readonly IQuoteFormHandler _quoteFormHandler;
        private readonly IReferenceDataService _referenceDataService;

        public QuoteController(IQuoteFormHandler quoteFormHandler, IContentProvider contentProvider, IReferenceDataService referenceDataService) : base(contentProvider)
        {
            _quoteFormHandler = quoteFormHandler;
            _referenceDataService = referenceDataService;
        }

        [NoCache]
        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            BicycleQuote model = new BicycleQuote();

            await _quoteFormHandler.UpdateModelFromDto(this.GetPolicyId(), model);
            SetReferenceData(model);

            return View(model);
        }

        private void SetReferenceData(BicycleQuote model)
        {
            ViewData.SetReferenceData(_quoteFormHandler.CreateReferenceData());
            MinMaxDTO minMax = _referenceDataService.GetSumInuredRange();

            if (minMax.MinValue != null) model.Quote.Options[0].SumInsured.MinValue = (int)minMax.MinValue;
            if (minMax.MaxValue != null) model.Quote.Options[0].SumInsured.MaxValue = (int)minMax.MaxValue;
        }

        [NoCache]
        [HttpPost]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> UpdateSelectedProduct(FormCollection formValues)
        {
            ViewModelPolicy<BicycleQuote> quote = new ViewModelPolicy<BicycleQuote>();
            TryUpdateModel(quote);

            ViewData.SetViewModelBase(quote.ViewModel);

            IList<BicycleQuoteQuoteOption> products = quote.ViewModel.Quote.Options;
            BicycleQuoteQuoteOption product = products.FirstOrDefault(x => x.IsSelected);

            ViewBag.Index = products.IndexOf(product);

            return PartialView("PremiumContainer", quote.ViewModel);
        }

        [NoCache]
        [HttpPost]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        public virtual async Task<ActionResult> UpdatePremium(FormCollection formValues)
        {
            BicycleQuote quote = new BicycleQuote();
            TryUpdateModel(quote);

            ViewData.SetViewModelBase(quote);

            PolicyDTO policy = await _quoteFormHandler.UpdateSumInsured(this.GetPolicyId(true), quote.Quote.Options[0].SumInsured.Value);

            ViewBag.Index = 0;

            _quoteFormHandler.UpdateModelFromDto(policy, quote);

            SetReferenceData(quote);

            return PartialView("PremiumContainer", quote);
        }

        [NoCache]
        [HttpPost]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AcceptParameter(Name = ViewModelBase.SUBMIT_ACTION, Value = MvcUtil.ACTION_SUBMIT)]
        public virtual ActionResult Index(FormCollection formValues)
        {
            BicycleQuote model = new BicycleQuote();
            TryUpdateModel(model);

            // TODO : This is the proper place for this call to be made!
            // _quoteFormHandler.UpdateCoverType(this.GetApiToken(), model.Quote.Options.FirstOrDefault(x => x.IsSelected));
            return NavigateToAction(LocalMVC.PolicyDetail.Index());
        }

        [NoCache]
        [HttpPost]
        [ActionName("Index")]
        [B2CValidateAntiForgeryToken]
        [ValidateInput(false)]
        [AcceptParameter(Name = ViewModelBase.SUBMIT_ACTION, Value = MvcUtil.ACTION_BACK)]
        public virtual ActionResult Back(FormCollection formValues)
        {
            return NavigateToAction(LocalMVC.Question.Index());
        }
    }
}