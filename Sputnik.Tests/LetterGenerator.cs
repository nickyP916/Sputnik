using FakeItEasy;


namespace Sputnik.Tests
{
    internal class LetterGenerator
    {
        internal static string Generate(Random random)
        {
            var lengthOfResult = 9;
            var consonants = "BCDFGHJKLMNPQRSTVWXYZ";
            var vowels = "AEIOU";
            var minVowels = 4;
            var minConsonants = 5;
            var strChars = new char[lengthOfResult];
            var vowelChars = new char[minVowels];
            var consonantChars = new char[minConsonants];

            for (int i = 0; i < minVowels; i++)
            {
                vowelChars[i] = vowels[random.Next(vowels.Length)];
            }

            for (int i = 0; i < minConsonants; i++)
            {
                consonantChars[i] = consonants[random.Next(consonants.Length)];
            }

            char[] letters = vowelChars.Concat(consonantChars).ToArray();

            return new string(Shuffle(new Random(), letters));
        }

        internal static char[] Shuffle(Random random, char[] letters)
        {
            for (int i = letters.Length - 1; i > 0; i--)
            {
                var temp = letters[i];
                var swapIndex = random.Next(0, i + 1);
                letters[i] = letters[swapIndex];
                letters[swapIndex] = temp;
            }

            return letters;
        }
    }
}
