using System.Collections.Generic;

namespace Server.Models
{
    public class ChatRequest
    {
        public List<ChatMessage> Messages { get; set; } = new();
    }
}
