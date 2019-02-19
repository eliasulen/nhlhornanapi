using Microsoft.AspNetCore.Mvc;
using nhlhornanapi.Model;
using nhlhornanapi.Model.Player;
using nhlhornanapi.Util;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace nhlhornanapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[Controller]")]
    public class PlayerSalaryController : Controller
    {
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

                    var jsonPlayer = JsonPlayer.Get(await response.Content.ReadAsStringAsync());

                    var salaryUrl = $"{Constants.HockeyReferenceLinks.Base}{Constants.HockeyReferenceLinks.Salaries}";
                    var salaryResponse = await client.GetStringAsync(salaryUrl);

                    var salaryResponseList = salaryResponse.Split(new string[] { "data-stat=" }, StringSplitOptions.None).ToList();
                    salaryResponseList.RemoveAll(x => (!x.Contains("salary") && !x.Contains("player")) || x.Contains("DOCTYPE") || x.Contains("scope"));

                    var playerEntries = salaryResponseList.Where(x => x.Contains("player")).ToList();
                    var salaryEntries = salaryResponseList.Where(x => x.Contains("salary")).ToList();

                    if (playerEntries.Count != salaryEntries.Count)
                        return BadRequest("SalaryData mismatch.");

                    string playerSplitStart = ".html\">";
                    string playerSplitEnd = "</a></th>";
                    string salarySplitStart = "salary\" >";
                    string salarySplitEnd = "</td><td";

                    for(int i = 0; i < playerEntries.Count; i++)
                    {
                        var p = playerEntries[i];
                        var s = salaryEntries[i];

                        p = p.Substring(p.IndexOf(playerSplitStart) + playerSplitStart.Length
                            , p.IndexOf(playerSplitEnd) - p.IndexOf(playerSplitStart) - playerSplitStart.Length);
                        s = s.Substring(s.IndexOf(salarySplitStart) + salarySplitStart.Length
                            , s.IndexOf(salarySplitEnd) - s.IndexOf(salarySplitStart) - salarySplitStart.Length);
                    }

                    return Ok(jsonPlayer);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }
        }
    }
}