
namespace Sputnik.Services
{
    public interface IConsoleService
    {
        int CursorTop { get; }
        int CursorLeft { get; }
        ConsoleColor ForegroundColor { set; }

        void SetCursorPosition(int left, int top);
        void Write(char value);
        void ResetColor();
    }
}
