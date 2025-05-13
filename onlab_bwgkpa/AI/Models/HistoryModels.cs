using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AI.Models
{
    public class HistoryInput
    {
        [Required]
        public List<HistoryItem> Messages { get; set; } = [];

        public class HistoryItem
        {
            [Required]
            public HistoryRole Role { get; set; } = HistoryRole.User;

            [Required]
            public string Content { get; set; } = "";
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum HistoryRole
        {
            User,
            System
        }
    }
}
