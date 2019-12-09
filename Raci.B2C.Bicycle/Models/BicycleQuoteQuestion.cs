using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuoteQuestion : ViewModelBase<BicycleQuote>
    {
        public BicycleQuoteQuestionBikeDetails BikeDetails { get; set; }
        public BicycleQuoteQuestionCustomerDetails CustomerDetails { get; set; }

        public BicycleQuoteQuestion(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
            BikeDetails = new BicycleQuoteQuestionBikeDetails(viewModelRoot);
            CustomerDetails = new BicycleQuoteQuestionCustomerDetails(viewModelRoot);
        }
    }
}