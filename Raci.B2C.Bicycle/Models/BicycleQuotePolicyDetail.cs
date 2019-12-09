using System;
using System.ComponentModel.DataAnnotations;
using Raci.B2C.Bicycle.Utils;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuotePolicyDetail : ViewModelBase<BicycleQuote>
    {
        public BicycleQuotePolicyDetailContact Contact { get; set; }
        public BicycleQuotePolicyDetailBikeDetails BikeDetails { get; set; }

        public DateTime PolicyStartDate { get; set; }

        public BicycleQuotePolicyDetail(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
            Contact = new BicycleQuotePolicyDetailContact(viewModelRoot);
            BikeDetails = new BicycleQuotePolicyDetailBikeDetails(viewModelRoot);
            PolicyStartDate = DateUtil.CurrentDateTime;
        }
    }
}