﻿using System.Text.Json.Serialization;

namespace Server.Models
{
    public class ChatMessage
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
