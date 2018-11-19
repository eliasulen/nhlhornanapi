using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhlhornanapi.Database.Entity;
using nhlhornanapi.Database.Context;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace nhlhornanapi.Database.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private readonly MatchContext _context = null;

        public MatchRepository(IOptions<DatabaseSettings> settings)
        {
            _context = new MatchContext(settings);
        }

        public async Task<IEnumerable<Match>> GetAllMatches()
        {
            var matches = await _context.Matches.Find(_ => true).ToListAsync();
            return matches;
        }

        public async Task<IEnumerable<Match>> GetMatchesBySeason(string season)
        {
            List<Match> matches = new List<Match>();

            if (season.ToLower() == "any")
                matches = await _context.Matches.Find(_ => true).ToListAsync();
            else
                matches = await _context.Matches.Find(match => match.Season == season).ToListAsync();
            return matches;
        }

        public async Task<IEnumerable<Match>> GetMatchesBySeasonTeam(string season, string team)
        {
            List<Match> matches = new List<Match>();
            if(season.ToLower() == "any")
                matches = await _context.Matches.Find(match => match.Home.Team == team || match.Away.Team == team).ToListAsync();

            matches = await _context.Matches.Find(match => match.Season == season && (match.Home.Team == team || match.Away.Team == team)).ToListAsync();
            return matches;
        }


        public async Task<IEnumerable<Match>> GetMatchesByTeam(string team)
        {
            var matches = await _context.Matches.Find(match => match.Home.Team == team || match.Away.Team == team).ToListAsync();
            return matches;
        }

        public async Task AddMatch(Match item)
        {
            try
            {
                await _context.Matches.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
