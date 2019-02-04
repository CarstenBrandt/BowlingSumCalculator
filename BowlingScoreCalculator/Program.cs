//      ***** Bowling Score Calculator (c)2019 Carsten Brandt *****

using System;

namespace BowlingScoreCalculator
{
    public class Program
    {
        // Program entry point
        static void Main(string[] args)
        {
            Console.WriteLine("\t***** Bowling Score Calculator (c)2019 Carsten Brandt *****\n");

            // Contact the REST API endpoint and extract a points array from its JSON response
            var apiCommunicator = new APICommunicator(@"http://13.74.31.101/api/points");
            var points = apiCommunicator.GetPointsFromJsonString();

            // Calculate point sums and return them as a list
            var sumCalculator = new SumCalculator(points);
            var sums = sumCalculator.CalculateSums();

            // Display the sums list
            Console.Write("\nCalculating bowling point sums:\n   ");
            Console.Write(String.Join(", ", sums));

            // Convert the sums list and post it to the REST API
            apiCommunicator.PostPointSumsToAPI(sums);

            Console.WriteLine("\n\nPress any key to end the program");
            Console.ReadKey();
        }
    }
}
