using System;
using Npgsql;

namespace RusINNfoBot.Data
{
    public sealed class DatabaseConnection
    {
        private static readonly Lazy<DatabaseConnection> _instance =
            new(() => new DatabaseConnection());

        private readonly NpgsqlConnection _connection;

        public static DatabaseConnection Instance => _instance.Value;

        private DatabaseConnection()
        {
            var connectionString = Environment.GetEnvironmentVariable("RusINNfoBot:PG");
                
            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
        }

        public NpgsqlConnection Connection => _connection;

        public void Close()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
