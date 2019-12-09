using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BikeInsurance.DataExtract
{
    public class Options
    {
        public string ToEmailAddresses { get; set; }
        public string FromEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string ConnectionString { get; set; }
        public string SendGridApiKey { get; set; }
        public string ZipPassword { get; set; }
        public string TimeZone { get; set; }

        public IList<string> ToEmailAddressesAsList()
        {
            if (string.IsNullOrEmpty(ToEmailAddresses))
            {
                return new List<string>();
            }

            return ToEmailAddresses.Split(',').ToList();
        }

        public bool Valid()
        {
            return
                !(
                        string.IsNullOrEmpty(ConnectionString) ||
                        string.IsNullOrEmpty(SendGridApiKey) ||
                        string.IsNullOrEmpty(ToEmailAddresses) ||
                        string.IsNullOrEmpty(FromEmailAddress) ||
                        string.IsNullOrEmpty(EmailSubject) ||
                        string.IsNullOrEmpty(ZipPassword) ||
                        string.IsNullOrEmpty(TimeZone)
                );
        }

        public static Options FromConfig()
        {
            Options options = new Options()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["BikeInsuranceReadonly"].ConnectionString,
                SendGridApiKey = ConfigurationManager.AppSettings["SendGrid.ApiKey"],

                ToEmailAddresses = ConfigurationManager.AppSettings["Raci.Admin.Email.To"],
                FromEmailAddress = ConfigurationManager.AppSettings["Raci.Admin.Email.From"],
                EmailSubject = ConfigurationManager.AppSettings["Raci.Admin.Email.Subject"],

                ZipPassword = ConfigurationManager.AppSettings["Raci.Admin.Zip.Password"],
                TimeZone = ConfigurationManager.AppSettings["TimeZone"]
            };

            return options;
        }
    }
}