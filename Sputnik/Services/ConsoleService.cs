namespace Sputnik.Services
{
    public class ConsoleService : IConsoleService
    {
        public int CursorTop => Console.CursorTop;
        public int CursorLeft => Console.CursorLeft;
        public ConsoleColor ForegroundColor { set => Console.ForegroundColor = value; }

        public void ResetColor()
        {
            Console.ResetColor();
        }

        public void SetCursorPosition(int left, int top)
        {
            var bufferHeight = Console.BufferHeight;
            var topDiff = top - Console.CursorTop;

            if (top >= bufferHeight)
            {
                Console.MoveBufferArea(0, 1, Console.BufferWidth, Console.WindowHeight - 1, 0, 0);
                Console.SetCursorPosition(left, top - topDiff);

            }
            else
            {
                Console.SetCursorPosition(left, top);
            }
        }

        public void Write(char value)
        {
            Console.Write(value);
        }
    }
}
