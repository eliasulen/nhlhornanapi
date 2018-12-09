using AutoMapper;
using nhlhornanapi.Model.Recent;
using nhlhornanapi.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace nhlhornanapi.Map.Schedule
{
    internal class RecentConverter : ITypeConverter<JsonRecent, IEnumerable<DTORecent>>
    {
        public IEnumerable<DTORecent> Convert(JsonRecent source, IEnumerable<DTORecent> destination, ResolutionContext context)
        {
            List<DTORecent> recents = new List<DTORecent>();

            var games = source.dates.SelectMany(d => d.games).ToList();

            var sampleGame = games.FirstOrDefault();

            foreach(var g in games)
            {
                var recent = new DTORecent();

                var home = new DTOParticipant();
                var away = new DTOParticipant();

                home.LogoLink = LinkBuilder.GetLogoLink(g.teams.home.team.id);
                home.Name = g.teams.home.team.name;
                home.Wins = g.teams.home.leagueRecord.wins;
                home.OtLosses = g.teams.home.leagueRecord.ot;
                home.Losses = g.teams.home.leagueRecord.losses;
                home.Shots = g.linescore.teams.home.shotsOnGoal;
                home.Goals = g.linescore.teams.home.goals;

                away.LogoLink = LinkBuilder.GetLogoLink(g.teams.away.team.id);
                away.Name = g.teams.away.team.name;
                away.Wins = g.teams.away.leagueRecord.wins;
                away.OtLosses = g.teams.away.leagueRecord.ot;
                away.Losses = g.teams.away.leagueRecord.losses;
                away.Shots = g.linescore.teams.away.shotsOnGoal;
                away.Goals = g.linescore.teams.away.goals;

                List<DTOPeriod> periods = new List<DTOPeriod>();
                foreach(var p in g.linescore.periods)
                {
                    var period = new DTOPeriod();
                    List<DTOEvent> pEvents = new List<DTOEvent>();
                    period.Number = p.num;
                    var periodMinutes = (p.endTime - p.startTime).TotalMinutes;
                    var periodSeconds = Math.Round((periodMinutes - Math.Truncate(periodMinutes)) * 60, 0);
                    period.Length = $"{Math.Truncate(periodMinutes)}m, {periodSeconds}s";

                    var pHome = new DTOPeriodParticipant() { Goals = p.home.goals, Shots = p.home.shotsOnGoal };
                    var pAway = new DTOPeriodParticipant() { Goals = p.away.goals, Shots = p.away.shotsOnGoal };
                    

                    var events = g.scoringPlays.Where(sp => sp.about.period == p.num);
                    foreach(var ev in events)
                    {
                        var pEvent = new DTOEvent()
                        {
                            Description = ev.result.description,
                            EventType = ev.result.@event,
                            PeriodTime = ev.about.periodTime,
                            Time = ev.about.dateTime,
                            EmptyNet = ev.result.emptyNet,
                            GameWinningGoal = ev.result.gameWinningGoal,
                            Strength = ev.result.strength.name,
                            HomeGoals = ev.about.goals.home,
                            AwayGoals = ev.about.goals.away,
                            Players = ev.players.Select(pe => new DTOPlayer()
                            {
                                Name = pe.player.fullName,
                                NhlHornanLink = string.Empty, //TODO§
                                Id = pe.player.id.ToString(),
                                PortraitLink = LinkBuilder.GetPortraitLink(pe.player.id),
                                PlayerType = pe.playerType
                            }).ToList()
                        };

                        pEvents.Add(pEvent);

                    }

                    for(int i = 0; i < pEvents.Count; i++)
                    {
                        if(i == 0)
                        {
                            if (pEvents[i].HomeGoals > pEvents[i].AwayGoals)
                                pHome.Events.Add(pEvents[i]);
                            else pAway.Events.Add(pEvents[i]);
                        }
                        else
                        {
                            if (pEvents[i - 1].HomeGoals == pEvents[i].HomeGoals)
                                pAway.Events.Add(pEvents[i]);
                            else pHome.Events.Add(pEvents[i]);
                        }
                    }

                    period.Home = pHome;
                    period.Away = pAway;
                    periods.Add(period);
                }

                recent.Venue = g.venue.name;
                recent.Date = g.gameDate;
                recent.Home = home;
                recent.Away = away;
                recent.State = g.status.detailedState;

                recents.Add(recent);
            }

            return recents;
        }

    }
}