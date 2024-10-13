namespace Sputnik
{
    static internal class Game
    {
        public static List<IMiniGame> MiniGames { get; set; }

        private static bool awaitingGameChoice;
        public static async Task Play(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                MiniGames = [new NumberCrunch(), new WordHunt()];

                Console.WriteLine("Select your game");
                for (int i = 0; i < MiniGames.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {MiniGames[i]}");
                }

                awaitingGameChoice = true;
                while (awaitingGameChoice && !token.IsCancellationRequested)
                {
                    var gameChoice = await Task.Run(() => ListenForInput(token), token);
                    if (!token.IsCancellationRequested)
                    {
                        if (gameChoice != null && (int)gameChoice > 0 && (int)gameChoice <= MiniGames.Count)
                        {
                            awaitingGameChoice = false;
                            var game = MiniGames[(int)gameChoice - 1];
                            await game.Play();
                        }
                        else
                        {
                            Console.WriteLine($"Please select a game between 1 and {MiniGames.Count}");
                        };
                    }
                }
            }

        }

        private static async Task<int?> ListenForInput(CancellationToken token)
        {
            
            while (!token.IsCancellationRequested)
            {
                if(Console.KeyAvailable)
                {
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out var gameChoice))
                        return gameChoice;
                }
                
            }
            throw new OperationCanceledException();
        }
    }
}
