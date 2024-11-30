

namespace Draw
{
    public static class ShapeDrawer
    {
        public static void DrawShape(int x, int y, char shape, ConsoleColor colour)
        {
            //Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colour;
            Console.Write(shape);
            Console.ResetColor();

        }
    }
}