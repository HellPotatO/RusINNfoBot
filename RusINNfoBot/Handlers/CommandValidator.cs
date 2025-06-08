using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RusINNfoBot.Handlers
{
    internal class CommandValidator
    {
        private static readonly string[] knownCommands = 
        {
            "/start",
            "/help",
            "/hello", 
            "/inn",
            "/last"
        };

        public static bool IsKnownCommand(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false;
            }

            var command = text.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
            return knownCommands.Contains(command);
        }
    }
}
