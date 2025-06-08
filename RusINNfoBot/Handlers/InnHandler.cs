using System.Text;
using Dadata;
using Dadata.Model;
using Newtonsoft.Json.Linq;

namespace RusINNfoBot.Handlers
{
    public static class InnHandler
    {
        private static readonly string token = Environment.GetEnvironmentVariable("RusINNfoBot:DadataToken");
        private static readonly SuggestClientAsync api = new SuggestClientAsync(token);

        public static async Task<string> HandleInnAsync(string text)
        {
            var parts = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                return "Вы не указали ИНН. Пример использования: /inn 7707083893";
            }

            var inns = parts.Skip(1).ToList();
            var validInns = new List<string>();
            var invalidInns = new List<string>();

            foreach (var inn in inns)
            {
                if (IsValidInn(inn))
                {
                    validInns.Add(inn);
                }
                else
                {
                    invalidInns.Add(inn);
                }
            }

            if (validInns.Count == 0)
            {
                return "Все указанные ИНН некорректны. Убедитесь, что каждый ИНН содержит 10 или 12 цифр.";
            }

            var companies = new List<(string Name, string Address)>();

            foreach (var inn in validInns)
            {
                var response = await api.FindParty(inn);
                var suggestion = response.suggestions.FirstOrDefault();

                if (suggestion != null)
                {
                    var data = suggestion.data;
                    companies.Add((data.name.full_with_opf, data.address.value));
                }
            }

            if (companies.Count == 0)
            {
                return "По указанным ИНН не удалось найти ни одной компании.";
            }

            var sb = new StringBuilder();

            if (invalidInns.Count > 0)
            {
                sb.AppendLine($"Некорректные ИНН: {string.Join(", ", invalidInns)}");
            }

            sb.AppendLine("Результаты поиска:");

            foreach (var company in companies.OrderBy(c => c.Name))
            {
                sb.AppendLine($" {company.Name}\n {company.Address}\n");
            }

            var result = sb.ToString();

            if (result.Length > 4096)
            {
                return "Извините, ответ слишком длинный. Пожалуйста, укажите меньше ИНН для обработки. В будущих обновлениях мы постараемся устранить это ограничение.";
            }

            return result;
        }


        private static bool IsValidInn(string inn)
        {
            return (inn.Length == 10 || inn.Length == 12) && inn.All(char.IsDigit);
        }


    }
}