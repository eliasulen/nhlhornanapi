using Newtonsoft.Json;
using nhlhornanapi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Model.Schedule
{
    public class Status
    {
        public string abstractGameState { get; set; }
        public string codedGameState { get; set; }
        public string detailedState { get; set; }
        public string statusCode { get; set; }
        public bool startTimeTBD { get; set; }
    }

    public class LeagueRecord
    {
        public int wins { get; set; }
        public int losses { get; set; }
        public int ot { get; set; }
        public string type { get; set; }
    }

    public class Team
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Away
    {
        public LeagueRecord leagueRecord { get; set; }
        public int score { get; set; }
        public Team team { get; set; }
    }

    public class LeagueRecord2
    {
        public int wins { get; set; }
        public int losses { get; set; }
        public int ot { get; set; }
        public string type { get; set; }
    }

    public class Team2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Home
    {
        public LeagueRecord2 leagueRecord { get; set; }
        public int score { get; set; }
        public Team2 team { get; set; }
    }

    public class Teams
    {
        public Away away { get; set; }
        public Home home { get; set; }
    }

    public class Away2
    {
        public int scores { get; set; }
        public int attempts { get; set; }
    }

    public class Home2
    {
        public int scores { get; set; }
        public int attempts { get; set; }
    }

    public class ShootoutInfo
    {
        public Away2 away { get; set; }
        public Home2 home { get; set; }
        public DateTime? startTime { get; set; }
    }

    public class Team3
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Home3
    {
        public Team3 team { get; set; }
        public int goals { get; set; }
        public int shotsOnGoal { get; set; }
        public bool goaliePulled { get; set; }
        public int numSkaters { get; set; }
        public bool powerPlay { get; set; }
    }

    public class Team4
    {
        public int id { get; set; }
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Away3
    {
        public Team4 team { get; set; }
        public int goals { get; set; }
        public int shotsOnGoal { get; set; }
        public bool goaliePulled { get; set; }
        public int numSkaters { get; set; }
        public bool powerPlay { get; set; }
    }

    public class Teams2
    {
        public Home3 home { get; set; }
        public Away3 away { get; set; }
    }

    public class IntermissionInfo
    {
        public int intermissionTimeRemaining { get; set; }
        public int intermissionTimeElapsed { get; set; }
        public bool inIntermission { get; set; }
    }

    public class PowerPlayInfo
    {
        public int situationTimeRemaining { get; set; }
        public int situationTimeElapsed { get; set; }
        public bool inSituation { get; set; }
    }

    public class Linescore
    {
        public int currentPeriod { get; set; }
        public string currentPeriodOrdinal { get; set; }
        public string currentPeriodTimeRemaining { get; set; }
        public List<object> periods { get; set; }
        public ShootoutInfo shootoutInfo { get; set; }
        public Teams2 teams { get; set; }
        public string powerPlayStrength { get; set; }
        public bool hasShootout { get; set; }
        public IntermissionInfo intermissionInfo { get; set; }
        public PowerPlayInfo powerPlayInfo { get; set; }
    }

    public class Venue
    {
        public string name { get; set; }
        public string link { get; set; }
    }

    public class Content
    {
        public string link { get; set; }
    }

    public class Game
    {
        public int gamePk { get; set; }
        public string link { get; set; }
        public string gameType { get; set; }
        public string season { get; set; }
        public DateTime gameDate { get; set; }
        public Status status { get; set; }
        public Teams teams { get; set; }
        public Linescore linescore { get; set; }
        public List<object> scoringPlays { get; set; }
        public Venue venue { get; set; }
        public Content content { get; set; }
    }

    public class Date
    {
        public string date { get; set; }
        public int totalItems { get; set; }
        public int totalEvents { get; set; }
        public int totalGames { get; set; }
        public int totalMatches { get; set; }
        public List<Game> games { get; set; }
        public List<object> events { get; set; }
        public List<object> matches { get; set; }
    }

    public class JsonSchedule
    {
        public string copyright { get; set; }
        public int totalItems { get; set; }
        public int totalEvents { get; set; }
        public int totalGames { get; set; }
        public int totalMatches { get; set; }
        public int wait { get; set; }
        public List<Date> dates { get; set; }

        public static JsonSchedule Get(string stringResult, DateTime startDate, DateTime endDate)
        {
            var rawSchedule = JsonConvert.DeserializeObject<JsonSchedule>(stringResult);

            if (Globals.UtcOffset != 0)
            {
                rawSchedule.dates.ForEach(d => d.games.ForEach(g => { g.gameDate = DateUtil.HandleOffset(g.gameDate); }));
                rawSchedule.dates.ForEach(d => d.games.RemoveAll(g => g.gameDate > endDate || g.gameDate < startDate));
                rawSchedule.dates.RemoveAll(d => !d.games.Any());
                rawSchedule.dates.ForEach(d =>
                {
                    d.totalGames = d.games.Count;
                    d.totalItems = d.games.Count;
                });

                rawSchedule.totalItems = rawSchedule.dates.SelectMany(d => d.games).Count();
                rawSchedule.totalGames = rawSchedule.dates.SelectMany(d => d.games).Count();
            }

            return rawSchedule;
        }
    }

}
