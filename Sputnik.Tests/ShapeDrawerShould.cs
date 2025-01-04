﻿using Draw;

namespace Sputnik.Tests
{
    internal class ShapeDrawerShould
    {
        [Test]
        public void Creates_a_collection_of_grids_to_be_drawn()
        {
            var numGrids = 4;
            var result = ShapeDrawer.DrawShapeSequence(3, numGrids);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Length, Is.EqualTo(numGrids));
        }

        [Test]
        public void Generates_the_correct_number_of_unique_grids()
        {
            var numGrids = 4;
            var result = ShapeDrawer.GenerateUniqueGrids(3, 3, 3, 6, numGrids);

            Assert.That(result.Count, Is.EqualTo(numGrids));
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

            var matchingGridIndexes = new int[] { 1, 3 };

            var result = ShapeDrawer.CombineUniqueAndMatchingGrids(markPositionsForMatchingGrids, matchingGridIndexes, uniqueGrids);

            var expectedResult = new List<bool[,]>
            {
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
                markPositionsForMatchingGrids
            };

            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
