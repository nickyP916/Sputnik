namespace Sputnik
{
    static internal class Game
    {
        public static List<IMiniGame> MiniGames { get; set; }

        public static async Task Play()
        {
            MiniGames = [new NumberCrunch(), new WordHunt()];

            Console.WriteLine("Select your gameChoice");
            for (int i = 0; i < MiniGames.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {MiniGames[i]}");
            }

            if (int.TryParse(Console.ReadLine(), out var gameChoice)
                && gameChoice > 0 && gameChoice <= MiniGames.Count)
            {
                var game = MiniGames[gameChoice - 1];
                await game.Play();
            }
            else
            {
                Console.WriteLine($"Please select a game between 1 and {MiniGames.Count}");
            };

        }
    }
}
