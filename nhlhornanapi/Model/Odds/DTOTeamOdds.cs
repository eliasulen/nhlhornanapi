namespace nhlhornanapi.Model.Odds
{
    public class DTOTeamOdds
    {
        public string Name { get; set; }
        public DTODTOMatchStatsPair DTOMatchStats { get; set; }
        public DTONullableOddsPair AverageOdds { get; set; }
        public DTONullableScoreOddsPair ScoreOdds { get; set; }
        public DTOSpecificOddsSetPair HighestOdds { get; set; }
        public DTOSpecificOddsSetPair LowestOdds { get; set; }
    }

    public class DTOSpecificOdds
    {
        public DTOSpecificOdds(Database.Entity.Match entity)
        {
            Key = entity.Key;
            Odds = new Odds()
            {
                Away = entity.Odds.Away,
                Draw = entity.Odds.Draw,
                Home = entity.Odds.Home
            };

            Outcome = entity.Outcome.ToString();
            Result = entity.FullTime != entity.Total ? $"{entity.Total} ({entity.FullTime})" : entity.FullTime;
        }

        public string Key { get; set; }
        public string Outcome { get; set; }
        public string Result { get; set; }
        public Odds Odds { get; set; }
    }

    public class DTOSpecificOddsSet
    {
        public DTOSpecificOddsSet(DTOSpecificOdds home, DTOSpecificOdds draw, DTOSpecificOdds away)
        {
            Home = home;
            Draw = draw;
            Away = away;
        }

        public DTOSpecificOdds Home { get; set; }
        public DTOSpecificOdds Draw { get; set; }
        public DTOSpecificOdds Away { get; set; }
    }

    public class DTOSpecificOddsSetPair
    {
        public DTOSpecificOddsSetPair(DTOSpecificOddsSet home, DTOSpecificOddsSet away)
        {
            Home = home;
            Away = away;
        }

        public DTOSpecificOddsSet Home { get; set; }
        public DTOSpecificOddsSet Away { get; set; }
    }

    public class DTODTOMatchStatsPair
    {
        public DTODTOMatchStatsPair(DTOMatchStats home, DTOMatchStats away, DTOMatchStats total)
        {
            Total = total;
            Home = home;
            Away = away;
        }

        public DTOMatchStats Total{ get; set; }
        public DTOMatchStats Home { get; set; }
        public DTOMatchStats Away { get; set; }
    }

    public class DTOMatchStats
    {
        public int Amount { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OtLosses { get; set; }
        public double PercentOfPoints { get; set; }
    }

    public class DTONullableScoreOddsPair
    {
        public DTONullableOddsPair Factors { get; set; }
        public DTONullableOddsPair Score { get; set; }

        public DTONullableScoreOddsPair(DTONullableOddsPair factors, DTONullableOddsPair score)
        {
            Factors = factors;
            Score = score;
        }
    }

    public class DTONullableOdds
    {
        public double? Home { get; set; }
        public double? Draw { get; set; }
        public double? Away { get; set; }
    }

    public class DTONullableOddsPair
    {
        public DTONullableOdds Home { get; set; }
        public DTONullableOdds Away { get; set; }

        public DTONullableOddsPair(DTONullableOdds home, DTONullableOdds away)
        {
            Home = home;
            Away = away;
        }
    }
}