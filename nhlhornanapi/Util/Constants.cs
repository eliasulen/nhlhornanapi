using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Util
{
    public class Constants
    {

        public class Links
        {
            public static string Base = "https://statsapi.web.nhl.com/api/v1/";
            public static string ScheduleLinescore = "schedule?expand=schedule.linescore&";
            public static string ScheduleLinescoreScoringplays = "schedule?expand=schedule.linescore&expand=schedule.scoringplays&";
            public static string Schedule = "schedule?";
        }
    }
}
