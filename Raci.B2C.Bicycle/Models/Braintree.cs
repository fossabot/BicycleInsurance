using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raci.B2C.Web.ComponentModel.DataAnnotations;

namespace Raci.B2C.Bicycle.Models
{
    public class Braintree
    {

        public string ClientToken { get; set; }
        public string payment_method_nonce { get; set; }
        public bool PaymentFailed { get; set; }


    }
}