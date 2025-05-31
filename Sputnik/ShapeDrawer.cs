using Sputnik.Services;

namespace Sputnik
{
    public class ShapeDrawer(IConsoleService consoleService) : IShapeDrawer
    {
        static readonly char shape = '\u25A0';
        static readonly ConsoleColor colour = ConsoleColor.Blue;
        static readonly ConsoleColor markColour = ConsoleColor.Green;
        static readonly int gridGapSize = 2;
        static readonly int xGapSize = 1;
        private readonly IConsoleService consoleService = consoleService;

        public int[] DrawShapeSequence(int gridWidth, int numGrids)
        {
            var gridHeight = gridWidth;
            var minMarks = gridHeight;
            var maxMarks = gridWidth * 2;

            bool[,] markPositionsPerGrid = GenerateMarkPositionsPerGrid(gridWidth, gridHeight, minMarks, maxMarks);
            int[] matchingGridIndexes = GenerateIndexesFor2MatchingGrids(numGrids);

            int numUniqueGrids = numGrids - matchingGridIndexes.Count();
            var uniqueGrids = GenerateUniqueGrids(gridWidth, gridHeight, minMarks, maxMarks, numUniqueGrids);
            List<bool[,]> gridList = CombineUniqueAndMatchingGrids(markPositionsPerGrid, matchingGridIndexes, uniqueGrids);

            Draw(gridWidth, gridHeight, numGrids, gridList);

            return matchingGridIndexes;
        }

        private void Draw(int gridWidth, int gridHeight, int numGrids, List<bool[,]> gridList)
        {
            var gapsPerGrid = gridWidth * xGapSize;

            var gridTop = consoleService.CursorTop;
            bool bottomReached = false;
            for (int g = 0; g < numGrids; g++)
            {
                int gridLeft = (gridWidth + gapsPerGrid + gridGapSize) * g;

                if (bottomReached)
                {
                    var topOfRow = consoleService.CursorTop - (gridHeight - 1);
                    consoleService.SetCursorPosition(gridLeft, topOfRow);
                    bottomReached = false;
                    gridTop = consoleService.CursorTop;
                }
                else
                {
                    consoleService.SetCursorPosition(gridLeft, consoleService.CursorTop);
                }

                for (int y = 0; y < gridHeight; y++)
                {
                    consoleService.SetCursorPosition(gridLeft, gridTop + y);

                    for (int x = 0; x < gridWidth; x++)
                    {
                        var positions = gridList.ElementAt(g);
                        if (positions[x, y])
                            consoleService.ForegroundColor = markColour;
                        else
                            consoleService.ForegroundColor = colour;

                        consoleService.Write(shape);
                        consoleService.ResetColor();
                        consoleService.SetCursorPosition(consoleService.CursorLeft + xGapSize, consoleService.CursorTop);
                    }
                }

                bottomReached = true;
            }

            consoleService.SetCursorPosition(0, consoleService.CursorTop + 1);
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
            var grid1Index = new Random().Next(numGrids );
            int grid2Index = new Random().Next(numGrids );
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

            while (uniqueGrids.Count < numGrids)
            {
                for (int i = 0; i < numGrids; i++)
                {
                    uniqueGrids.Add(GenerateMarkPositionsPerGrid(gridWidth, gridHeight, minMarks, maxMarks));
                }
            }

            return uniqueGrids;
        }

        public static bool[,] GenerateMarkPositionsPerGrid(int gridWidth, int gridHeight, int minMarks, int maxMarks)
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