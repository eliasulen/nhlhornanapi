using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Model.Recent
{
    public class DTORecent
    {
        public DateTime Date { get; set; }
        public DTOParticipant Home { get; set; }
        public DTOParticipant Away { get; set; }
        public string BackgroundImageLink { get; set; }
        public string Venue { get; set; }
        public List<DTOPeriod> Periods = new List<DTOPeriod>();
        public string State { get; set; }
    }

    public class DTOParticipant
    {
        public string Name { get; set; }
        public string LogoLink { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OtLosses { get; set; }
        public int Shots { get; set; }
        public int Goals { get; set; }
    }

    public class DTOPeriodParticipant
    {
        public int Shots { get; set; }
        public int Goals { get; set; }
        public List<DTOEvent> Events = new List<DTOEvent>();
    }

    public class DTOPeriod
    {
        public DTOPeriodParticipant Home { get; set; }
        public DTOPeriodParticipant Away { get; set; }
        public string Length { get; set; }
        public int Number { get; set; }
    }

    public class DTOEvent
    {
        public List<DTOPlayer> Players = new List<DTOPlayer>();
        public string Description { get; set; }
        public string Strength { get; set; }
        public string PeriodTime { get; set; }
        public string EventType { get; set; }
        public DateTime Time { get; set; }
        public bool EmptyNet { get; set; }
        public bool GameWinningGoal { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
    }

    public class DTOPlayer
    {
        public string PortraitLink { get; set; }
        public string Name { get; set; }
        public string NhlHornanLink { get; set; }
        public string PlayerType { get; set; }
        public string Id { get; set; }
    }
}
