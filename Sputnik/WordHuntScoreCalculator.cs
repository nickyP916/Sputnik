
namespace Sputnik
{
    internal class WordHuntScoreCalculator
    {
        private static int score = 0;

        internal static int Calculate(string word, int minWordLength, int maxWordLength)
        {
            var points = 0;
            if (word.Length < minWordLength || word.Length > maxWordLength)
                return points;

            var length = word.Length;

            if (length == maxWordLength)
                points = length * 2;
            else
                points = length;

            score += points;

            return points;
        }

        internal static int GetFinalScore() => score;
    }
}
