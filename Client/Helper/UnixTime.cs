using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartRetail.Client.Helper
{
    public static class UnixTime
    {
        public static int GetCurrentSecond()
        {
            return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
        }

        public static long GetCurrentMilliSecond()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }

        public static string UnixSecondToLocalTimeFormat(long value)
        {
            var _value = (long)value ;
            if (_value <= 24 * 3600)
            {
                var hours = _value / 3600;
                var minute = (_value % 3600) / 60;
                var second = _value % 60;
                return hours.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }
            else
                return UnixSecondToLocalTime((long)_value).ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static string UnixMillisToLocalTimeFormat(long value)
        {
            var _value = (long)value / 1000;
            if (_value <= 24 * 3600)
            {
                var hours = _value / 3600;
                var minute = (_value % 3600) / 60;
                var second = _value % 60;
                return hours.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }
            else
                return UnixSecondToLocalTime((long)_value).ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static System.DateTime UnixSecondToLocalTime(long value)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(value).ToLocalTime();
            return dtDateTime;
        }

        public static System.DateTime UnixMillisToLocalTime(long value)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(value).ToLocalTime();
            return dtDateTime;
        }

        public static int LocalTimeToUnixSecond(DateTime value)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = (value.ToUniversalTime() - epoch);
            return (int)span.TotalSeconds;
        }
        public static int StartLocaltimeDay(int value)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(value).ToLocalTime();

            var startDayTime = new DateTime(dtDateTime.Year, dtDateTime.Month, dtDateTime.Day, 0, 0, 0);
            return LocalTimeToUnixSecond(startDayTime);
        }
    }
}
