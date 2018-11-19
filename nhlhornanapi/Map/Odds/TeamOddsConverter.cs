using AutoMapper;
using nhlhornanapi.Database.Entity;
using nhlhornanapi.Model.Odds;
using System;
using System.Collections.Generic;
using System.Linq;
using static nhlhornanapi.Enumeration.Odds.OddsEnumeration;

namespace nhlhornanapi.Map.Odds
{
    internal class TeamOddsConverter : ITypeConverter<IEnumerable<Match>, IEnumerable<DTOTeamOdds>>
    {
        public IEnumerable<DTOTeamOdds> Convert(IEnumerable<Match> source, IEnumerable<DTOTeamOdds> destination, ResolutionContext context)
        {
            var teams = new List<DTOTeamOdds>();

            var teamNames = source.GroupBy(s => s.Home.Team).Select(g => g.First().Home.Team).Concat(source.GroupBy(s => s.Away.Team).Select(g => g.First().Away.Team)).Distinct().OrderBy(t => t);

            foreach (var t in teamNames)
            {
                var name = t;
                var amountOfGames = source.Where(s => s.Away.Team == t || s.Home.Team == t).Count();
                var homeGames = source.Where(s => s.Home.Team == t);
                var awayGames = source.Where(s => s.Away.Team == t);

                var averageHomeOdds = new NullableOdds()
                {
                    Home = homeGames.Any()
                    ? Math.Round(homeGames.Average(s => s.Odds.Home), 2)
                    : (double?)null,
                    Away = homeGames.Any()
                    ? Math.Round(homeGames.Average(s => s.Odds.Away), 2)
                    : (double?)null,
                    Draw = homeGames.Any()
                    ? Math.Round(homeGames.Average(s => s.Odds.Draw), 2)
                    : (double?)null
                };
                var averageAwayOdds = new NullableOdds()
                {
                    Home = awayGames.Any()
                    ? Math.Round(awayGames.Average(s => s.Odds.Home), 2)
                    : (double?)null,
                    Away = awayGames.Any()
                    ? Math.Round(awayGames.Average(s => s.Odds.Away), 2)
                    : (double?)null,
                    Draw = awayGames.Any()
                    ? Math.Round(awayGames.Average(s => s.Odds.Draw), 2)
                    : (double?)null
                };

                var averageHomeOddsOutcome = new NullableOdds()
                {
                    Home = homeGames.Where(s => s.Outcome == MatchOutcome.Home).Any()
                    ? Math.Round(homeGames.Where(s => s.Outcome == MatchOutcome.Home).Average(s => s.Odds.Home), 2)
                    : (double?)null,
                    Draw = homeGames.Where(s => s.Outcome == MatchOutcome.Draw).Any()
                    ? Math.Round(homeGames.Where(s => s.Outcome == MatchOutcome.Draw).Average(s => s.Odds.Draw), 2)
                    : (double?)null,
                    Away = homeGames.Where(s => s.Outcome == MatchOutcome.Away).Any()
                    ? Math.Round(homeGames.Where(s => s.Outcome == MatchOutcome.Away).Average(s => s.Odds.Away), 2)
                    : (double?)null,
                };

                var averageAwayOddsOutcome = new NullableOdds()
                {
                    Home = awayGames.Where(s => s.Outcome == MatchOutcome.Home).Any()
                    ? Math.Round(awayGames.Where(s => s.Outcome == MatchOutcome.Home).Average(s => s.Odds.Home), 2)
                    : (double?)null,
                    Draw = awayGames.Where(s => s.Outcome == MatchOutcome.Draw).Any()
                    ? Math.Round(awayGames.Where(s => s.Outcome == MatchOutcome.Draw).Average(s => s.Odds.Draw), 2)
                    : (double?)null,
                    Away = awayGames.Where(s => s.Outcome == MatchOutcome.Away).Any()
                    ? Math.Round(awayGames.Where(s => s.Outcome == MatchOutcome.Away).Average(s => s.Odds.Away), 2)
                    : (double?)null
                };

                var scoreHomeOdds = new NullableOdds()
                {
                    Home = Math.Round((homeGames.Where(s => s.Outcome == MatchOutcome.Home).Count() * averageHomeOddsOutcome.Home.GetValueOrDefault(0) - homeGames.Count()), 2),
                    Draw = Math.Round((homeGames.Where(s => s.Outcome == MatchOutcome.Draw).Count() * averageHomeOddsOutcome.Draw.GetValueOrDefault(0) - homeGames.Count()), 2),
                    Away = Math.Round((homeGames.Where(s => s.Outcome == MatchOutcome.Away).Count() * averageHomeOddsOutcome.Away.GetValueOrDefault(0) - homeGames.Count()), 2)
                };

                var scoreAwayOdds = new NullableOdds()
                {
                    Home = Math.Round((awayGames.Where(s => s.Outcome == MatchOutcome.Home).Count() * averageAwayOddsOutcome.Home.GetValueOrDefault(0) - awayGames.Count()), 2),
                    Draw = Math.Round((awayGames.Where(s => s.Outcome == MatchOutcome.Draw).Count() * averageAwayOddsOutcome.Draw.GetValueOrDefault(0) - awayGames.Count()), 2),
                    Away = Math.Round((awayGames.Where(s => s.Outcome == MatchOutcome.Away).Count() * averageAwayOddsOutcome.Away.GetValueOrDefault(0) - awayGames.Count()), 2)
                };

                var matchHomeStats = new MatchStats()
                {
                    Wins = homeGames.Where(s => s.Outcome == MatchOutcome.Home).Count(),
                    Draws = homeGames.Where(s => s.Outcome == MatchOutcome.Draw).Count(),
                    Losses = homeGames.Where(s => s.Outcome == MatchOutcome.Away).Count(),
                    Amount = homeGames.Count()
                };

                var matchAwayStats = new MatchStats()
                {
                    Wins = awayGames.Where(s => s.Outcome == MatchOutcome.Away).Count(),
                    Draws = awayGames.Where(s => s.Outcome == MatchOutcome.Draw).Count(),
                    Losses = awayGames.Where(s => s.Outcome == MatchOutcome.Home).Count(),
                    Amount = awayGames.Count()
                };

                var homeHighestHomeOdds = new SpecificOdds(homeGames.OrderByDescending(s => s.Odds.Home).FirstOrDefault());
                var homeHighestDrawOdds = new SpecificOdds(homeGames.OrderByDescending(s => s.Odds.Draw).FirstOrDefault());
                var homeHighestAwayOdds = new SpecificOdds(homeGames.OrderByDescending(s => s.Odds.Away).FirstOrDefault());
                var homeLowestHomeOdds = new SpecificOdds(homeGames.OrderBy(s => s.Odds.Home).FirstOrDefault());
                var homeLowestDrawOdds = new SpecificOdds(homeGames.OrderBy(s => s.Odds.Draw).FirstOrDefault());
                var homeLowestAwayOdds = new SpecificOdds(homeGames.OrderBy(s => s.Odds.Away).FirstOrDefault());
                var awayHighestHomeOdds = new SpecificOdds(awayGames.OrderByDescending(s => s.Odds.Home).FirstOrDefault());
                var awayHighestDrawOdds = new SpecificOdds(awayGames.OrderByDescending(s => s.Odds.Draw).FirstOrDefault());
                var awayHighestAwayOdds = new SpecificOdds(awayGames.OrderByDescending(s => s.Odds.Away).FirstOrDefault());
                var awayLowestHomeOdds = new SpecificOdds(awayGames.OrderBy(s => s.Odds.Home).FirstOrDefault());
                var awayLowestDrawOdds = new SpecificOdds(awayGames.OrderBy(s => s.Odds.Draw).FirstOrDefault());
                var awayLowestAwayOdds = new SpecificOdds(awayGames.OrderBy(s => s.Odds.Away).FirstOrDefault());

                var homeHighestSet = new SpecificOddsSet(homeHighestHomeOdds, homeHighestDrawOdds, homeHighestAwayOdds);
                var awayHighestSet = new SpecificOddsSet(awayHighestHomeOdds, awayHighestDrawOdds, awayHighestAwayOdds);
                var homeLowestSet = new SpecificOddsSet(homeLowestHomeOdds, homeLowestDrawOdds, homeLowestAwayOdds);
                var awayLowestSet = new SpecificOddsSet(awayLowestHomeOdds, awayLowestDrawOdds, awayLowestAwayOdds);

                teams.Add(new DTOTeamOdds()
                {
                    Name = name,
                    MatchStats = new MatchStatsPair(matchHomeStats, matchAwayStats, amountOfGames),
                    AverageOdds = new NullableOddsPair(averageHomeOdds, averageAwayOdds),
                    ScoreOdds = new NullableScoreOddsPair(new NullableOddsPair(averageHomeOddsOutcome, averageAwayOddsOutcome), new NullableOddsPair(scoreHomeOdds, scoreAwayOdds)),
                    HighestOdds = new SpecificOddsSetPair(homeHighestSet, awayHighestSet),
                    LowestOdds = new SpecificOddsSetPair(homeLowestSet, awayLowestSet)
                });
            }

            var specifiedTeam = teams.FirstOrDefault(t => t.MatchStats.Amount == source.Count());
            if (specifiedTeam != null)
                return new List<DTOTeamOdds>() { specifiedTeam };

            return teams;
        }
    }
}