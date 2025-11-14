using ConsoleCafe.ConsoleUI.Logging.Interfaces;

namespace ConsoleCafe.ConsoleUI.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogInline(string message)
        {
            Console.Write(message);
        }
    }
}