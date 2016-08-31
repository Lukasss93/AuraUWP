using System;
using System.Globalization;

namespace Aura.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Restituisce il nome completo del mese in base alle impostazioni della cultura
        /// </summary>
        public static string GetMonthName(this DateTime dt)
        {
            int month = dt.Month;
            string monthname=DateTimeFormatInfo.CurrentInfo.GetMonthName(month).ToLower().ToUpperFirst();
            return monthname;
        }
        
        /// <summary>
        /// Restituisce il nome completo del giorno della settimana in base alle impostazioni della cultura
        /// </summary>
        public static string GetDayOfWeekName(this DateTime dt)
        {
            DayOfWeek dayofweek = dt.DayOfWeek;
            string dayofweekname=DateTimeFormatInfo.CurrentInfo.GetAbbreviatedDayName(dayofweek).ToLower().ToUpperFirst();
            return dayofweekname;
        }

        /// <summary>
        /// Restituisce le ultime 2 cifre dell'anno
        /// </summary>
        public static string y(this DateTime dt)
        {
            return dt.Year.ToString().Substring(2, 2);
        }

        /// <summary>Resets the time part to 00:00:00. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <returns>The new date time. </returns>
        public static DateTime ToStartOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
        }

        /// <summary>Sets the time part to the latest time of the day. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <returns>The new date time. </returns>
        public static DateTime ToEndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59, 999);
        }

        /// <summary>Resets the time part to 00:00:00. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <returns>The new date time. </returns>
        public static DateTime? ToStartOfDay(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToStartOfDay() : (DateTime?)null;
        }

        /// <summary>Sets the time part to the latest time of the day. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <returns>The new date time. </returns>
        public static DateTime? ToEndOfDay(this DateTime? dt)
        {
            return dt.HasValue ? dt.Value.ToEndOfDay() : (DateTime?)null;
        }

        /// <summary>Checks whether a date time is between two date times. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <param name="start">The starting date time. </param>
        /// <param name="end">The ending start time. </param>
        /// <returns>True when the date time is between. </returns>
        public static bool IsBetween(this DateTime dt, DateTime start, DateTime end)
        {
            return start <= dt && dt < end;
        }

        /// <summary>Checks whether a date time is between two date times. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <param name="start">The starting date time. </param>
        /// <param name="end">The ending start time. </param>
        /// <returns>True when the date time is between. </returns>
        public static bool IsBetween(this DateTime? dt, DateTime start, DateTime end)
        {
            return dt.HasValue && dt.Value.IsBetween(start, end);
        }

        /// <summary>Checks whether a date time is between two date times. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <param name="start">The starting date time. </param>
        /// <param name="end">The ending start time. Null means undefinitely in the future. </param>
        /// <returns>True when the date time is between. </returns>
        public static bool IsBetween(this DateTime dt, DateTime start, DateTime? end)
        {
            return start <= dt && (end == null || dt < end.Value);
        }

        /// <summary>Checks whether a date time is between two date times. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <param name="start">The starting date time. </param>
        /// <param name="end">The ending start time. Null means undefinitely in the future. </param>
        /// <returns>True when the date time is between. </returns>
        public static bool IsBetween(this DateTime? dt, DateTime start, DateTime? end)
        {
            return dt.HasValue && dt.Value.IsBetween(start, end);
        }

        /// <summary>
        /// Checks whether a date time is between two date times. 
        /// </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <param name="start">The starting date time. Null means undefinitely in the past. </param>
        /// <param name="end">The ending start time. Null means undefinitely in the future. </param>
        /// <returns>True when the date time is between. </returns>
        public static bool IsBetween(this DateTime dt, DateTime? start, DateTime? end)
        {
            return (start == null || start.Value <= dt) && (end == null || dt < end.Value);
        }

        /// <summary>Checks whether a date time is between two date times. </summary>
        /// <param name="dt">The date time to work with. </param>
        /// <param name="start">The starting date time. Null means undefinitely in the past. </param>
        /// <param name="end">The ending start time. Null means undefinitely in the future. </param>
        /// <returns>True when the date time is between. </returns>
        public static bool IsBetween(this DateTime? dt, DateTime? start, DateTime? end)
        {
            return dt.HasValue && dt.Value.IsBetween(start, end);
        }

        /// <summary>Changes only the time part of the DateTime. </summary>
        /// <param name="date">The date. </param>
        /// <param name="hour">The hour. </param>
        /// <param name="minute">The minute. </param>
        /// <param name="second">The second. </param>
        /// <returns></returns>
        public static DateTime SetTimeTakeDate(this DateTime date, int hour, int minute, int second)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second);
        }
    }
}
