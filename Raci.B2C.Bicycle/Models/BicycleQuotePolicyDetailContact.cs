using System;
using System.Collections.Generic;
using Raci.B2C.Bicycle.Utils;
using Raci.B2C.Web.Util;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.ComponentModel.DataAnnotations;
using Raci.B2C.Web.Models;
using Raci.B2C.Web.Models.Common;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuotePolicyDetailContact : ViewModelBase<BicycleQuote>
    {
        [B2CRequired]
        public string FirstName { get; set; }

        [B2CRequired]
        public string LastName { get; set; }

        [DateDataFormat(IsRequired = true)]
        public DateData DateOfBirth { get; set; }

        [B2CRequired]
        public string Address { get; set; }

        [B2CRequired]
        public string Suburb { get; set; }

        [B2CRequired]
        public string PostCode { get; set; }

        [B2CRequired]
        public string ContactNumber { get; set; }

        [B2CRequired]
        [EmailFormat]
        public string ContactEmail { get; set; }

        public BicycleQuotePolicyDetailContact(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
        }

        public override IEnumerable<ErrorInfo> PerformComplexValidation()
        {
            List<ErrorInfo> errorList= new List<ErrorInfo>();

            if( DateOfBirth.Year == null || 
                DateOfBirth.Month== null || 
                DateOfBirth.Day == null ||
                !DateOfBirth.ToDateTime().Between(DateUtil.CurrentDateTime.AddYears(-99), DateUtil.CurrentDateTime.AddYears(-18)))
            {
                errorList.Add(new ErrorInfo("DateOfBirth","Please enter a valid date of birth, You must be over the age of 18" ));
            }

            return errorList;
        }
    }
}