using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BowlingScoreCalculator
{
    // Handles API communication and JSON data extraction
    public class APICommunicator
    {
        private string url;
        private string token;

        // Constructor
        public APICommunicator(string url)
        {
            this.url = url;
        }

        // Extract data from the JSON string and return the point scores as an array
        public int[] GetPointsFromJsonString()
        {
            // Start by deserializing the JSON string into a custom object
            var jsonString = GetJsonStringFromAPI().Result;
            var jsonObject = JsonConvert.DeserializeObject<JsonObject>(jsonString);

            // Flatten the two-dimensional points array to a normal array (using LINQ)
            var points = jsonObject.Points.Cast<int>().ToArray();

            // Store the extracted token
            token = jsonObject.Token;

            // Display the extracted data for testing purposes
            Console.WriteLine($"Extracting token from JSON string:\n   {token}\n");
            Console.Write("Extracting points from JSON string:\n   ");
            Console.WriteLine(String.Join(", ", points));

            return points;
        }

        // Contact REST API endpoint (asynchronously) and return its JSON response as a string
        public async Task<string> GetJsonStringFromAPI()
        {
            string jsonString;

            using (var http = new HttpClient())
            {
                var response = await http.GetAsync(url);
                Console.WriteLine($"Sending request to API:\n   {response.RequestMessage}\n");
                Console.WriteLine($"Getting response from API:\n   Statuscode: {response.StatusCode}\n");
                jsonString = await response.Content.ReadAsStringAsync();
            }

            Console.WriteLine($"Receiving JSON string:\n   {jsonString}\n");

            return jsonString;
        }

        // Convert a list of point sums to a JSON string and post it
        public void PostPointSumsToAPI (IList<int> sums)
        {
            var jsonString = JsonConvert.SerializeObject(sums);
            PostJsonStringToAPI(jsonString).Wait();
        }

        // Contact REST API endpoint (asynchronously) and post a JSON string
        public async Task PostJsonStringToAPI(string jsonString)
        {
            using (var http = new HttpClient())
            {
                // Use a dictionary to encode the token and the JSON string containing the point sums
                var parameters = new Dictionary<string, string> { { "token", token }, { "points", jsonString } };
                var encodedContent = new FormUrlEncodedContent(parameters);
                Console.WriteLine($"\n\nEncoding token and sums as parameters:");
                Console.WriteLine($"   \"{encodedContent.ReadAsStringAsync().Result}\"");

                // Post it to the API and get a response 
                var response = await http.PostAsync(url, encodedContent);
                Console.WriteLine("\nPosting to API and receiving response:");
                Console.WriteLine($"   Response statuscode: {response.StatusCode}," +
                    $" value: {(int)response.StatusCode}");

                // If succes (= token was correct) then display the response content 
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"   Response JSON string: {responseContent}");
                }
            }
        }
    }
}