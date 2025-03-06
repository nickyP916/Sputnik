namespace Sputnik.Services
{
    public class ConsoleService : IConsoleService
    {
        public int CursorTop { get => Console.CursorTop; }
        public int CursorLeft { get => Console.CursorLeft; }
        public ConsoleColor ForegroundColor { set => Console.ForegroundColor = value; }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public void Write(char value)
        {
            Console.Write(value);
        }
    }
}
