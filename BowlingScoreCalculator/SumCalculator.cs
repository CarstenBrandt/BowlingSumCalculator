using System.Collections.Generic;

namespace BowlingScoreCalculator
{
    public class SumCalculator
    {
        private int[] points;

        // Constructor
        public SumCalculator(int[] points)
        {
            this.points = points;
        }

        // Calculate score sums and return them as a list
        public IList<int> CalculateSums()
        {
            IList<int> sums = new List<int>();
            int currentSumTotal = 0;

            // Iterate until the points array ends or the last frame (no. 10) has been calculated
            for (int pointIndex = 0; pointIndex < points.Length && pointIndex < 20; pointIndex += 2)
            {
                if (IsStrike(pointIndex))
                {
                    currentSumTotal += CalculateStrikePoints(pointIndex);
                }
                else if (IsSpare(pointIndex))
                {
                    currentSumTotal += CalculateSparePoints(pointIndex);
                }
                else
                {
                    currentSumTotal += CalculatePointsInFrame(pointIndex);
                }

                sums.Add(currentSumTotal);
            }

            return sums;
        }

        // Helper methods used by CalculateSums()

        private bool IsStrike(int pointIndex) => points[pointIndex] == 10;

        private bool IsSpare(int pointIndex) => points[pointIndex] + points[pointIndex + 1] == 10;

        private int CalculateStrikePoints(int pointIndex)
        {
            // Points for the original strike
            int strikePoints = 10;

            // Add the next two point scores as bonuses if possible
            for (int nextScore = 1; nextScore < 3; nextScore++)
            {
                // Check for end of points array
                if (pointIndex + 2 >= points.Length) break;

                // Point to the next bonus score and add its value
                pointIndex += 2;
                strikePoints += points[pointIndex];

                // If not a strike or in the "11th frame", let index target the 2nd score in the same frame
                if (points[pointIndex] != 10 || pointIndex == 20)
                {
                    pointIndex--;
                }
            }

            return strikePoints;
        }

        private int CalculateSparePoints(int pointIndex)
        {
            // Points for the original spare
            int sparePoints = 10;

            // Check for end of points array
            if (pointIndex + 2 < points.Length)
            {
                // Add next point score as a bonus
                sparePoints += points[pointIndex + 2];
            }

            return sparePoints;
        }

        private int CalculatePointsInFrame(int pointIndex) => points[pointIndex] + points[pointIndex + 1];
    }
}