namespace Sputnik.Tests
{
    internal class StringFinderShould
    {
        [TestCase("DOVGJEOWPEUID", "dog")]
        [TestCase("DOVGJEOWPEUID", "POWdd")]
        [TestCase("DOVGJEOWPEUID", "DOVE")]
        public void Return_true_if_given_word_exists_in_letters(string letters, string toFind)
        {
            var exists = StringFinder.Find(letters, toFind);

            Assert.That(exists, Is.True);
        }

        [TestCase("DOVGJEOWPEUID", "DOVGJEOWPEUIDads")]
        [TestCase("DOVGJEOWPEUID", "JEZ")]
        [TestCase("DOVGJEOWPEUID", "")]
        public void Return_false_if_given_word_does_not_exist_in_letters(string letters, string toFind)
        {
            var exists = StringFinder.Find(letters, toFind);

            Assert.That(exists, Is.False);
        }
    }
}
