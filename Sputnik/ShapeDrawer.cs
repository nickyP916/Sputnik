using Sputnik.Services;

namespace Sputnik
{
    public class ShapeDrawer(IConsoleService consoleService) : IShapeDrawer
    {
        private const char Shape = '\u25A0';
        private const ConsoleColor Colour = ConsoleColor.Blue;
        private const ConsoleColor MarkColour = ConsoleColor.Green;
        private const int GridGapSize = 2;
        private const int XGapSize = 1;

        public int[] DrawShapeSequence(int gridWidth, int numGrids)
        {
            var gridHeight = gridWidth;
            var minMarks = gridHeight;
            var maxMarks = gridWidth * 2;

            var markPositionsForMatchingGrids = GenerateMarkPositionsPerGrid(gridWidth, gridHeight, minMarks, maxMarks);
            var matchingGridIndexes = GenerateIndexesFor2MatchingGrids(numGrids);

            var numUniqueGrids = numGrids - matchingGridIndexes.Length;
            var uniqueGrids = GenerateUniqueGrids(gridWidth, gridHeight, minMarks, maxMarks, numUniqueGrids, markPositionsForMatchingGrids);
            var gridList = CombineUniqueAndMatchingGrids(markPositionsForMatchingGrids, matchingGridIndexes, uniqueGrids);

            Draw(gridWidth, gridHeight, numGrids, gridList);

            return matchingGridIndexes;
        }

        private void Draw(int gridWidth, int gridHeight, int numGrids, List<bool[,]> gridList)
        {
            var gapsPerGrid = gridWidth * XGapSize;
            var gridTop = consoleService.CursorTop;

            consoleService.SetCursorPosition(0, consoleService.CursorTop);
            for (var g = 0; g < numGrids; g++)
            {
                var gridLeft = (gridWidth + gapsPerGrid + GridGapSize) * g;

                //If we've just drawn a grid, move back up to the top row to start a new grid
                if (g != 0)
                {
                    var topOfRow = consoleService.CursorTop - (gridHeight - 1);
                    consoleService.SetCursorPosition(gridLeft, topOfRow);
                    gridTop = consoleService.CursorTop;
                }

                //Drawing each row of the grid
                for (var y = 0; y < gridHeight; y++)
                {
                    consoleService.SetCursorPosition(gridLeft, gridTop + y);

                    //Drawing each square in the row
                    for (var x = 0; x < gridWidth; x++)
                    {
                        var positions = gridList.ElementAt(g);
                        consoleService.ForegroundColor = positions[x, y] ? MarkColour : Colour;
                        consoleService.Write(Shape);
                        consoleService.ResetColor();
                        consoleService.SetCursorPosition(consoleService.CursorLeft + XGapSize, consoleService.CursorTop);
                    }
                }
            }

            consoleService.SetCursorPosition(0, consoleService.CursorTop + 1);
        }

        public static List<bool[,]> CombineUniqueAndMatchingGrids(bool[,] markPositionsForMatchingGrids, int[] matchingGridIndexes, HashSet<bool[,]> uniqueGrids)
        {
            matchingGridIndexes = matchingGridIndexes.OrderBy(x => x).ToArray();
            var gridList = uniqueGrids.ToList();
            foreach (var matchingIndex in matchingGridIndexes)
            {
                gridList.Insert(matchingIndex, markPositionsForMatchingGrids);
            }

            return gridList;
        }

        private static int[] GenerateIndexesFor2MatchingGrids(int numGrids)
        {
            var grid1Index = new Random().Next(numGrids);
            var grid2Index = new Random().Next(numGrids);
            while (grid2Index == grid1Index)
            {
                grid2Index = new Random().Next(numGrids - 1);
            }

            return [grid1Index, grid2Index];
        }

        public static HashSet<bool[,]> GenerateUniqueGrids(int gridWidth, int gridHeight, int minMarks, int maxMarks, int numGrids, bool[,] matchingGrid)
        {
            var uniqueGrids = new HashSet<bool[,]>();
            var flattenedMatching = matchingGrid.Cast<bool>();
            while (uniqueGrids.Count < numGrids)
            {
                var generateMarkPositionsPerGrid = GenerateMarkPositionsPerGrid(gridWidth, gridHeight, minMarks, maxMarks);
                var flattenedUnique = generateMarkPositionsPerGrid.Cast<bool>();
                if (!flattenedUnique.SequenceEqual(flattenedMatching))
                {
                    uniqueGrids.Add(generateMarkPositionsPerGrid);
                }
            }

            return uniqueGrids;
        }

        public static bool[,] GenerateMarkPositionsPerGrid(int gridWidth, int gridHeight, int minMarks, int maxMarks)
        {
            var markPositions = new bool[gridWidth, gridHeight];

            var numMarks = new Random().Next(minMarks, maxMarks);

            for (var i = 0; i < numMarks; i++)
            {
                var xPos = new Random().Next(gridWidth);
                var yPos = new Random().Next(gridHeight);

                markPositions[xPos, yPos] = true;
            }

            return markPositions;
        }
    }
}