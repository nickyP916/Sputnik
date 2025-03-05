namespace Sputnik.Tests
{
    public class RoundsScoreCalculatorShould
    {
        [TestCase(0,0)]
        [TestCase(1,1)]
        [TestCase(3,4)]
        [TestCase(5,16)]
        [TestCase(7,64)]
        public void Calculate_the_score(int score, int expected)
        {
            var result = RoundsScoreCalculator.CalculateScore(score);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}