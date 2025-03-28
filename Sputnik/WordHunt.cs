﻿using Sputnik.Services;

namespace Sputnik
{
    internal class WordHunt(IInputListener inputListener) : IMiniGame
    {
        public string Name => "Word Hunt";

        private const int minWordLength = 2;

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");
            List<string> words = new();
            var letters = LetterGenerator.Generate(new Random());
            Console.WriteLine("Enter as many words as you can from the word:");
            Console.WriteLine(letters);
            var timeoutTask = Task.Delay(5000);
            while(!timeoutTask.IsCompleted && !token.IsCancellationRequested) 
            {
                var cts = new CancellationTokenSource();
                var combinedTcs = CancellationTokenSource.CreateLinkedTokenSource(token, cts.Token);
                var inputTask = Task.Run(() => inputListener.ListenForInput(combinedTcs.Token));
                var completedTask = await Task.WhenAny(timeoutTask, inputTask);

                if (completedTask == inputTask)
                {
                    var word = await inputTask;
                    if(word != null)
                    {
                        var isValid = await WordValidator.Validate(word, letters);
                        if (isValid)
                        {
                            words.Add(word);
                            var points = WordHuntScoreCalculator.Calculate(word, minWordLength, letters.Length);
                            Console.WriteLine(points + " points!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid word");
                        }
                    }
                }
                else
                {
                    cts.Cancel();  

                    var position = Console.CursorTop;
                    Console.SetCursorPosition(0, position + 1);

                    Console.WriteLine("Time's up!");
                    break;
                }
                
            }
            var totalScore = WordHuntScoreCalculator.GetFinalScore();
            Console.WriteLine($"Total Score: {totalScore}");
        }
    }
}
