using System;
using System.Configuration;

namespace BikeInsurance.DataExtract.Utils
{
    public class DateUtil
    {

        public static string TimeZoneId = ConfigurationManager.AppSettings["TimeZone"];
        public static TimeZoneInfo TimeZone => System.TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);


        /// <summary>
        /// With servers running UTC we need to find local date time.
        /// </summary>
        public static DateTime CurrentDateTime => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZone);
    }
}
