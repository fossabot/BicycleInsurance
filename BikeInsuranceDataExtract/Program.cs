using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BikeInsurance.DataExtract.Utils;
using CsvHelper;
using Exceptions;
using Ionic.Zip;
using SendGrid;

namespace BikeInsurance.DataExtract
{
    internal class Program
    {
        private const string GetPoliciesQuery = "    SELECT PolicyNumber, FirstName, LastName, DateOfBirth, PhoneNumber, EmailAddress,                         " +
                                                "    AddressLine1, Suburb, PostCode, Code, AgreedValue, Make, Model, Year, Type, AnnualPremium, Excess,     " +
                                                "    TransactionId, DATEADD(hour, 8, [dbo].[PaymentDetails].[CreatedAt]) AS PolicyStartDate                 " +
                                                "    FROM[dbo].[Policies]                                                                                   " +
                                                "    LEFT JOIN[dbo].[PolicyContacts]                                                                        " +
                                                "    ON[dbo].[PolicyContacts].[Id] = [dbo].[Policies].[ContactId]                                           " +
                                                "    LEFT JOIN[dbo].[PolicyOptions]                                                                         " +
                                                "    ON[dbo].[PolicyOptions].[Id] = [dbo].[Policies].[OptionId]                                             " +
                                                "    LEFT JOIN[dbo].[BicyclePolicyDetails]                                                                  " +
                                                "    ON[dbo].[BicyclePolicyDetails].[Id] = [dbo].[Policies].[DetailId]                                      " +
                                                "    LEFT JOIN[dbo].[PaymentDetails]                                                                        " +
                                                "    ON[dbo].[PaymentDetails].[Id] = [dbo].[Policies].[PaymentId]                                           " +
                                                "    LEFT JOIN[dbo].[Addresses]                                                                             " +
                                                "    ON[dbo].[Addresses].[Id] = [dbo].[PolicyContacts].[AddressId]                                          " +
                                                "    WHERE[dbo].[Policies].[State] = 2                                                                      " +
                                                "    AND [dbo].[Policies].[Id] > 9                                                                          " +
                                                "    ORDER BY[dbo].[Policies].[Id]                                                                          " +
                                                "    ASC;                                                                                                   ";

        private const string GetQuotesQuery = @"SELECT [dbo].[Policies].[Id], DATEADD(hour, 8, [dbo].[Policies].[CreatedAt]) as CreatedAt, FirstName, LastName, DateOfBirth, PhoneNumber, EmailAddress, AddressLine1, Suburb, PostCode, Code, AgreedValue, Make, Model, Year, Type, AnnualPremium, Excess
                                                FROM [dbo].[Policies]
                                                LEFT JOIN [dbo].[PolicyContacts]
                                                ON [dbo].[PolicyContacts].[Id] = [dbo].[Policies].[ContactId]
                                                LEFT JOIN [dbo].[PolicyOptions]
                                                ON [dbo].[PolicyOptions].[Id] = [dbo].[Policies].[OptionId]
                                                LEFT JOIN [dbo].[BicyclePolicyDetails]
                                                ON [dbo].[BicyclePolicyDetails].[Id] = [dbo].[Policies].[DetailId]
                                                LEFT JOIN [dbo].[Addresses]
                                                ON [dbo].[Addresses].[Id] = [dbo].[PolicyContacts].[AddressId]
                                                WHERE [dbo].[Policies].[State] IN(0,1)
                                                ORDER BY [dbo].[Policies].[Id] ASC;";

        private static void Main()
        {
            Options options = Options.FromConfig();

            if (!options.Valid())
            {
                throw new ArgumentException("Some values not provided in App.settings.");
            }

            Program program = new Program();
            program.Run(options);
        }

        private void Run(Options options)
        {
            string policyCsv = CreateCsvFromDto(GetData(options, GetPoliciesQuery, "Policies"));
            string quoteCsv = CreateCsvFromDto(GetData(options, GetQuotesQuery, "Quotes"));

            byte[] attachment = CreateZipFileAttachment(options, policyCsv, quoteCsv);

            SendZipFileToRecipients(options, attachment);

            Console.WriteLine("Sent email(s) to " + options.ToEmailAddresses + ".");
        }

        private DataTable GetData(Options options, string query, string name)
        {
            SqlConnection sqlconnection = new SqlConnection(options.ConnectionString);

            SqlCommand sqlCommand = new SqlCommand(query, sqlconnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataSet dt = new DataSet();
            adapter.Fill(dt);

            var table = dt.Tables[0];

            Console.WriteLine("Loaded " + table.Rows.Count + " rows for " + name);

            return table;
        }

        private string CreateCsvFromDto(DataTable policies)
        {
            string[] columnNames = policies.Columns.Cast<DataColumn>().
                Select(column => column.ColumnName).
                ToArray();

            using (StringWriter stringWriter = new StringWriter())
            {
                using (var streamWriter = new CsvWriter(stringWriter))
                {
                    streamWriter.Configuration.HasHeaderRecord = true;
                    streamWriter.Configuration.Delimiter = ",";
                    streamWriter.Configuration.HasExcelSeparator = true;
                    streamWriter.Configuration.QuoteAllFields = true;

                    foreach (string column in columnNames)
                    {
                        streamWriter.WriteField(column);
                    }

                    streamWriter.NextRecord();

                    foreach (DataRow row in policies.Rows)
                    {
                        foreach (var field in row.ItemArray.Select(field => field.ToString()).
                            ToArray())
                        {
                            streamWriter.WriteField(field);
                        }

                        streamWriter.NextRecord();
                    }
                }

                return stringWriter.ToString();
            }

        }

        private byte[] CreateZipFileAttachment(Options options, string policies, string quotes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (ZipFile zipFile = new ZipFile { Password = options.ZipPassword })
                {
                    zipFile.AddEntry($"policies-{DateUtil.CurrentDateTime:yyyy-MM-dd_hh-mm-ss-tt}.csv", policies);
                    zipFile.AddEntry($"quotes-{DateUtil.CurrentDateTime:yyyy-MM-dd_hh-mm-ss-tt}.csv", quotes);
                    zipFile.Save(stream);
                }

                stream.Close();

                return stream.ToArray();
            }

        }

        private void SendZipFileToRecipients(Options options, byte[] policies)
        {
            using (MemoryStream ms = new MemoryStream(policies))
            {
                SendGridMessage message = new SendGridMessage();

                string subject = $"{options.EmailSubject} {DateUtil.CurrentDateTime}";

                message.To = options.ToEmailAddressesAsList().Select(email => new MailAddress(email)).ToArray();
                message.Subject = subject;
                message.From = new MailAddress(options.FromEmailAddress, "RAC Bicycle Insurance");
                message.ReplyTo = new MailAddress[] { message.From };
                message.AddAttachment(ms, "DataExtract.zip");
                message.Text = "Attached is the Bike Insurance data extract for " + DateUtil.CurrentDateTime.ToLongTimeString();

                Web transportWeb = new Web(options.SendGridApiKey);

                Task task = Task.Run(async () =>
                {
                    try
                    {
                        await transportWeb.DeliverAsync(message);
                    }
                    catch (InvalidApiRequestException e)
                    {
                        foreach (string error in e.Errors)
                        {
                            Console.WriteLine("Error: " + error);
                        }
                    }

                });

                task.Wait(TimeSpan.FromSeconds(30));
            }
        }
    }
}
