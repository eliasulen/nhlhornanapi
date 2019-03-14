using AutoMapper;
using nhlhornanapi.Model.Player;
using nhlhornanapi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Map.Player
{

    internal class PlayerSalaryConverter : ITypeConverter<JsonPlayers, IEnumerable<DTOPlayerSalary>>
    {
        public IEnumerable<DTOPlayerSalary> Convert(JsonPlayers source, IEnumerable<DTOPlayerSalary> destination, ResolutionContext context)
        {
            List<DTOPlayerSalary> playerSalaries = new List<DTOPlayerSalary>();

            foreach(var p in source.data)
            {
                playerSalaries.Add(new DTOPlayerSalary()
                {
                    Name = p.playerName,
                    Salary = p.salary,
                    Points = p.points,
                    GamesPlayed = p.gamesPlayed,
                    GameWinningGoals = p.gameWinningGoals,
                    Assists = p.assists,
                    Goals = p.goals,
                    ProfileLink = LinkBuilder.GetPortraitLink(p.playerId),
                    UnitPerPoint = p.salary.HasValue ? p.salary / (p.points > 0 ? p.points : 1) : null,
                    UnitPerAssist = p.salary.HasValue ? p.salary / (p.assists > 0 ? p.assists : 1 ) : null,
                    UnitPerGoal = p.salary.HasValue ? p.salary / (p.goals > 0 ? p.goals : 1) : null
                });
            }

            return playerSalaries;

        }
    }
}
