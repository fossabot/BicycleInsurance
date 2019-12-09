using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Raci.B2C.Bicycle.App_Start;
using Raci.B2C.Common.DynamicContent;
using Raci.B2C.Web;

namespace Raci.B2C.Bicycle
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            B2CPlatform.Init(UnityConfig.GetConfiguredContainer());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            ValidatorConfig.SetupValidators(ModelValidatorProviders.Providers);

            IContentProvider contentProvider = UnityConfig.GetConfiguredContainer().Resolve<IContentProvider>();

            contentProvider.RegisterBundle("~\\Views", "BicycleQuote", false);
            contentProvider.RegisterBundle("~\\Views\\Shared", "Common", false);
            contentProvider.RegisterBundle("~\\Views\\Question", "Question", false);
            contentProvider.RegisterBundle("~\\Views\\Quote", "Quote", false);
            contentProvider.RegisterBundle("~\\Views\\PolicyDetail", "PolicyDetail", false);
            contentProvider.RegisterBundle("~\\Views\\Payment", "PolicyPayment", false);
            contentProvider.RegisterBundle("~\\Views\\Confirmation", "Confirmation", false);
        }
    }
}
