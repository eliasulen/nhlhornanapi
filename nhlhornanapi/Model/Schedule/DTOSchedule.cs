using nhlhornanapi.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nhlhornanapi.Model.Schedule
{

    public class DTOSchedule
    {
        public class Opponent
        {
            public string LogoLink { get; set; }
            public bool Away { get; set; }
            public DateTime GameDate { get; set; }
            public string Status { get; set; }
        }

        public int Id { get; set; }
        public string Team { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OtLosses { get; set; }
        public int Points { get; set; }
        public string LogoLink { get; set; }
        public int AmountOfGames { get; set; }

        public IEnumerable<Opponent> Opponents { get; set; }
    }
}