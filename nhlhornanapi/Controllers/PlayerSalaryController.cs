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

                    string playerSplitStartHtml = ".html\">";
                    string playerSplitEndHtml = "</a></th>";
                    string playerSplitStartNoHtml = "player\" >";
                    string playerSplitEndNoHtml = "</th><td";
                    string salarySplitStart = "salary\" >";
                    string salarySplitEnd = "</td><td";

                    for(int i = 0; i < playerEntries.Count; i++)
                    {
                        try
                        {

                            var p = playerEntries[i];
                            var s = salaryEntries[i];

                            string playerSplitStart = p.Contains("html") ? playerSplitStartHtml : playerSplitStartNoHtml;
                            string playerSplitEnd = p.Contains("html") ? playerSplitEndHtml : playerSplitEndNoHtml;

                            p = p.Substring(p.IndexOf(playerSplitStart) + playerSplitStart.Length
                                , p.IndexOf(playerSplitEnd) - p.IndexOf(playerSplitStart) - playerSplitStart.Length);
                            s = s.Substring(s.IndexOf(salarySplitStart) + salarySplitStart.Length
                                , s.IndexOf(salarySplitEnd) - s.IndexOf(salarySplitStart) - salarySplitStart.Length);

                            var player = jsonPlayer.data.FirstOrDefault(pl => pl.playerName == p);
                            if (player != null)
                            {
                                player.salary = Convert.ToInt32(s.Replace(",", string.Empty));
                            }
                            else if(p.IndexOf(',') != -1)
                            {
                                var split = p.Split(',');
                                var firstName = split[1] != null ? split[1].Replace(" ", string.Empty) : string.Empty;
                                var lastName = split[0] != null ? split[0].Replace(" ", string.Empty) : string.Empty;

                                player = jsonPlayer.data.FirstOrDefault(pl => pl.playerFirstName == firstName && pl.playerLastName == lastName);
                                if(player != null)
                                {
                                    player.salary = Convert.ToInt32(s.Replace(",", string.Empty));
                                }
                                else
                                {
                                    player = jsonPlayer.data.Count(pl => pl.playerLastName == lastName) == 1
                                        ? jsonPlayer.data.FirstOrDefault(pl => pl.playerLastName == lastName) : null;
                                    if(player != null)
                                    {
                                        player.salary = Convert.ToInt32(s.Replace(",", string.Empty));
                                    }
                                }
                            }
                        }catch(Exception e)
                        {
                            var y = "bb";
                        }
                    }


                    var test = jsonPlayer.data.Where(x => !x.salary.HasValue).ToList();

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