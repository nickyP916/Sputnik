namespace Sputnik.Tests
{
    internal class WordValidatorShould
    {
        [TestCase("HQQLI")]
        [TestCase("AAI%")]
        [TestCase("SAD")]
        [TestCase("")]
        [TestCase(" ")]
        public async Task Negatively_validate_words_which_contain_illegal_letters(string wordToCheck)
        {
            var isValid = await WordValidator.Validate(wordToCheck, "DNMAAIOP");

            Assert.That(isValid, Is.EqualTo(false));
        }

        [TestCase("DNMA")]
        [TestCase("AAI")]
        [TestCase("PAMA")]
        [TestCase("MAIO")]
        public async Task Negatively_validate_words_which_dont_exist_in_dictionary(string wordToCheck)
        {
            var isValid = await WordValidator.Validate(wordToCheck, "DNMAAIOP");

            Assert.That(isValid, Is.EqualTo(false));
        }

        [TestCase("MAP")]
        [TestCase("NAP")]
        [TestCase("PAD")]
        public async Task Positively_validate_words_with_legal_letters_and_that_exist_in_dictionary(string wordToCheck)
        {
            var isValid = await WordValidator.Validate(wordToCheck, "DNMAAIOP");

            Assert.That(isValid, Is.EqualTo(true));
        }

        [TestCase("A")]
        [TestCase("I")]
        [TestCase("STREAMINGS")]
        public async Task Must_only_accept_words_with_min_2_and_max_number_of_letters(string wordToCheck)
        {
            var isValid = await WordValidator.Validate(wordToCheck, "NSTRAEMIG");

            Assert.That(isValid, Is.EqualTo(false));
        }

        private static IEnumerable<string> WordsToCheck()
        {
            yield return "MAX";
            yield return "MAD";
            yield return "HQQLI";
            yield return "";
            yield return "DAMN";
            yield return "PAD";
            yield return "AAI";
        }
    }
}
