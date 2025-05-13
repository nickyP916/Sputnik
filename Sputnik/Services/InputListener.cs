

namespace Sputnik.Services
{
    internal class InputListener : IInputListener
    {
        public string? ListenForInput(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var input = Console.ReadLine();
                    return input;
                }
            }
            throw new OperationCanceledException();
        }
    }
}