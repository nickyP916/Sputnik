namespace Sputnik
{
    internal class NumberCrunch : IMiniGame
    {
        public string Name => "Number Crunch";

        private readonly int _maxRounds = 10;

        private int _roundsWon = 0;

        private bool _timeLeft = true;
        private bool _playing => _roundsWon < _maxRounds;

        public async Task Play()
        {
            Console.WriteLine($"You are playing {Name}!");

            while (_roundsWon < _maxRounds)
            {
                var number1 = new Random().Next(100);
                var number2 = new Random().Next(100);

                Console.WriteLine($"What is {number1} + {number2}?");

                var inputTask = Task.Run(Console.ReadLine);
                if (await Task.WhenAny(inputTask, Task.Delay(5000)) == inputTask)
                {
                    if (int.TryParse(inputTask.Result, out int answer))
                    {
                        if (number1 + number2 == answer)
                        {
                            Console.WriteLine("Correct!");
                            _roundsWon++;
                            continue;
                        }
                    }
                    Console.WriteLine("Wrong!");
                    break;
                }
                else
                {
                    Console.WriteLine("Time's up!");
                    break;
                }

            }
            var totalScore = ScoreCalculator.CalculateScore(_roundsWon);
            Console.WriteLine($"Total Score: {totalScore}");
        }

    }
}
