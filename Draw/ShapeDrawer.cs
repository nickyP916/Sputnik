namespace Draw
{
    public static class ShapeDrawer
    {
        public static int[] DrawShapeSequence(int gridWidth, int numGrids)
        {
            var shape = '\u25A0';
            var colour = ConsoleColor.Blue;
            var markColour = ConsoleColor.Green;
            var gridGapSize = 2;
            var xGapSize = 1;
            var gapsPerGrid = gridWidth * xGapSize;
            var gridHeight = gridWidth;
            var minMarks = gridHeight;
            var maxMarks = gridWidth * 2;

            bool[,] markPositionsPerGrid = GenerateMarkPositionsPerGrid(gridWidth, gridHeight, minMarks, maxMarks);
            int[] matchingGridIndexes = GenerateIndexesFor2MatchingGrids(numGrids);

            int numUniqueGrids = numGrids - matchingGridIndexes.Length;
            var uniqueGrids = GenerateUniqueGrids(gridWidth, gridHeight, minMarks, maxMarks, numUniqueGrids);
            List<bool[,]> gridList = CombineUniqueAndMatchingGrids(markPositionsPerGrid, matchingGridIndexes, uniqueGrids);

            var gridTop = Console.CursorTop;
            for (int g = 0; g < numGrids; g++)
            {
                int gridLeft = (gridWidth + gapsPerGrid + gridGapSize) * g;
                Console.SetCursorPosition(gridLeft, gridTop);

                for (int y = 0; y < gridHeight; y++)
                {
                    Console.SetCursorPosition(gridLeft, gridTop + y); //Hitting System Argument exc, buffer size? related to console size perhaps?

                    for (int x = 0; x < gridWidth; x++)
                    {
                        var positions = gridList.ElementAt(g);
                        if (positions[x, y])
                            Console.ForegroundColor = markColour;
                        else
                            Console.ForegroundColor = colour;

                        Console.Write(shape);
                        Console.ResetColor();
                        Console.SetCursorPosition(Console.CursorLeft + xGapSize, Console.CursorTop);
                    }
                }
            }

            Console.SetCursorPosition(0, Console.CursorTop + 1);

            return matchingGridIndexes;
        }

        public static List<bool[,]> CombineUniqueAndMatchingGrids(bool[,] markPositionsForMatchingGrids, int[] matchingGridIndexes, HashSet<bool[,]> uniqueGrids)
        {
            matchingGridIndexes = matchingGridIndexes.OrderBy(x => x).ToArray();
            var gridList = uniqueGrids.ToList();
            for (int i = 0; i < matchingGridIndexes.Length; i++)
            {
                int matchingIndex = matchingGridIndexes[i];
                gridList.Insert(matchingIndex, markPositionsForMatchingGrids);
            }

            return gridList;
        }

        private static int[] GenerateIndexesFor2MatchingGrids(int numGrids)
        {
            var grid1Index = new Random().Next(numGrids - 1);
            int grid2Index = new Random().Next(numGrids - 1);
            while (grid2Index == grid1Index)
            {
                grid2Index = new Random().Next(numGrids - 1);
            }
            var matchingGridIndexes = new int[2] { grid1Index, grid2Index };
            matchingGridIndexes.Order();
            return matchingGridIndexes;
        }

        public static HashSet<bool[,]> GenerateUniqueGrids(int gridWidth, int gridHeight, int minMarks, int maxMarks, int numGrids)
        {
            var uniqueGrids = new HashSet<bool[,]>();

            while(uniqueGrids.Count < numGrids)
            {
                for (int i = 0; i < numGrids; i++)
                {
                    uniqueGrids.Add(GenerateMarkPositionsPerGrid(gridWidth, gridHeight, minMarks, maxMarks));
                }
            }

            return uniqueGrids;
        }

        public static bool[,] GenerateMarkPositionsPerGrid(int gridWidth,int gridHeight, int minMarks, int maxMarks)
        {
            bool[,] markPositions = new bool[gridWidth, gridHeight];

            var numMarks = new Random().Next(minMarks, maxMarks);

            for (int i = 0; i < numMarks; i++)
            {
                var xPos = new Random().Next(gridWidth);
                var yPos = new Random().Next(gridHeight);

                markPositions[xPos, yPos] = true;
            }

            return markPositions;
        }
    }
}