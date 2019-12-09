using Raci.B2C.Common.MVC;
using Raci.B2C.Web.ComponentModel.DataAnnotations;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuoteQuestionCustomerDetails : ViewModelBase<BicycleQuote>
    {
        [B2CRequired]
        [EmailFormat(FailOnNullOrEmpty = false)]
        public string EmailAddress { get; set; }

        public BicycleQuoteQuestionCustomerDetails(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
        }
    }
}