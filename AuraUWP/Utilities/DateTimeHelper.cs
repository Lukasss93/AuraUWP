using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuraUWP.Extensions;

namespace AuraUWP.Utilities
{
    public class DateTimeHelper
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string SecondsToMMSS(double seconds)
        {
            int min = Convert.ToInt32(seconds) / 60;
            int sec = Convert.ToInt32(seconds) % 60;

            return min.ToString().AddZero() + ":" + sec.ToString().AddZero();
        }

        /// <summary>Ottiene il timestamp unix</summary>
        public static long GetCurrentUnixTimestampSeconds()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
        }

        /// <summary>Converte un timestamp unix a DateTime</summary>
        public static DateTime TimestampToDateTime(long unixTimestamp)
        {
            return UnixEpoch.AddSeconds(unixTimestamp);
        }

        /// <summary>Converte un oggetto DateTime ad un unix timestamp</summary>
        public static double DateTimeToTimestamp(DateTime dateTime)
        {
            return (dateTime.ToUniversalTime() - UnixEpoch).TotalSeconds;
        }

        public static TimeSpan GetDifference(DateTime time, bool localTime=false)
        {
            DateTime current = DateTime.UtcNow;
            DateTime toSubstract = time;

            if(localTime==true)
            {
                current.ToLocalTime();
                toSubstract.ToLocalTime();
            }

            return current.Subtract(time);
        }        

    }
}
