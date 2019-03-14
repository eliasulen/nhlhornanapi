using Newtonsoft.Json;
using nhlhornanapi.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhlhornanapi.Model.Player
{

    public class Player
    {
        public int assists { get; set; }
        public double faceoffWinPctg { get; set; }
        public int gameWinningGoals { get; set; }
        public int gamesPlayed { get; set; }
        public int goals { get; set; }
        public int otGoals { get; set; }
        public int penaltyMinutes { get; set; }
        public string playerBirthCity { get; set; }
        public string playerBirthCountry { get; set; }
        public string playerBirthDate { get; set; }
        public string playerBirthStateProvince { get; set; }
        public int? playerDraftOverallPickNo { get; set; }
        public int? playerDraftRoundNo { get; set; }
        public int? playerDraftYear { get; set; }
        public string playerFirstName { get; set; }
        public int playerHeight { get; set; }
        public int playerId { get; set; }
        public int playerInHockeyHof { get; set; }
        public int playerIsActive { get; set; }
        public string playerLastName { get; set; }
        public string playerName { get; set; }
        public string playerNationality { get; set; }
        public string playerPositionCode { get; set; }
        public string playerShootsCatches { get; set; }
        public string playerTeamsPlayedFor { get; set; }
        public int playerWeight { get; set; }
        public int plusMinus { get; set; }
        public int points { get; set; }
        public double pointsPerGame { get; set; }
        public int ppGoals { get; set; }
        public int ppPoints { get; set; }
        public int seasonId { get; set; }
        public int shGoals { get; set; }
        public int shPoints { get; set; }
        public double shiftsPerGame { get; set; }
        public double shootingPctg { get; set; }
        public int shots { get; set; }
        public double timeOnIcePerGame { get; set; }
        public int? salary { get; set; }
    }

    public class JsonPlayers
    {
        public List<Player> data { get; set; }
        public int total { get; set; }

        public static JsonPlayers Get(string stringResult)
        {
            var rawPlayer = JsonConvert.DeserializeObject<JsonPlayers>(stringResult);

            return rawPlayer;
        }

        public static void FillSalaries(string salaryResponse, JsonPlayers jsonPlayers)
        {
            var salaryResponseList = salaryResponse.Split(new string[] { "data-stat=" }, StringSplitOptions.None).ToList();
            salaryResponseList.RemoveAll(x => (!x.Contains("salary") && !x.Contains("player")) || x.Contains("DOCTYPE") || x.Contains("scope"));

            var playerEntries = salaryResponseList.Where(x => x.Contains("player")).ToList();
            var salaryEntries = salaryResponseList.Where(x => x.Contains("salary")).ToList();

            if (playerEntries.Count != salaryEntries.Count)
                throw new Exception("SalaryData mismatch.");

            string playerSplitStartHtml = ".html\">";
            string playerSplitEndHtml = "</a></th>";
            string playerSplitStartNoHtml = "player\" >";
            string playerSplitEndNoHtml = "</th><td";
            string salarySplitStart = "salary\" >";
            string salarySplitEnd = "</td><td";

            for (int i = 0; i < playerEntries.Count; i++)
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

                    var player = jsonPlayers.data.FirstOrDefault(pl => pl.playerName == p);
                    if (player != null)
                    {
                        player.salary = Convert.ToInt32(s.Replace(",", string.Empty));
                    }
                    else if (p.IndexOf(',') != -1)
                    {
                        var split = p.Split(',');
                        var firstName = split[1] != null ? split[1].Replace(" ", string.Empty) : string.Empty;
                        var lastName = split[0] != null ? split[0].Replace(" ", string.Empty) : string.Empty;

                        player = jsonPlayers.data.FirstOrDefault(pl => pl.playerFirstName == firstName && pl.playerLastName == lastName);
                        if (player != null)
                        {
                            player.salary = Convert.ToInt32(s.Replace(",", string.Empty));
                        }
                        else
                        {
                            player = jsonPlayers.data.Count(pl => pl.playerLastName == lastName) == 1
                                ? jsonPlayers.data.FirstOrDefault(pl => pl.playerLastName == lastName) : null;
                            if (player != null)
                            {
                                player.salary = Convert.ToInt32(s.Replace(",", string.Empty));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception($"Error filling salarydata. {jsonPlayers.data[i].playerName}");
                }
            }
        }
    }


}
