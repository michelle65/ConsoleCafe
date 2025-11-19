using ConsoleCafe.ConsoleUI.Logging.Interfaces;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class MenuInputReader
    {
        private readonly ILogger _logger;

        public MenuInputReader(ILogger logger)
        {
            _logger = logger;
        }

        public string ReadOption(string prompt, IEnumerable<string> validOptions)
        {
            var optionSet = validOptions.ToArray();
            string? input;

            do
            {
                _logger.LogInline(prompt);
                input = Console.ReadLine();

                if (input is not null && optionSet.Contains(input))
                {
                    return input;
                }

                _logger.Log("Invalid input. Please try again.");
            } while (true);
        }

        public string ReadText(string prompt, string defaultValue)
        {
            _logger.LogInline(prompt);
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }

            return input;
        }
    }
}
