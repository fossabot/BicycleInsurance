using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuoteConfirmation: ViewModelBase<BicycleQuote>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string PolicyStartDate { get; set; }
        public string Excess { get; set; }
        public string Premium { get; set; }

        public BicycleQuoteConfirmation(ModelRoot<BicycleQuote> viewModelRoot) 
            : base(viewModelRoot)
        {
        }
    }
}