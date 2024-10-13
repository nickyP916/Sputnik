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
