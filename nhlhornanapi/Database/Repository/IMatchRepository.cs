using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nhlhornanapi.Database.Entity;

namespace nhlhornanapi.Database.Repository
{
    public interface IMatchRepository
    {
        Task<IEnumerable<Match>> GetAllMatches();
        Task<IEnumerable<Match>> GetMatchesBySeason(string season);
        Task<IEnumerable<Match>> GetMatchesByTeam(string team);
        Task<IEnumerable<Match>> GetMatchesBySeasonTeam(string season, string team);
        Task AddMatch(Match match);
    }
}
