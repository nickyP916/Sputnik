
namespace Sputnik.Tests
{
    internal class ShapeDrawerShould
    {
        [Test]
        public void Generates_the_correct_number_of_unique_grids()
        {
            var numGrids = 4;
            var matchingIndexes = new[,] {{true, false}, {true, false}};
            var result = ShapeDrawer.GenerateUniqueGrids(3, 3, 3, 6, numGrids, matchingIndexes);

            Assert.That(result.Count, Is.EqualTo(numGrids));
            Assert.That(!result.Contains(matchingIndexes));//Not a good test yet
        }

        [Test]
        public void Combines_unique_and_matching_grids_correctly()
        {
            var markPositionsForMatchingGrids = new bool[3, 3]
            {
                {true, false, false},
                {false, true , true},
                {false, true, false }
            };

            var uniqueGrids = new HashSet<bool[,]>
            {
                new bool[3, 3]
                {
                    {false, true, false },
                    {false, false, false },
                    {false, true, true }
                },
                new bool[3,3]
                {
                    {true, false, false},
                    {false, false, false},
                    {true, true, false}
                }
            };

            var matchingGridIndexes = new int[] { 2, 0 };

            var result = ShapeDrawer.CombineUniqueAndMatchingGrids(markPositionsForMatchingGrids, matchingGridIndexes, uniqueGrids);

            var expectedResult = new List<bool[,]>
            {
                markPositionsForMatchingGrids,
                new bool[3, 3]
                {
                    {false, true, false },
                    {false, false, false },
                    {false, true, true }
                },
                markPositionsForMatchingGrids,
                new bool[3, 3]
                {
                    {true, false, false},
                    {false, false, false},
                    {true, true, false}
                },
            };

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
