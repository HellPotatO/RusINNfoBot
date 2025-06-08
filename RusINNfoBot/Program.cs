using Newtonsoft.Json;
using Npgsql;
using RusINNfoBot.Data;
using RusINNfoBot.Handlers;
using RusINNfoBot.Json;

namespace RusINNfoBot
{
    internal class Program
    {
        public class BodyMessage
        {
            public string chat_id { get; set; }
            public string text { get; set; }
        }
        static async Task Main(string[] args)
        {
            int offset = -1;
            while (true)
            {
                var uri = BotClient.Instance.GetUpdateMethod(offset);
                var message = await BotClient.Instance.Http.GetAsync(uri);

                if (message != null && message.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var response = await message.Content.ReadAsStringAsync();

                    var telegramUpdate = JsonConvert.DeserializeObject<TelegramUpdate>(response);
                    if (!telegramUpdate.Ok || telegramUpdate.Result.Length == 0)
                    {
                        continue;
                    }

                    offset = telegramUpdate.Result[telegramUpdate.Result.Length - 1].UpdateId + 1;
                    var chatId = telegramUpdate.Result[telegramUpdate.Result.Length - 1].Message.Chat.Id;
                    string text = telegramUpdate.Result[telegramUpdate.Result.Length - 1].Message.Text;

                    if (CommandValidator.IsKnownCommand(text))
                    {
                        string answer = await HandleCommandAsync(text, chatId.ToString());
                        await MessageSender.SendMessageAsync(chatId.ToString(), answer);
                        await LastResponseRepository.SaveLastResponseAsync(chatId.ToString(), answer);
                    }
                    else
                    {
                        string answer = "Команда не распознана. Напишите /help для списка доступных команд.";
                        await MessageSender.SendMessageAsync(chatId.ToString(), answer);
                    }
                }

            }

        }
        public static async Task<string> HandleCommandAsync(string text, string chatId)
        {
            string command = text.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0].ToLower();

            switch (command)
            {
                case "/start":
                    return "Привет! Я бот для получения информации по ИНН. Напиши /help для списка команд.";

                case "/help":
                    return "Доступные команды:\n" +
                           "/start – начать общение\n" +
                           "/help – справка\n" +
                           "/hello – информация о разработчике\n" +
                           "/inn [ИНН...] – получить данные компаний\n" +
                           "/last - повторение последнего действия бота";

                case "/hello":
                    return "Разработчик: Мухин Дмитрий\nEmail: Dimoss432@yandex.ru\nGitHub: https://github.com/HellPotatO\nHH: https://hh.ru/resume/f3d1bcbdff0d2fb7300039ed1f544757753043";

                case "/inn":
                    return await InnHandler.HandleInnAsync(text);

                case "/last":
                    return await LastResponseRepository.GetLastResponseAsync(chatId.ToString());


                default:
                    return "Команда не распознана. Напишите /help для списка доступных команд.";
            }
        }
    }
}
