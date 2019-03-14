using Microsoft.AspNetCore.Mvc;
using nhlhornanapi.Model;
using nhlhornanapi.Model.Player;
using nhlhornanapi.Util;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;

namespace nhlhornanapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class PlayerSalaryController : Controller
    {
        private readonly IMapper _mapper;

        public PlayerSalaryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var url = $"{Constants.RestLinks.Base}{Constants.RestLinks.Player}";

                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonPlayers = JsonPlayers.Get(await response.Content.ReadAsStringAsync());

                    var salaryUrl = $"{Constants.HockeyReferenceLinks.Base}{Constants.HockeyReferenceLinks.Salaries}";
                    var salaryResponse = await client.GetStringAsync(salaryUrl);

                    JsonPlayers.FillSalaries(salaryResponse, jsonPlayers);

                    return Ok(_mapper.Map<IEnumerable<DTOPlayerSalary>>(jsonPlayers)
                        .Where(x => x.Salary.HasValue && x.GamesPlayed > 30)
                        .OrderBy(x => x.UnitPerPoint)
                        .ToList());
                }
                
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }
}