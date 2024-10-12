namespace Sputnik
{
    internal class NumberCrunch : IMiniGame
    {
        public string Name => "Number Crunch";

        private readonly int _maxRounds = 10;

        private int _roundsWon;

        public void Play()
        {
            Console.WriteLine($"You are playing {Name}!");

            while(_roundsWon < _maxRounds)
            {
                var number1 = new Random().Next(100);
                var number2 = new Random().Next(100);

                Console.WriteLine($"What is {number1} + {number2}?");

                if (int.TryParse(Console.ReadLine(), out int answer))
                {
                    if (number1 + number2 == answer)
                    {
                        Console.WriteLine("Correct!");
                        _roundsWon++;
                        continue;
                    }
                    Console.WriteLine("Wrong!");
                    break;
                }
            }
            var totalScore = ScoreCalculator.CalculateScore(_roundsWon);
            Console.WriteLine($"You scored {totalScore} points!");
        }
    }
}
