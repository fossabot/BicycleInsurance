using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Raci.B2C.Common.MVC;
using Raci.B2C.Web.Models;

namespace Raci.B2C.Bicycle.Models
{
    public class BicycleQuotePaymentDetails: ViewModelBase<BicycleQuote>
    {
        public string PolicyStartDate { get; set; }
        public string Excess { get; set; }
        public Braintree BrainTree{ get; set; }

        [UIHint("Payment")]
        public string Payment { get; set; }
        public string payment_method_nonce { get; set; }
        public string Premium { get; set; }
        public int? PaymentFrequency { get; set; }
        public int? PaymentMethod { get; set; }

        public BicycleQuotePaymentDetails(ModelRoot<BicycleQuote> viewModelRoot) : base(viewModelRoot)
        {
        }

        public override IEnumerable<ErrorInfo> PerformComplexValidation()
        {

            List<ErrorInfo> errorList = new List<ErrorInfo>();

            if (BrainTree.PaymentFailed)
            {
                errorList.Add(new ErrorInfo("PaymentFailed", "")); // todo fix this to meet with B2C convention: this error is specified in payment.cshtml
            }

            return errorList;
        }
    }
}