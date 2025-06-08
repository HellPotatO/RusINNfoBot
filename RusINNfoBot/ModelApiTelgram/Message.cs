using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusINNfoBot.Json
{
    internal class Message
    {
        [JsonProperty("message_id")]
        public int MessageId { get; set; }

        [JsonProperty("from")]
        public User? From { get; set; }

        [JsonProperty("chat")]
        public Chat Chat { get; set; }

        [JsonProperty("date")]
        public int Date { get; set; }

        [JsonProperty("text")]
        public string? Text { get; set; }
    }
}
