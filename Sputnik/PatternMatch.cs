using Draw;
using Sputnik.Services;

namespace Sputnik
{
    internal class PatternMatch(IInputListener inputListener) : IMiniGame
    {
        public string Name => "Pattern Match";

        public async Task Play(CancellationToken token)
        {
            Console.WriteLine($"You are playing {Name}!");

            var timeoutTask = Task.Delay(5000);
            while(!timeoutTask.IsCompleted && !token.IsCancellationRequested)
            {
                ShapeDrawer.DrawShapeSequence(3, 4);

                Console.WriteLine("Which two are the same?");

                var inputTask = Task.Run(() => inputListener.ListenForInput(token), token);
                var completedTask = await Task.WhenAny(timeoutTask, inputTask);

                if (completedTask == inputTask)
                {
                    var response = await inputTask;
                    Console.WriteLine("you selected " + response);
                }
                else
                {
                    var position = Console.CursorTop;
                    Console.SetCursorPosition(0, position + 1);
                    Console.WriteLine("Time's up!");
                    break;
                }
            }

        }

    }
}
