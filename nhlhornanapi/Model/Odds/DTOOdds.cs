using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Model.Odds
{
    public class DTOOdds
    {
        public string Key { get; set; }
        public DateTime Date { get; set; }
        public Home Home { get; set; }
        public Away Away { get; set; }
        public string Outcome { get; set; }
        public string MatchType { get; set; }
        public Odds Odds { get; set; }
        public string WinningMethod { get; set; }
        public string FullTime { get; set; }
        public string Total { get; set; }
        public string Season { get; set; }
   
    }

    public class Home
    {
        public string Team { get; set; }
        public int? Points { get; set; }
        public int Goals { get; set; }
        public int? OvertimeGoals { get; set; }
        public string Result { get; set; }
    }

    public class Away
    {
        public string Team { get; set; }
        public int? Points { get; set; }
        public int Goals { get; set; }
        public int? OvertimeGoals { get; set; }
        public string Result { get; set; }
    }

    public class Odds
    {
        public double Home { get; set; }
        public double Draw { get; set; }
        public double Away { get; set; }
    }
}
