﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusINNfoBot.Json
{
    internal class Poll
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
