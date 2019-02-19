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
        public int salary { get; set; }
    }

    public class JsonPlayer
    {
        public List<Player> data { get; set; }
        public int total { get; set; }

        public static JsonPlayer Get(string stringResult)
        {
            var rawPlayer = JsonConvert.DeserializeObject<JsonPlayer>(stringResult);

            return rawPlayer;
        }
    }


}
