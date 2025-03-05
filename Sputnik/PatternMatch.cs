using Draw;
using Sputnik.Services;

namespace Sputnik
{
    internal class PatternMatch(IInputListener inputListener) : IMiniGame
    {
        public string Name => "Pattern Match";

        private int _roundsWon = 0;

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            var timeoutTask = Task.Delay(15000);
            
            while (!timeoutTask.IsCompleted && !token.IsCancellationRequested)
            {
                var matchingIndexes = ShapeDrawer.DrawShapeSequence(3, 4);
                Console.WriteLine("Which two are the same?");
                var cts = new CancellationTokenSource();
                var combinedTcs = CancellationTokenSource.CreateLinkedTokenSource(token, cts.Token);
                var inputTask = Task.Run(() => ListenForNumberInput(combinedTcs.Token));
                var completedTask = await Task.WhenAny(timeoutTask, inputTask);

                if (completedTask == inputTask)
                {
                    var response = await inputTask;
                    if(response != null)
                    {
                        var indexResponse = response.Select(x => x - 1).ToArray();
                        if (indexResponse!.Order().SequenceEqual(matchingIndexes.Order()))
                        {
                            Console.WriteLine("Correct!");
                            _roundsWon++;
    }
                        else
                            Console.WriteLine("Incorrect!");
                    }
                    else
                        Console.WriteLine("enter two numbers");
                }
                else
                {
                    var position = Console.CursorTop;
                    Console.SetCursorPosition(0, position + 1);
                    Console.WriteLine("Time's up!");
                    cts.Cancel();
                    //var totalScore = NumberCrunchScoreCalculator.CalculateScore(_roundsWon);
                    //Console.WriteLine($"Total Score: {totalScore}");
                    break;
                }
            }
        }
        public int[]? ListenForNumberInput(CancellationToken token)
        {
            var input = inputListener.ListenForInput(token);
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
            else
                return null;
        }

    }
}
