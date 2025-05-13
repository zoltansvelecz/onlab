using System.Text.Json.Serialization;

namespace Server.Models
{
    public class HRResponse
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

    }
}
