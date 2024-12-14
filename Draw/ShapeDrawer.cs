using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Draw
{
    public static class ShapeDrawer
    {
        public static void DrawShape(int x, int y, char shape, ConsoleColor colour)
        {
            Console.SetCursorPosition(x, y);
            var position = Console.CursorTop;
            Console.SetCursorPosition(0, position + 1);
            Console.ForegroundColor = colour;
            Console.Write(shape);
            Console.ResetColor();
        }

        public static void DrawShapeSequence(List<char> shapes, List<ConsoleColor> colours)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                char shape = shapes[i];
                foreach (var colour in colours)
                {
                    Console.ForegroundColor = colour;
                    Console.Write(shape);
                    Console.ResetColor();
                }
            }
        }

        public static void DrawShapeSequence(int shapesPerSide, int numGrids)
        {
            var shape = '\u25A0';
            var colour = ConsoleColor.Blue;
            var markColour = ConsoleColor.Green;
            var gapSize = 1;
            var minMarks = shapesPerSide;
            var maxMarks = shapesPerSide * 2;

            bool[,] markPositions = GenerateMarkPositions(shapesPerSide, minMarks, maxMarks);

            var gridTop = Console.CursorTop;
            for (int g = 0; g < numGrids; g++)
            {
                int gridLeft = (shapesPerSide + gapSize) * g;
                Console.SetCursorPosition(gridLeft, gridTop);

                for (int s = 0; s < shapesPerSide; s++)
                {
                    Console.SetCursorPosition(gridLeft, gridTop + s);

                    for (int i = 0; i < shapesPerSide; i++)
                    {
                        if (markPositions[i, s])
                            Console.ForegroundColor = markColour;
                        else
                            Console.ForegroundColor = colour;

                        Console.Write(shape);
                        Console.ResetColor();
                    }


                }
            }

            Console.SetCursorPosition(0, Console.CursorTop + 1);
        }

        private static bool[,] GenerateMarkPositions(int shapesPerSide, int minMarks, int maxMarks)
        {
            bool[,] markPositions = new bool[shapesPerSide, shapesPerSide];

            var numMarks = new Random().Next(minMarks, maxMarks);

            for (int i = 0; i < numMarks; i++)
            {
                var xPos = new Random().Next(shapesPerSide);
                var yPos = new Random().Next(shapesPerSide);

                markPositions[xPos, yPos] = true;
            }

            return markPositions;
        }
    }
}