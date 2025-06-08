using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusINNfoBot
{
    public class BotClient
    {
        private static BotClient instance;
        private static readonly object lockObj = new object();
        private readonly HttpClient httpClient;
        private static readonly string Token = Environment.GetEnvironmentVariable("RusINNfoBot:TelegramToken");
        private static readonly string BaseUrl = $"https://api.telegram.org/bot{Token}/";

        private BotClient()
        {
            httpClient = new HttpClient();
        }

        public static BotClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new BotClient();
                        }
                    }
                }
                return instance;
            }
        }

        public HttpClient Http => httpClient;


        public Uri GetUpdateMethod(int offset)
        {
            string method = "getUpdates";
            if (offset != -1)
            {
                method += $"?offset={offset}";
            }
            return new Uri(BaseUrl + method);
        }

        public string GetApiUrl(string method)
        {
            return BaseUrl + method;
        }

    }
}
