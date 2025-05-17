using Sputnik.Services;

namespace Sputnik
{
    internal class NumberCrunch(IPlayMechanicsService playMechanicsService) : IMiniGame
    {
        public string Name => "Number Crunch";

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
