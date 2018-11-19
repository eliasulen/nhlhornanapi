using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static nhlhornanapi.Enumeration.Odds.OddsEnumeration;

namespace nhlhornanapi.Database.Entity
{
    public class Match
    {
        [BsonId]
        public ObjectId InternalId { get; set; }

        //public string Id { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; } 

        public string Key { get; set; }
        public DateTime Date { get; set; }
        public Home Home { get; set; }
        public Away Away { get; set; }
        public MatchOutcome Outcome { get; set; }
        public MatchType MatchType { get; set; }
        public Odds Odds { get; set; }
        public WinningMethod WinningMethod { get; set; }
        public string FullTime { get; set; }
        public string Total { get; set; }
        public string Season { get; set; }

        public bool IsParticipant(string team) => Home.Team == team || Away.Team == team;
    }

    public class Home
    {
        public string Team { get; set; }
        public int? Points { get; set; }
        public int Goals { get; set; }
        public int? OvertimeGoals { get; set; } 
        public ParticipantOutcome Result { get; set; }
    }

    public class Away
    {
        public string Team { get; set; }
        public int? Points { get; set; }
        public int Goals { get; set; }
        public int? OvertimeGoals { get; set; } 
        public ParticipantOutcome Result { get; set; }
    }

    public class Odds
    {
        public double Home { get; set; }
        public double Draw { get; set; }
        public double Away { get; set; }
    }   
}
