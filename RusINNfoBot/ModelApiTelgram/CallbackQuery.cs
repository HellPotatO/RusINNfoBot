using Newtonsoft.Json;

namespace RusINNfoBot.Json
{
    internal class CallbackQuery
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
