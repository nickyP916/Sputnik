using FakeItEasy;


namespace Sputnik.Tests
{
    public class LetterGeneratorShould
    {
        Random fakeRand;
        private const string consonants = "BCDFGHJKLMNPQRSTVWXYZ";
        private const string vowels = "AEIOU";
        LetterGenerator letterGenerator;

        [SetUp]
        public void SetUp()
        {
            fakeRand = A.Fake<Random>();
            A.CallTo(() => fakeRand.Next(consonants.Length)).ReturnsNextFromSequence(5, 17, 19, 17, 1);
            A.CallTo(() => fakeRand.Next(vowels.Length)).ReturnsNextFromSequence(4, 1, 2, 0);

            letterGenerator = new LetterGenerator();
        }

        [Test]
        public void Generate_nine_random_letters()
        {
            string letters = LetterGenerator.Generate(fakeRand);

            Assert.That(letters.Count, Is.EqualTo(9));
        }

        [Test]
        public void Generate_a_string_with_min_4_vowels_and_5_consonants()
        {
            string letters = LetterGenerator.Generate(fakeRand);

            var vowelsInResult = letters.Where(vowels.Contains).ToList();
            var consonantsInResult = letters.Where(consonants.Contains).ToList();

            Assert.That(vowelsInResult.Count, Is.EqualTo(4));
            Assert.That(consonantsInResult.Count, Is.EqualTo(5));
        }

        [Test]
        public void Shuffle_letters()
        {
            var fakeShuffleRand = A.Fake<Random>();
            A.CallTo(() => fakeShuffleRand.Next(0, 9)).Returns(5);
            A.CallTo(() => fakeShuffleRand.Next(0, 8)).Returns(1);
            A.CallTo(() => fakeShuffleRand.Next(0, 7)).Returns(6);
            A.CallTo(() => fakeShuffleRand.Next(0, 6)).Returns(5);
            A.CallTo(() => fakeShuffleRand.Next(0, 5)).Returns(5);
            A.CallTo(() => fakeShuffleRand.Next(0, 4)).Returns(0);
            A.CallTo(() => fakeShuffleRand.Next(0, 3)).Returns(1);
            A.CallTo(() => fakeShuffleRand.Next(0, 2)).Returns(2);
            A.CallTo(() => fakeShuffleRand.Next(0, 1)).Returns(1);
            var lettersToBeShuffled = new char[] { 'U','E','I','A','H','W','Y','W','C' };
            LetterGenerator.Shuffle(fakeShuffleRand, lettersToBeShuffled);

            Assert.That(lettersToBeShuffled, Is.EquivalentTo(new char[] { 'A', 'W', 'I', 'U', 'C', 'H', 'Y', 'E', 'W' })); 
        }
    }
}
