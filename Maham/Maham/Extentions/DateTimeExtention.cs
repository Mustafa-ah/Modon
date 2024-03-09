using System;
using System.Collections.Generic;
using System.Text;

namespace Maham.Extentions
{
    public static class DateTimeExtention
    {
        /// <summary>
        /// from date time without hours and minutes to string for UI Date 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShortDateStringForView(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string ToShortDateStringFromApIDateTime(this string date)
        {
            DateTime.TryParseExact(date, "yyyy/MM/dd h:mm tt", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime datetime);
            return datetime.ToShortDateStringForView();
        }

        public static string ToShortDateStringFromStringDateTime(this string date)
        {
            DateTime.TryParse(date, out DateTime datetime);
            return datetime.ToShortDateStringForView();
        }

        /// <summary>
        /// date time with hours and minutes to string for api
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToDateTimeStringForAPI(this DateTime date)
        {
            string converteddate;

            converteddate = date.ToString("yyyy-MM-dd h:mm tt", System.Globalization.CultureInfo.InvariantCulture);

            return converteddate;
        }

        /// <summary>
        /// used for comparing dates
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime ToDateTimeFromShortDate(this string date)
        {
            DateTime.TryParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime datetime);
            return datetime;// DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture); 
        }
    }
}
