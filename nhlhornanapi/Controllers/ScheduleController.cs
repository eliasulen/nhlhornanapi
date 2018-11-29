using Microsoft.AspNetCore.Mvc;
using nhlhornanapi.Model;
using nhlhornanapi.Util;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using nhlhornanapi.Model.Schedule;
using AutoMapper;
using System.Collections.Generic;

namespace nhlhornanapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class ScheduleController : Controller
    {
        private readonly IMapper _mapper;

        public ScheduleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime? startDate, DateTime? endDate, int? utcOffset)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    if (utcOffset.HasValue)
                        Globals.SetUtcOffset(utcOffset.Value);

                    if (!endDate.HasValue && !startDate.HasValue)
                        (startDate, endDate) = DateUtil.ScheduleStandard;

                    if (!endDate.HasValue)
                        endDate = DateUtil.ScheduleStandard.EndDate;

                    if (!startDate.HasValue)
                        startDate = DateUtil.ScheduleStandard.StartDate;

                    var url = $"{Constants.Links.Base}{Constants.Links.ScheduleLinescore}startDate=" +
                        $"{startDate.Value.AddDays(-1).ToShortDateString()}&endDate={endDate.Value.AddDays(1).ToShortDateString()}";

                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var jsonSchedule = JsonSchedule.Get(await response.Content.ReadAsStringAsync(), startDate.Value, endDate.Value);

                    return Ok(_mapper.Map<IEnumerable<DTOSchedule>>(jsonSchedule));
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }
}