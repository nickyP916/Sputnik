namespace Sputnik.Tests
{
    internal class WordHuntScoreCalculatorShould
    {
        private const int minWordLength = 2;

        [TestCase("MAP", 3)]
        [TestCase("DYNAMO", 6)]
        [TestCase("PAD", 3)]
        [TestCase("A", 0)]
        public void Calculate_the_score(string wordToScore, int expectedScore)
        {
            var score = WordHuntScoreCalculator.Calculate(wordToScore, minWordLength, 9);

            Assert.That(score, Is.EqualTo(expectedScore));
        }

        [Test]
        public void Give_double_points_for_a_word_with_the_maximum_number_of_letters()
        {
            var score = WordHuntScoreCalculator.Calculate("DANGEROUS", minWordLength, 9);
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
    }
}
