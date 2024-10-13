namespace Sputnik
{
    internal class NumberCrunch : IMiniGame
    {
        public string Name => "Number Crunch";

        private readonly int _maxRounds = 10;

        private int _roundsWon = 0;

        private bool _timeLeft = true;
        private bool _playing => _roundsWon < _maxRounds;

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            while (_roundsWon < _maxRounds && !token.IsCancellationRequested)
            {
                var number1 = new Random().Next(100);
                var number2 = new Random().Next(100);

                Console.WriteLine($"What is {number1} + {number2}?");

                var inputTask = Task.Run(() => ListenForInput(token), token);
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
                    break;
                }
            }
            var totalScore = ScoreCalculator.CalculateScore(_roundsWon);
            Console.WriteLine($"Total Score: {totalScore}");
        }

        private static int? ListenForInput(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadLine();
                    if (int.TryParse(input, out var answer))
                        return answer;
                    else
                        return null;
                }

            }
            throw new OperationCanceledException();
        }
    }
}
