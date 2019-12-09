using System.ComponentModel.DataAnnotations;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.ComponentModel.DataAnnotations;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Mvc;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuoteQuestionBikeDetails : ViewModelBase<BicycleQuote>
    {
        [B2CRequired]
        public string Make { get; set; }

        [B2CRequired]
        public string Model { get; set; }

        [B2CRequired]
        public string Year { get; set; }

        [B2CRequiredSelect]
        [UIHint(TemplateNames.SelectList)]
        public string Type { get; set; }

        public BicycleQuoteQuestionBikeDetails(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
        }
    }
}