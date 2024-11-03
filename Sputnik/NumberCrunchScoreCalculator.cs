namespace Sputnik
{
    internal static class NumberCrunchScoreCalculator
    {
        public static int CalculateScore(int _roundsWon)
        {
            if (_roundsWon == 0)
                return 0;
            else
                return Calculate(_roundsWon);
        }

        private static int Calculate(int round)
        {
            int totalScore;

            if (round == 1)
            {
                totalScore = round;
                return totalScore;
            }
            else
            {
                var score = CalculateScore(round - 1);
                return score * 2;
            }
        }
    }
}
