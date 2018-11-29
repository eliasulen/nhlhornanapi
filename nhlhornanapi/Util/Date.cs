using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Util
{
    public static class DateUtil
    {
        public const int UtcOffsetStandard = -120;
 
        private static (DateTime StartDate, DateTime EndDate) ScheduleStandardInternal()
        {
            var dt = Now();
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

        public static DateTime Now() => DateTime.Now;


        public static (DateTime StartDate, DateTime EndDate) ScheduleStandard => ScheduleStandardInternal();

        public static (DateTime StartDate, DateTime EndDate) RecentStandard => (Now().Date.AddDays(-1), Now().Date.AddDays(-1));

    }
}
