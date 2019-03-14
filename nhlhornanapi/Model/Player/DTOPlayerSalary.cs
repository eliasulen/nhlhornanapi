using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Model.Player
{
    public class DTOPlayerSalary
    {
        public string Name { get; set; }
        public string ProfileLink { get; set; }

        public int GameWinningGoals { get; set; }

        public int Assists { get; set; }
        public int Goals { get; set; }

        public int? Salary { get; set; }
        public int Points { get; set; }

        public double? UnitPerPoint { get; set; }
        public double? UnitPerGoal { get; set; }

        public double? UnitPerAssist { get; set; }

        public int GamesPlayed { get; set; }
    }
}
