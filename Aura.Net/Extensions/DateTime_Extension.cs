using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aura.Net.Extensions
{
    public static class DateTime_Extension
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


    }
}
