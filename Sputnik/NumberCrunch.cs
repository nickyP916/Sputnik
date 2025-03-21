using Newtonsoft.Json.Linq;
using Sputnik.Services;

namespace Sputnik
{
    public record GameInstructions
    {
        public IInputListener InputListener { get; set; }
        public Func<int,bool> ScoreLogic { get; set; }
        public Action Setup { get; set; }
    }

    internal class NumberCrunch(IInputListener inputListener, IPlayMechanicsService playMechanics) : IMiniGame
    {
        public string Name => "Number Crunch";

        private int _roundsWon = 0;

        public async Task Play(CancellationToken token)
        {
            var instructions = new GameInstructions()
            {
                InputListener = inputListener,
                ScoreLogic = ScoreLogic,
                Setup = Setup
            };
            await playMechanics.PlayToMaxRounds(instructions, token);

            var totalScore = RoundsScoreCalculator.CalculateScore(_roundsWon);
            Console.WriteLine($"Total Score: {totalScore}");
        }

        private bool ScoreLogic(int guess)
        {
            return true;
        }

        private void Setup()
        {
            var number1 = new Random().Next(100);
            var number2 = new Random().Next(100);

            Console.WriteLine($"What is {number1} + {number2}?");
        }

        //public async Task Play(CancellationToken token)
        //{
        //    Console.WriteLine($"You are playing {Name}!");

        //    while (_roundsWon < _maxRounds && !token.IsCancellationRequested)
        //    {
        //        var number1 = new Random().Next(100);
        //        var number2 = new Random().Next(100);

        //        Console.WriteLine($"What is {number1} + {number2}?");

        //        var cts = new CancellationTokenSource();
        //        var combinedTcs = CancellationTokenSource.CreateLinkedTokenSource(token, cts.Token);
        //        var inputTask = Task.Run(() => inputListener.ListenForNumberInput(combinedTcs.Token));
        //        if (await Task.WhenAny(inputTask, Task.Delay(5000)) == inputTask)
        //        {
        //            if (!token.IsCancellationRequested)
        //            {
        //                if (inputTask.Result != null && number1 + number2 == (int)inputTask.Result)
        //                {
        //                    Console.WriteLine("Correct!");
        //                    _roundsWon++;
        //                    continue;
        //                }
        //                Console.WriteLine("Wrong!");
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            cts.Cancel();
        //            var position = Console.CursorTop;
        //            Console.SetCursorPosition(0, position + 1);

        //            Console.WriteLine("Time's up!");
        //            break;
        //        }
        //    }
        //    var totalScore = RoundsScoreCalculator.CalculateScore(_roundsWon);
        //    Console.WriteLine($"Total Score: {totalScore}");
        //}
    }
}
