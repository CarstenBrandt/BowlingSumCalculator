using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BowlingScoreCalculator;

namespace BowlingScoreCalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void SingleFrame_AddsCorrectly()
        {
            // Arrange
            var points = new int[] { 5, 2 };
            var expected = new List<int> { 7 };

            // Act and Assert
            ActAndAssert(points, expected);
        }

        [TestMethod]
        public void FramesBelowTenPoints_AddsCorrectly()
        {
            // Arrange
            var points = new int[] { 3, 1, 5, 4, 2, 7, 0, 0, 9, 0, 1, 8 };
            var expected = new List<int> { 4, 13, 22, 22, 31, 40 };

            // Act and Assert
            ActAndAssert(points, expected);
        }

        [TestMethod]
        public void TwelveStrikes_AddsCorrectly()
        {
            // Arrange
            var points = new int[] { 10, 0, 10, 0, 10, 0, 10, 0, 10, 0, 10, 0,
                                     10, 0, 10, 0, 10, 0, 10, 0, 10, 10};
            var expected = new List<int> { 30, 60, 90, 120, 150, 180, 210,
                                           240, 270, 300 };

            // Act and Assert
            ActAndAssert(points, expected);
        }

        [TestMethod]
        public void PointsStrikesAndSpares_AddsCorrectly()
        {
            // Arrange
            var points = new int[] { 5, 0, 2, 8, 4, 4, 10, 0, 7, 2, 3, 0 };
            var expected = new List<int> { 5, 19, 27, 46, 55, 58 };

            // Act and Assert
            ActAndAssert(points, expected);
        }

        [TestMethod]
        public void GivenExample_AddsCorrectly()
        {
            // Arrange
            var points = new int[] { 3, 7, 10, 0, 8, 2, 8, 1, 10, 0, 3, 4, 7, 0, 5, 5, 3, 2, 2, 5 };
            var expected = new List<int> { 20, 40, 58, 67, 84, 91, 98, 111, 116, 123 };

            // Act and Assert
            ActAndAssert(points, expected);
        }
        
        // Act and Assert helper method
        private void ActAndAssert(int[] points, List<int> expected)
        {
            // Act
            var sumCalculator = new SumCalculator(points);
            var actual = (List<int>)sumCalculator.CalculateSums();

            // Assert
            CollectionAssert.AreEqual(expected, actual, "Points not added correctly");
        }
    }
}
