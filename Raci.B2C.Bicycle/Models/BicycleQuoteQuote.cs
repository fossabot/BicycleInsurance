using System.Collections.Generic;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuoteQuote : ViewModelBase<BicycleQuote>
    {
        public List<BicycleQuoteQuoteOption> Options { get; set; }

        public BicycleQuoteQuote(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
            Options = new List<BicycleQuoteQuoteOption>();
        }
    }
}