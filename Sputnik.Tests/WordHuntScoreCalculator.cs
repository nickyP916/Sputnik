
namespace Sputnik.Tests
{
    internal class WordHuntScoreCalculator
    {
        internal static int Calculate(IEnumerable<string> words, int maxWordLength)
        {
            var score = 0;
            foreach (var word in words)
            {
                int points = 0;
                int length = word.Length;

                if (length == maxWordLength)
                    points = length * 2;
                else
                    points = length;

                score += points;
                Console.WriteLine(points + " points!");
            }

            return score;
        }
    }
}
