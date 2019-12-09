using System;
using System.Collections.Generic;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Areas.Quote.Models;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Models.Common;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuote : PolicyViewModel<BicycleQuote>, IQuotePolicy
    {
        public BicycleQuoteQuestion Question { get; set; }
        public BicycleQuoteQuote Quote { get; set; }
        public BicycleQuotePolicyDetail PolicyDetail { get; set; }
        public BicycleQuotePayment PolicyPayment{ get; set; }
        public BicycleQuoteConfirmation Confirmation { get; set; }

        public BicycleQuote()
        {
            ViewModelRoot = new ModelRoot<BicycleQuote>(this);
            Question = new BicycleQuoteQuestion(ViewModelRoot);
            Quote = new BicycleQuoteQuote(ViewModelRoot);
            PolicyDetail = new BicycleQuotePolicyDetail(ViewModelRoot);
            PolicyPayment = new BicycleQuotePayment(ViewModelRoot);
            Confirmation = new BicycleQuoteConfirmation(ViewModelRoot);

            TermsAndConditions.ViewModelRoot = ViewModelRoot;
        }

        public static PageHeadingModel HeadingModel => new PageHeadingModel()
        {
            PageTitle = "Common.pageTitle",
            PageTitleWithReferenceNumberDesktop = "Common.pageTitleWithQuoteNumberDesktop",
            PageTitleWithReferenceNumberMobile = "Common.pageTitleWithQuoteNumberMobile",
            StepIndicator = StepIndicatorModel.CreateQuoteSteps()
        };

        public override IList<IPolicyContact> GetContacts()
        {
            throw new NotImplementedException();
        }

        public QuotePolicy Policy { get; }

        public string GoogleDataLayer { get; set; }

    }
}