using Microsoft.Extensions.Options;

namespace Sputnik.Services
{
    public class PlayMechanicsService : IPlayMechanicsService
    {
        private readonly int _maxRounds;
        private readonly int _timeout;

        private int _roundsWon = 0;

        private readonly GameSettings _settings;

        public PlayMechanicsService()
        {
            //TODO get IOptions working - neeed Microsoft.Extensions.DependencyInjection and Microsoft.Extensions.Options.ConfigurationExtensions
            //_settings = gameSettings.Value;
            //_maxRounds = _settings.DefaultMaxRounds;
            //_timeout = _settings.DefaultTimeout;

            _maxRounds = 10;
            _timeout = 10000;
        }
        public async Task<int> PlayToMaxRounds(GameInstructions instructions, CancellationToken token)
        {
            var inputListener = instructions.InputListener;
            var logic = instructions.ScoreLogic;
            var setup = instructions.Setup;

            Console.WriteLine("Max rounds is " + _maxRounds);

            while (_roundsWon < _maxRounds && !token.IsCancellationRequested)
            {
                var inputs = setup.Invoke();

                var cts = new CancellationTokenSource();
                var combinedTcs = CancellationTokenSource.CreateLinkedTokenSource(token, cts.Token);
                var inputTask = Task.Run(() => inputListener.ListenForInput(combinedTcs.Token));

                if (await Task.WhenAny(inputTask, Task.Delay(_timeout)) == inputTask)
                {
                    if (!token.IsCancellationRequested)
                    {
                        if (inputTask.Result != null && await logic(inputs,inputTask.Result))
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
                    await cts.CancelAsync();
                    var position = Console.CursorTop;
                    Console.SetCursorPosition(0, position + 1);

                    Console.WriteLine("Time's up!");
                    break;
                }
            }

            return _roundsWon;
        }
    }
}
