using System.Text.Json.Serialization;

namespace Server.Models
{
    public class HRRequest
    {
        [JsonPropertyName("cv")]
        public string Cv { get; set; }
    }
}
