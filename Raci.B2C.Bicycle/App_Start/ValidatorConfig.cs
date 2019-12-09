using System.Collections.Generic;
using System.Web.Mvc;

// ReSharper disable CheckNamespace
namespace Raci.B2C.Web
// ReSharper restore CheckNamespace
{
    public static class ValidatorConfig
    {
        public static void SetupValidators(ModelValidatorProviderCollection providerCollection)
        {
            var providers = new List<ModelValidatorProvider>(providerCollection);

            foreach (ModelValidatorProvider currentItem in providers)
            {
                if (currentItem is DataAnnotationsModelValidatorProvider)
                {
                    providerCollection.Remove(currentItem);
                }
            }
        }
    }
}