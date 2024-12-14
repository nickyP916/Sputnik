using System.Data;
using System.Net.Security;
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
            var gapSize = 2;
            var minMarks = shapesPerSide;
            var maxMarks = shapesPerSide * 2;

            bool[,] markPositions = GenerateMarkPositions(shapesPerSide, minMarks, maxMarks);
            var grid1Index = new Random().Next(numGrids - 1);
            int grid2Index = new Random().Next(numGrids - 1);
            while (grid2Index == grid1Index)
            {
                grid2Index = new Random().Next(numGrids - 1);
            }
            var matchingGridIndexes = new Tuple<int, int>( grid1Index, grid2Index );

            int numUniqueGrids = numGrids - 2;
            var uniqueGrids = GenerateUniqueGrids(shapesPerSide, minMarks, maxMarks, numUniqueGrids, markPositions);

            var gridList = uniqueGrids.ToList();
            gridList.Insert(matchingGridIndexes.Item1, markPositions);
            gridList.Insert(matchingGridIndexes.Item2, markPositions);

            var gridTop = Console.CursorTop;
            for (int g = 0; g < numGrids; g++)
            {
                int gridLeft = (shapesPerSide + gapSize) * g;
                Console.SetCursorPosition(gridLeft, gridTop);

                for (int y = 0; y < shapesPerSide; y++)
                {
                    Console.SetCursorPosition(gridLeft, gridTop + y); //Hitting System Argument exc, buffer size? related to console size perhaps?

                    for (int x = 0; x < shapesPerSide; x++)
                    {
                        var positions = gridList.ElementAt(g);
                        if (positions[x, y])
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

        private static HashSet<bool[,]> GenerateUniqueGrids(int shapesPerSide, int minMarks, int maxMarks, int numGrids, bool[,] marksToOffset)
        {
            var uniqueGrids = new HashSet<bool[,]>();

            while(uniqueGrids.Count < numGrids)
            {
                for (int i = 0; i < numGrids; i++)
                {
                    uniqueGrids.Add(GenerateMarkPositions(shapesPerSide, minMarks, maxMarks));
                }
            }

            return uniqueGrids;
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