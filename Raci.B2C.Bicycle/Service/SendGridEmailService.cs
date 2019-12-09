using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using CsvHelper;
using Newtonsoft.Json.Linq;
using Raci.B2C.Bicycle.ClientApi.Models;
using RestSharp;
using SendGrid;

namespace Raci.B2C.Bicycle.Service
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _sendGridApiKey;
        private readonly string _confirmationEmailTemplateId;
        private readonly string _surveyTemplateId;

        public SendGridEmailService(string apiKey, string confirmationEmailTemplateId, string surveyTemplateId)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(confirmationEmailTemplateId))
            {
                throw new ArgumentException();
            }

            if (string.IsNullOrEmpty(surveyTemplateId))
            {
                throw new ArgumentException();
            }

            _sendGridApiKey = apiKey;
            _confirmationEmailTemplateId = confirmationEmailTemplateId;
            _surveyTemplateId = surveyTemplateId;
        }

        private string[] AdminEmailsTo
        {
            get
            {
                string emails = ConfigurationManager.AppSettings["Raci.Admin.Email.To"];
                if (string.IsNullOrEmpty(emails))
                {
                    throw new ConfigurationErrorsException("Please provide a valid raci admin email address");
                }

                return emails.Split(',');
            }
        }

        private string AdminEmailsFrom
        {
            get
            {
                string result = ConfigurationManager.AppSettings["Raci.Admin.Email.From"];

                if (string.IsNullOrWhiteSpace(result))
                {
                    throw new ConfigurationErrorsException("Must have a send from address for admin emails.");
                }

                return result;
            }
        }

        private string ExternalEmailsFrom
        {
            get
            {
                string result = ConfigurationManager.AppSettings["Raci.External.Email.From"];

                if (string.IsNullOrWhiteSpace(result))
                {
                    throw new ConfigurationErrorsException("Must have a send from address for external emails.");
                }

                return result;
            }
        }
            
        public async Task SendPolicyConfirmation(PolicyDTO policy)
        {
            string fullName = $"{policy.Contact.FirstName} {policy.Contact.LastName}";

            SendGridMessage message = new SendGridMessage()
            {
                To = new MailAddress[] { new MailAddress(policy.Contact.EmailAddress, fullName), },
                Subject = "RAC Insurance Purchase Confirmation",
                From = new MailAddress(ExternalEmailsFrom, "RAC Bicycle Insurance"),
                ReplyTo = new MailAddress[] { },
                Html = $"Your policy number is <strong>{policy.PolicyNumber}.</strong>",
                Text = $"Your policy number is {policy.PolicyNumber}."
            };

            message.EnableTemplateEngine(_confirmationEmailTemplateId);
            message.AddSubstitution("%%name%%", new List<string> { fullName } );

            SendGrid.Web transportWeb = new SendGrid.Web(_sendGridApiKey);
            await transportWeb.DeliverAsync(message);
        }

        public bool SendPolicyDetails(PolicyDTO policy)
        {
            SendGridMessage message = new SendGridMessage();

            var subject = "RAC Bicycle Insurance - " + policy.PolicyNumber;
             var body  = CreateCsvFromDto(policy); 

            message.To = AdminEmailsTo.Select(email => new MailAddress(email)).ToArray();
            message.Subject = "RAC Bicycle Insurance - " + policy.PolicyNumber;
            message.From = new MailAddress(AdminEmailsFrom, "RAC Bicycle Insurance");
            message.ReplyTo = new MailAddress[] { message.From };
            message.Text = body;

            // Todo complete the zip file implementation
            // ZipFile zipFile = new ZipFile(); 
            // zipFile.Password = "Some Password";
            // zipFile.AddEntry();


            message.AddAttachment(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(body)), subject + ".csv");

            var transportWeb = new SendGrid.Web(_sendGridApiKey);

            var task = Task.Run(async () =>
            {
                await transportWeb.DeliverAsync(message);
            });

            task.Wait(TimeSpan.FromSeconds(30));


            return true;
        }

        public async Task<string> SendQuoteSurvey(PolicyDTO policy)
        {
            string batchId = await GetBatchId();

            string fullName = $"{policy.Contact.FirstName} {policy.Contact.LastName}";

            SendGridMessage message = new SendGridMessage()
            {
                To = new MailAddress[] { new MailAddress(policy.Contact.EmailAddress, fullName), },
                Subject = "RAC Bicycle Insurance Survey",
                From = new MailAddress(ExternalEmailsFrom, "RAC Bicycle Insurance"),
                Html = $"Hi",
                Text = $"Hi"
            };

            message.Headers["batch_id"] = batchId;
            message.SetSendAt( DateTime.UtcNow.AddMinutes(10));
            message.EnableTemplateEngine(_surveyTemplateId);
            message.AddSubstitution("%%name%%", new List<string> { fullName });

            SendGrid.Web transportWeb = new SendGrid.Web(_sendGridApiKey);

            await transportWeb.DeliverAsync(message);

            return "Hello";
        }

        private static string CreateCsvFromDto(PolicyDTO policy)
        {
            var body = string.Empty;

            using (StringWriter stringWriter = new StringWriter())
            {
                using (var streamWriter = new CsvWriter(stringWriter))
                {
                    streamWriter.Configuration.HasHeaderRecord = true;
                    streamWriter.Configuration.Delimiter = ",";
                    streamWriter.Configuration.HasExcelSeparator = true;
                    streamWriter.Configuration.QuoteAllFields = true;

                    streamWriter.WriteHeader(typeof(PolicyDTO));
                    //streamWriter.NextRecord();
                    streamWriter.WriteRecord(policy);
                }
                
                body = stringWriter.ToString();
            }

            return body;
        }

        private async Task<string> GetBatchId()
        {
            Client client = new Client(_sendGridApiKey);

            HttpResponseMessage response = await client.Post("/v3/mail/batch", new JObject());

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unable to get batchid for sending email");
            }

            string rawString = response.Content.ReadAsStringAsync().Result;
            dynamic jsonObject = JObject.Parse(rawString);
            string batchId = jsonObject.batch_id;

            return batchId;
        }
    }
}