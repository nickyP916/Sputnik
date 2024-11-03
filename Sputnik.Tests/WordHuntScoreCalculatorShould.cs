
namespace Sputnik.Tests
{
    internal class WordHuntScoreCalculatorShould
    {
        [Test]
        public void Calculate_final_score()
        {
            var score = WordHuntScoreCalculator.Calculate(Words, 9);

            Assert.That(score, Is.EqualTo(13));
        }

        [Test]
        public void Give_double_points_for_a_word_with_the_maximum_number_of_letters()
        {
            var score = WordHuntScoreCalculator.Calculate(NineLetterWord, 9);
            Assert.That(score, Is.EqualTo(18));
        }

        private static IEnumerable<string> Words
        {
            get
            {
                yield return "MAP";
                yield return "A";
                yield return "DYNAMO";
                yield return "PAD";
            }
        }

        private static IEnumerable<string> NineLetterWord
        {
            get
            {
                yield return "DANGEROUS";
            }
        }
    }

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
