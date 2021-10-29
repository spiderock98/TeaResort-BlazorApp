using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;


namespace SmartRetail.Client.Helper
{
    public static class DateTimeHelper
    {
        public static string DateTime2DateTimeFormat(DateTime t, string format, bool includeHourMinSec)
        {
            // dd/M/yyyy exp.
            DateTime d = new DateTime(t.Year, t.Month, t.Day);
            if (includeHourMinSec)
            {
                return d.ToString(format, CultureInfo.InvariantCulture) + " " + t.Hour.ToString() + ":" + t.Minute.ToString() + ":" + t.Second.ToString();
            }
            else
            {
                return d.ToString(format, CultureInfo.InvariantCulture);
            }
        }
        public static string String2DateTimeFormat(string t, string format)
        {
            int year, month, day;
            // dd/M/yyyy exp.
            try
            {
                year = int.Parse(t.Substring(0, 4));
                month = int.Parse(t.Substring(4, 2));
                day = int.Parse(t.Substring(6, 2));
            }
            catch (Exception exp) { return null; }
            DateTime d = new DateTime(year, month, day);
            return d.ToString(format, CultureInfo.InvariantCulture);
        }
        public static DateTime String2DateTime(string t)
        {
            var year = int.Parse(t.Substring(0, 4));
            var month = int.Parse(t.Substring(4, 2));
            var day = int.Parse(t.Substring(6, 2));
            DateTime d = new DateTime(year, month, day);
            return d;
        }
        public static string DateTime2String(DateTime t)
        {
            return t.Year.ToString() + (t.Month < 10 ? "0" + t.Month.ToString() : t.Month.ToString()) + (t.Day < 10 ? "0" + t.Day.ToString() : t.Day.ToString());
        }

        public static List<string> DateTime2StringRange(DateTime tStart, DateTime tEnd)
        {
            List<string> returnData = new List<string>();
            var idxT = tStart;
            while (tEnd >= idxT)
            {
                returnData.Add(DateTime2String(idxT));
                idxT = idxT.AddDays(1);
            }
            return returnData;
        }
        
        public static List<DateTime> DateTime2DateTimeRange(DateTime tStart, DateTime tEnd)
        {
            List<DateTime> returnData = new List<DateTime>();
            var idxT = tStart;
            while (tEnd >= idxT)
            {
                returnData.Add(idxT);
                idxT = idxT.AddDays(1);
            }
            return returnData;
        }

        public static string HistoryLog2String(string t)
        {
            var y = t.Substring(0, 2);
            var M = t.Substring(2, 2);
            var d = t.Substring(4, 2);
            var H = t.Substring(6, 2);
            var m = t.Substring(8, 2);
            var s = t.Substring(10, 2);

            return d + "/" + M + "/" + y + " " + H + ":" + m + ":" + s;
        }
    }
}
