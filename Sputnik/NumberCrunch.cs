using Sputnik.Services;

namespace Sputnik
{
    internal class NumberCrunch(IInputListener inputListener) : IMiniGame
    {
        public string Name => "Number Crunch";

        private readonly int _maxRounds = 10;

        private int _roundsWon = 0;

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            while (_roundsWon < _maxRounds && !token.IsCancellationRequested)
            {
                var number1 = new Random().Next(100);
                var number2 = new Random().Next(100);

                Console.WriteLine($"What is {number1} + {number2}?");

                var cts = new CancellationTokenSource();
                var inputTask = Task.Run(() => ListenForNumberInput(cts.Token), token);
                if (await Task.WhenAny(inputTask, Task.Delay(5000)) == inputTask)
                {
                    if(!token.IsCancellationRequested)
                    {
                        if (inputTask.Result != null && number1 + number2 == (int)inputTask.Result)
                        {
                            Console.WriteLine("Correct!");
                            _roundsWon++;
                            continue;
                        }
                        Console.WriteLine("Wrong!");
                        break;
                    }
                }
                else
                {
                    var position = Console.CursorTop;
                    Console.SetCursorPosition(0, position + 1);
                    Console.WriteLine("Time's up!");
                    cts.Cancel();
                    break;
                }
            }
            var totalScore = NumberCrunchScoreCalculator.CalculateScore(_roundsWon);
            Console.WriteLine($"Total Score: {totalScore}");
        }

        private int? ListenForNumberInput(CancellationToken token)
        {
            var input = inputListener.ListenForInput(token);
            if (int.TryParse(input, out var answer))
                return answer;
            else
                return null;
        }
    }
}
