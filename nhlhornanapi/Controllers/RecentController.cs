using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using nhlhornanapi.Model.Recent;
using nhlhornanapi.Util;

namespace nhlhornanapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class RecentController : Controller
    {
        private readonly IMapper _mapper;

        public RecentController(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<IActionResult> Get()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var recentStandard = DateUtil.RecentStandard;

                    var url = $"{Constants.ApiLinks.Base}{Constants.ApiLinks.ScheduleLinescoreScoringplays}startDate=" +
                          $"{recentStandard.StartDate.ToShortDateString()}&endDate={recentStandard.EndDate.ToShortDateString()}";

                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonRecent = JsonRecent.Get(await response.Content.ReadAsStringAsync());

                    return Ok(_mapper.Map<IEnumerable<DTORecent>>(jsonRecent));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }
}