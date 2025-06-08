using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusINNfoBot.Json
{
    internal class PollAnswer
    {
        [JsonProperty("poll_id")]
        public string PollId { get; set; }
    }
}
