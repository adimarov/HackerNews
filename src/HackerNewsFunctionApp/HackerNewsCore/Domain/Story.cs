using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

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
        [JsonProperty("time"), JsonConverter(typeof(MicrosecondEpochConverter))]
        public string Time { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; } = "";
        [JsonProperty("type")]
        public string Type { get; set; } = "";
        [JsonProperty("url")]
        public string Url { get; set; } = "";
        [JsonProperty("text")]
        public string Text { get; set; } = "";
    }

    public class MicrosecondEpochConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) { return null; }
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds((long)reader.Value);
            return dateTimeOffset.UtcDateTime.ToString("MM/dd/yyyy");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
