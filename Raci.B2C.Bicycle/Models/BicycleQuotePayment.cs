using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuotePayment: ViewModelBase<BicycleQuote>
    {
        public BicycleQuotePaymentDetails PaymentDetails { get; set; }

        public BicycleQuotePayment(ModelRoot<BicycleQuote> viewModelRoot) 
            : base(viewModelRoot)
        {
            PaymentDetails = new BicycleQuotePaymentDetails(viewModelRoot);
        }
    }
}