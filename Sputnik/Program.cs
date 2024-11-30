namespace Sputnik
{
    internal class Program
    {
        private static CancellationTokenSource _cts = new();

        static async Task Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;

            try
            {
                await Game.Play(_cts.Token);
            }
            catch (OperationCanceledException)
            {
                var position = Console.CursorTop;
                Console.SetCursorPosition(0, position + 1);
                Console.WriteLine("Cancelled");
            }
            finally
            {
                Console.WriteLine("Exiting");
            }
            
        }

        private static void Console_CancelKeyPress(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            _cts.Cancel();
        }
    }
}
