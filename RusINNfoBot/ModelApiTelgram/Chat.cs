using Newtonsoft.Json;


namespace RusINNfoBot.Json
{
    public class Chat
    {
        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
