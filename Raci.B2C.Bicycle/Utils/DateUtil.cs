using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Raci.B2C.Bicycle.Utils
{

    public static class DateUtil
    {

        public static string TimeZoneId = ConfigurationManager.AppSettings["TimeZone"];
        public static TimeZoneInfo TimeZone => System.TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);


        /// <summary>
        /// With servers running UTC we need to find local date time.
        /// </summary>
        public static DateTime CurrentDateTime => TimeZoneInfo.ConvertTime(DateTime.Now, TimeZone);

        public static bool Between(this IComparable date, IComparable startDate, IComparable Enddate)

        {
            return date.CompareTo(startDate) >= 0 && date.CompareTo(Enddate) <= 0;
        }

    }
}