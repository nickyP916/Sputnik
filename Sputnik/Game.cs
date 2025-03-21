﻿using Microsoft.Extensions.DependencyInjection;

namespace Sputnik
{
    internal class Game(IServiceProvider serviceProvider)
    {
        public required List<IMiniGame> MiniGames { get; set; }

        private bool awaitingGameChoice;
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public async Task Play(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                MiniGames = new List<IMiniGame>
                {
                    serviceProvider.GetRequiredService<NumberCrunch>(),
                    serviceProvider.GetRequiredService<WordHunt>(),
                    serviceProvider.GetRequiredService <PatternMatch>()
                };

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
                        if (gameChoice != null && gameChoice > 0 && gameChoice <= MiniGames.Count)
                        {
                            awaitingGameChoice = false;
                            var game = MiniGames[(int)gameChoice - 1];
                            await game.Play(token);
                        }
                        else
                        {
                            Console.WriteLine($"Please select a game between 1 and {MiniGames.Count}");
                        };
                    }
                }
            }

        }

        private static int? ListenForInput(CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out var gameChoice))
                        return gameChoice;
                    else
                        return null;
                }

            }
            throw new OperationCanceledException();
        }
    }
}
