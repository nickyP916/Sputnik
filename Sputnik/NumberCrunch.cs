using Sputnik.Services;

namespace Sputnik
{
    internal class NumberCrunch(IPlayMechanicsService playMechanics) : IMiniGame
    {
        public string Name => "Number Crunch";
        private const int RoundsWon = 0;

        public async Task Play(CancellationToken token)
        {
            var instructions = new GameInstructions
            {
                ScoreLogic = ScoreLogic,
                Setup = Setup
            };
            await playMechanics.PlayToMaxRounds(instructions, token);

            var totalScore = RoundsScoreCalculator.CalculateScore(RoundsWon);
            Console.WriteLine($"Total Score: {totalScore}");
        }

        private static Tuple<int, int> Setup()
        {
            var number1 = new Random().Next(100);
            var number2 = new Random().Next(100);

            Console.WriteLine($"What is {number1} + {number2}?");

            return new Tuple<int, int>(number1, number2);
        }

        private static Task<bool> ScoreLogic(object inputs, string? guess)
        {
            if (int.TryParse(guess, out var numberGuess))
            {
                var numberInputs = (Tuple<int, int>)inputs;
                return Task.FromResult(numberInputs.Item1 + numberInputs.Item2 == (int)numberGuess);
            }
            else
            {
                return Task.FromResult(false);
            }
                
        }
    }
}
