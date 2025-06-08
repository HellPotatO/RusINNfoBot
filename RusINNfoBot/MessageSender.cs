using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RusINNfoBot.Program;

namespace RusINNfoBot
{
    public static class MessageSender
    {
        public static async Task SendMessageAsync(string chatId, string text)
        {
            BodyMessage bodyMessage = new BodyMessage
            {
                chat_id = chatId,
                text = text
            };

            string json = JsonConvert.SerializeObject(bodyMessage);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = BotClient.Instance.Http;
            string url = BotClient.Instance.GetApiUrl("sendMessage");
            await client.PostAsync(url, content);
        }
    }
}
