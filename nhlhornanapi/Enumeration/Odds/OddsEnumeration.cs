using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Enumeration.Odds
{
    public static class OddsEnumeration
    {

        public enum MatchType
        {
            Unknown = 0,
            Regular = 1,
            PlayOffs = 2,
            PreSeason = 3,
            GroupStage = 4,
            Qualification = 5,
            Relegation = 6,
            AllStar = 7
        }

        public enum MatchOutcome
        {
            Home = 0,
            Draw = 1,
            Away = 2,
        }

        public enum ParticipantOutcome
        {
            WinFulltime,
            LoseFulltime,
            WinOvertime,
            LoseOvertime,
            WinPenalties,
            LosePenalties,
            Draw
        }


        public enum MatchStatus
        {
            Unhandled,
            Valid,
            Live,
            Exception
        }

        public enum WinningMethod
        {
            Unknown,
            Fulltime,
            Overtime,
            Penalties,
            None
        }

    }
}
