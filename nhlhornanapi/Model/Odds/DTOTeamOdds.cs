namespace nhlhornanapi.Model.Odds
{
    public class DTOTeamOdds
    {
        public string Name { get; set; }
        public MatchStatsPair MatchStats { get; set; }
        public NullableOddsPair AverageOdds { get; set; }
        public NullableScoreOddsPair ScoreOdds { get; set; }
        public SpecificOddsSetPair HighestOdds { get; set; }
        public SpecificOddsSetPair LowestOdds { get; set; }
    }

    public class SpecificOdds
    {
        public SpecificOdds(Database.Entity.Match entity)
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

    public class SpecificOddsSet
    {
        public SpecificOddsSet(SpecificOdds home, SpecificOdds draw, SpecificOdds away)
        {
            Home = home;
            Draw = draw;
            Away = away;
        }

        public SpecificOdds Home { get; set; }
        public SpecificOdds Draw { get; set; }
        public SpecificOdds Away { get; set; }
    }

    public class SpecificOddsSetPair
    {
        public SpecificOddsSetPair(SpecificOddsSet home, SpecificOddsSet away)
        {
            Home = home;
            Away = away;
        }

        public SpecificOddsSet Home { get; set; }
        public SpecificOddsSet Away { get; set; }
    }

    public class MatchStatsPair
    {
        public MatchStatsPair(MatchStats home, MatchStats away, MatchStats total)
        {
            Total = total;
            Home = home;
            Away = away;
        }

        public MatchStats Total{ get; set; }
        public MatchStats Home { get; set; }
        public MatchStats Away { get; set; }
    }

    public class MatchStats
    {
        public int Amount { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int OtLosses { get; set; }
        public double PercentOfPoints { get; set; }
    }

    public class NullableScoreOddsPair
    {
        public NullableOddsPair Factors { get; set; }
        public NullableOddsPair Score { get; set; }

        public NullableScoreOddsPair(NullableOddsPair factors, NullableOddsPair score)
        {
            Factors = factors;
            Score = score;
        }
    }

    public class NullableOdds
    {
        public double? Home { get; set; }
        public double? Draw { get; set; }
        public double? Away { get; set; }
    }

    public class NullableOddsPair
    {
        public NullableOdds Home { get; set; }
        public NullableOdds Away { get; set; }

        public NullableOddsPair(NullableOdds home, NullableOdds away)
        {
            Home = home;
            Away = away;
        }
    }
}