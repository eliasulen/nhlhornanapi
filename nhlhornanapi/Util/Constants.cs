using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Util
{
    public class Constants
    {

        public class ApiLinks
        {
            public static string Base = "https://statsapi.web.nhl.com/api/v1/";
            public static string ScheduleLinescore = "schedule?expand=schedule.linescore&";
            public static string ScheduleLinescoreScoringplays = "schedule?expand=schedule.linescore&expand=schedule.scoringplays&";
            public static string Schedule = "schedule?";
        }

        public class RestLinks
        {
            public static string Base = "http://www.nhl.com/stats/rest/";
            public static string Player = "skaters?isAggregate=false&reportType=basic&isGame=false&reportName=skatersummary&sort=[{%22property%22:%22points%22,%22direction%22:%22DESC%22},{%22property%22:%22goals%22,%22direction%22:%22DESC%22},{%22property%22:%22assists%22,%22direction%22:%22DESC%22}]&cayenneExp=gameTypeId=2%20and%20seasonId%3E=20182019%20and%20seasonId%3C=20182019";
        }

        public class HockeyReferenceLinks
        {
            public static string Base = "https://www.hockey-reference.com/";
            public static string Salaries = "friv/current_nhl_salaries.cgi";
        }
    }
}
