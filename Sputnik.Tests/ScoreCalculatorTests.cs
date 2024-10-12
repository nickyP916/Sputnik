namespace Sputnik.Tests
{
    public class ScoreCalculatorShould
    {
        [TestCase(0,0)]
        [TestCase(1,1)]
        [TestCase(3,4)]
        [TestCase(5,16)]
        [TestCase(7,64)]
        public void Calculate_the_score(int score, int expected)
        {
            var result = ScoreCalculator.CalculateScore(score);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}