using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Util
{
    public static class DateUtil
    {
        public const int UtcOffsetStandard = -120;
 
        public static (DateTime StartDate, DateTime EndDate) ScheduleListStandard()
        {
            var dt = DateTime.Now;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            startDate = dt.AddDays(-diff).Date;

            endDate = startDate.AddDays(6);

            return (startDate, endDate);
        }

        public static DateTime HandleOffset(DateTime date)
        {
            var c = Globals.UtcOffset * -1;

            return date.AddMinutes(c);
        }

    }
}
