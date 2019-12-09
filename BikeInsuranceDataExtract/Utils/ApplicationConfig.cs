using System.Configuration;

namespace BikeInsurance.DataExtract.Utils
{
    public class ApplicationConfig
    {
        public static string RaciAdminEmailsFrom
        {
            get
            {
                var email = ConfigurationManager.AppSettings["Raci.Admin.Email.From"];

                if (string.IsNullOrEmpty(email))
                {
                    throw new ConfigurationErrorsException("Must have a send from address for admin emails.");
                }

                return email;
            }
        }

        public static string[] RaciAdminEmailsTo
        {
            get
            {
                var emails = ConfigurationManager.AppSettings["Raci.Admin.Email.To"];
                if (string.IsNullOrEmpty(emails))
                {
                    throw new ConfigurationErrorsException("Please provide a valid raci admin email address");
                }

                return emails.Split(',');
            }
        }

        public static string ZipFilePassword
        {
            get
            {
                var password = ConfigurationManager.AppSettings["Raci.Admin.Zip.Password"];
                if (string.IsNullOrEmpty(password))
                {
                    throw new ConfigurationErrorsException("Please provide a valid raci admin email address");
                }

                return password;
            }
        }


        public static string SendGridApiKey 
        {
            get
            {
                var apiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"];
                if (string.IsNullOrEmpty(apiKey))
                {
                    throw new ConfigurationErrorsException("The SendGrid Api Key has not been set in the application config.");
                }

                return apiKey;
                
            }
        }

    }
}
