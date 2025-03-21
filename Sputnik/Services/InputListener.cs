

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

        public int? ListenForNumberInput(CancellationToken token)
        {
            var input = ListenForInput(token);
            if (int.TryParse(input, out var answer))
                return answer;
            else
                return null;
        }
    }
}