using Sputnik.Services;

namespace Sputnik
{
    internal class WordHunt(IPlayMechanicsService playMechanicsService) : IMiniGame
    {
        public string Name => "Word Hunt";

        private const int MinWordLength = 2;

        public async Task Play(CancellationToken token)
        {
            var instructions = new GameInstructions
            {
                ScoreLogic = ScoreLogic,
                Setup = Setup
            };
            await playMechanicsService.PlayToMaxRounds(instructions, token);


            var totalScore = WordHuntScoreCalculator.GetFinalScore();
            Console.WriteLine($"Total Score: {totalScore}");
        }

        private static string Setup()
        {
            var letters = LetterGenerator.Generate(new Random());
            Console.WriteLine("Enter as many words as you can from the letters:");
            Console.WriteLine(letters);

            return letters;
        }

        private async Task<bool> ScoreLogic(object input, string? guess)
        {
            var letters = (string)input;

            if (guess == null)
                return false;

            var word = guess;

            var isValid = await WordValidator.Validate(word, letters);
            if (isValid)
            {
                var points = WordHuntScoreCalculator.Calculate(word, MinWordLength, letters.Length);
                Console.WriteLine(points + " points!");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid word");
                return false;
            }
        }
    }
}
