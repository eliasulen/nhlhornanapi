using AutoMapper;
using nhlhornanapi.Model.Schedule;
using nhlhornanapi.Util;
using System.Collections.Generic;
using System.Linq;

namespace nhlhornanapi.Map.Schedule
{
    internal class ScheduleConverter : ITypeConverter<JsonSchedule, IEnumerable<DTOSchedule>>
    {
        public IEnumerable<DTOSchedule> Convert(JsonSchedule source, IEnumerable<DTOSchedule> destination, ResolutionContext context)
        {
            List<DTOSchedule> schedules = new List<DTOSchedule>();

            var teams = (source.dates.Select(d => d.games).
              SelectMany(g => g).
              Select(g => (g.teams.away.team.id, g.teams.away.team.name))).Union
              (source.dates.Select(d => d.games).
              SelectMany(g => g).
              Select(g => (g.teams.home.team.id, g.teams.home.team.name))).Distinct();


            foreach (var t in teams)
            {
                var matches = source.dates.Select(d => d.games).
                    SelectMany(g => g).
                    Where(g => g.teams.home.team.id == t.Item1
                    || g.teams.away.team.id == t.Item1);

                schedules.Add(new DTOSchedule()
                {
                    LogoLink = LinkBuilder.GetLogoLink(t.Item1),
                    AmountOfGames = matches.Count(),
                    Team = t.Item2,
                    Id = t.Item1,
                    Opponents = matches.Select(m =>
                    new DTOSchedule.Opponent()
                    {
                        GameDate = m.gameDate,
                        Away = m.teams.away.team.id == t.Item1,
                        LogoLink = m.teams.away.team.id != t.Item1
                        ? LinkBuilder.GetLogoLink(m.teams.away.team.id)
                        : LinkBuilder.GetLogoLink(m.teams.home.team.id),
                        Status = m.status.statusCode
                    }
                    )
                });
            }

            return schedules;
        }
    }
}