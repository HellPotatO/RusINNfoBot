using Newtonsoft.Json;


namespace RusINNfoBot.Json
{
    internal class ChatMemberUpdated
    {
        [JsonProperty("chat")]
        public string Chat { get; set; }
    }
}
