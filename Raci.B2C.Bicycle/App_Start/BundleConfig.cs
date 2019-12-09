using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Raci.B2C.Bicycle.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/content/braintree-js")
            //    .Include("~/scripts/BrainTreePayment.js"));

            bundles.Add(new ScriptBundle("~/content/bicycle-js")
                .Include("~/scripts/QuotePageController.js"));

            bundles.Add(new StyleBundle("~/content/bicycle-css")
                .Include(
                    "~/content/site.css",
                    "~/content/bicycle-payment.css",
                    "~/content/bicycle-premium-container.css"
                ));
        }
    }
}