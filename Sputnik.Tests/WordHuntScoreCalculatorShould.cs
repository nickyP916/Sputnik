
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
}
