using Npgsql;
using System.Threading.Tasks;

namespace RusINNfoBot.Data
{
    public class LastResponseRepository
    {
        public static async Task SaveLastResponseAsync(string chatId, string response)
        {
            long chatIdLong = long.Parse(chatId);
            var conn = DatabaseConnection.Instance.Connection; 

            const string sql = @"
                INSERT INTO u_l_res (chat_id, l_res)
                VALUES (@chat_id, @l_res)
                ON CONFLICT (chat_id)
                DO UPDATE SET l_res = @l_res;
            ";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("chat_id", chatIdLong);
            cmd.Parameters.AddWithValue("l_res", response);
            await cmd.ExecuteNonQueryAsync();
        }

        public static async Task<string> GetLastResponseAsync(string chatId)
        {
            long chatIdLong = long.Parse(chatId);
            var conn = DatabaseConnection.Instance.Connection; 

            const string sql = "SELECT l_res FROM u_l_res WHERE chat_id = @chat_id";

            await using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("chat_id", chatIdLong);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return reader.GetString(0);
            }

            return null;
        }
    }
}
