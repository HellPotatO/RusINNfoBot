using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusINNfoBot.Json
{
    internal class ChosenInlineResult
    {
        [JsonProperty("result_id")]
        public string ResultId { get; set; }
    }
}
