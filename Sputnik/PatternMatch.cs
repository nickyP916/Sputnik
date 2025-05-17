using Sputnik.Services;

namespace Sputnik
{
    internal class PatternMatch(IShapeDrawer shapeDrawer, IPlayMechanicsService playMechanicsService) : IMiniGame
    {
        public string Name => "Pattern Match";

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            var instructions = new GameInstructions
            {
                ScoreLogic = ScoreLogic,
                Setup = Setup
            };

            var roundsWon = await playMechanicsService.PlayToMaxRounds(instructions, token);

            var totalScore = RoundsScoreCalculator.CalculateScore(roundsWon);
            Console.WriteLine($"Total Score: {totalScore}");
        }

        private int[] Setup()
        {
            var matchingIndexes = shapeDrawer.DrawShapeSequence(3, 4);
            Console.WriteLine("Which two are the same?");

            return matchingIndexes;
        }

        private Task<bool> ScoreLogic(object inputs, string? guess)
        {
            var matchingIndexes = (int[])inputs;

            var numbersGuess = ConvertStringInputToNumbers(guess);
            if (numbersGuess == null)
                return Task.FromResult(false);

            var indexesGuess = numbersGuess.Select(x => x - 1).ToArray();
            return Task.FromResult( indexesGuess.Order().SequenceEqual(matchingIndexes.Order()));
        }

        public int[]? ConvertStringInputToNumbers(string? input)
        {
            var results = new int[2];
            if (input!= null && input.Length == 2)
            {
                for (int i = 0; i < input.Length; i++)
                {
                    char item = input[i];
                    if (int.TryParse(item.ToString(), out var result))
                    {
                        results[i] = result;
                    }
                    else
                    {
                        return null;
                    }
                }

                return results;
            }

            return null;
        }

    }
}
