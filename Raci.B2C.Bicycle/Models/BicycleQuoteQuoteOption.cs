using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Models.Common;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuoteQuoteOption : ViewModelBase<BicycleQuote>
    {
        public bool IsSelected { get; set; }
        public string PolicyStartDate { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal AnnualPremium { get; set; }
        public decimal Excess { get; set; }

        public SumInsuredData SumInsured { get; set; }

        public string FormattedAnnualPremium => AnnualPremium.ToString("n");
        public string FormattedCurrencyAnnualPremium => AnnualPremium.ToString("c2");
        public string FormattedExcess => Excess.ToString("C0");

        /// <summary>
        /// This should only be used by the MVC framework
        /// </summary>
        public BicycleQuoteQuoteOption() : this(null)
        {
        }

        public BicycleQuoteQuoteOption(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
            SumInsured = new SumInsuredData();
        }
    }
}