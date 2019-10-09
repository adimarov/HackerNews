using Newtonsoft.Json;

namespace HackerNewsFunctionApp.Domain
{
    public class Story
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("by")]
        public string By { get; set; } = "";
        [JsonProperty("descendants")]
        public int Descendants { get; set; }
        [JsonProperty("kids")]
        public int[] Kids { get; set; }
        [JsonProperty("score")]
        public int Score { get; set; }
        [JsonProperty("time")]
        public long Time { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; } = "";
        [JsonProperty("type")]
        public string Type { get; set; } = "";
        [JsonProperty("url")]
        public string Url { get; set; } = "";
        [JsonProperty("text")]
        public string Text { get; set; } = "";

    }
}
