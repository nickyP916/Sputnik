using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sputnik.Tests
{
    internal class WordHuntScoreCalculatorShould
    {
        [TestCase("MAP", 3)]
        [TestCase("A", 1)]
        [TestCase("DYNAMO", 6)]
        [TestCase("PAD", 3)]
        public void Give_a_point_for_every_letter_in_the_word(string word, int expectedScore)
        {
            var letters = "NDOYAMP";
            var score = WordHuntScoreCalculator.Calculate(word);
        }
    }

    internal class WordHuntScoreCalculator
    {
        internal static object Calculate(string word)
        {
            return word.Length;
        }
    }
}
