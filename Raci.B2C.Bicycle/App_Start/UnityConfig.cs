using System;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Raci.B2C.Bicycle.ClientApi;
using Raci.B2C.Bicycle.Service;
using Raci.B2C.Web.FormHandlers;

namespace Raci.B2C.Bicycle
{
    public class UnityConfig
    {
        private static readonly Lazy<IUnityContainer> _container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer GetConfiguredContainer()
        {
            return _container.Value;
        }

        private static void RegisterTypes(IUnityContainer container)
        {
            RegisterFormHandlers(container);

            Policies policies = new Policies(new RaciB2CBicycleService(new Uri(ConfigurationManager.AppSettings["BicycleInsurance.ApiUrl"])));
            container.RegisterInstance<IPolicies>(policies);

            BicyclePolicies bicyclePolicies = new BicyclePolicies(new RaciB2CBicycleService(new Uri(ConfigurationManager.AppSettings["BicycleInsurance.ApiUrl"])));
            container.RegisterInstance<IBicyclePolicies>(bicyclePolicies);

            CoverOptions covers = new CoverOptions(new RaciB2CBicycleService(new Uri(ConfigurationManager.AppSettings["BicycleInsurance.ApiUrl"])));
            container.RegisterInstance<ICoverOptions>(covers);

            ReferenceDatas referenceDatas = new ReferenceDatas(new RaciB2CBicycleService(new Uri(ConfigurationManager.AppSettings["BicycleInsurance.ApiUrl"])));
            container.RegisterInstance<IReferenceDatas>(referenceDatas);

            IEmailService emailService = new SendGridEmailService
            (
                ConfigurationManager.AppSettings["SendGrid.ApiKey"],
                ConfigurationManager.AppSettings["SendGrid.ConfirmationEmailTemplateId"],
                ConfigurationManager.AppSettings["SendGrid.QuoteSurveyEmailTemplateId"]
            );

            container.RegisterInstance<IEmailService>(emailService);

            container.RegisterType<IReferenceDataService, ReferenceDataService>(new ContainerControlledLifetimeManager());
            container.Configure<Interception>().SetDefaultInterceptorFor<ReferenceDataService>(new VirtualMethodInterceptor());
        }

        private static void RegisterFormHandlers(IUnityContainer container)
        {
            Type[] types = typeof(UnityConfig).Assembly
                .GetExportedTypes()
                .Where(t => typeof(IFormHandler).IsAssignableFrom(t))
                .Where(t => t.IsClass)
                .Where(t => !t.IsAbstract)
                .ToArray();

            container.RegisterTypes(
                types,
                WithMappings.FromMatchingInterface,
                WithName.Default,
                WithLifetime.ContainerControlled);
        }
    }
}
