namespace BowlingScoreCalculator
{
    // Custom object for deserializing JSON string
    public class JsonObject
    {
        public int[,] Points { get; set; }
        public string Token { get; set; }
    }
}