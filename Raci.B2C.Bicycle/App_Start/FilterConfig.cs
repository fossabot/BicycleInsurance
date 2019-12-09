using System.Web.Mvc;
using Raci.B2C.Web.Mvc.Filters;

namespace Raci.B2C.Bicycle.App_Start
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //TODO: investigate the commented out attributes and see if they represent anything valuable to the bike project
            filters.Add(new HandleErrorAttribute(), 1);
            //            filters.Add(new SecureAttribute(), 2);
            filters.Add(new ViewModelRootAttribute(), 3);

            //            filters.Add(new SessionManagerActionFilterAttribute());
            filters.Add(new ViewModelValidatorActionFilterAttribute());
            filters.Add(new AccordionActionFilterAttribute());
            //            filters.Add(new StandardServicesActionFilterAttribute());
        }
    }
}