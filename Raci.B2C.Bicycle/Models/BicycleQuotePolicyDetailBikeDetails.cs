using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuotePolicyDetailBikeDetails : ViewModelBase<BicycleQuote>
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Usage { get; set; }
        public string Value { get; set; }

        public BicycleQuotePolicyDetailBikeDetails(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
            Make = "Make";
            Model = "Model";
            Year = "Year";
            Usage = "Usage";
            Value = "Value";
        }
    }
}