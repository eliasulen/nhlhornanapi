using Microsoft.AspNetCore.Mvc;
using nhlhornanapi.Database.Repository;
using nhlhornanapi.Database.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using nhlhornanapi.Model.Odds;
using AutoMapper;
using System;

namespace nhlhornanapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class TeamOddsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMatchRepository _matchRepository;

        public TeamOddsController(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var matches = await _matchRepository.GetAllMatches();
                return Ok(_mapper.Map<IEnumerable<DTOTeamOdds>>(matches));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{season}")]
        public async Task<IActionResult> Get(string season)
        {
            try
            {
                var matches = await _matchRepository.GetMatchesBySeason(season);
                return Ok(_mapper.Map<IEnumerable<DTOTeamOdds>>(matches));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{season}/{team}")]
        public async Task<IActionResult> Get(string season, string team)
        {
            try
            {
                var matches = await _matchRepository.GetMatchesBySeasonTeam(season, team);
                return Ok(_mapper.Map<IEnumerable<DTOTeamOdds>>(matches));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}