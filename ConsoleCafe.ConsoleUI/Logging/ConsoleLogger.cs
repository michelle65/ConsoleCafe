using ConsoleCafe.ConsoleUI.Logging.Interfaces;

namespace ConsoleCafe.ConsoleUI.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
        public void LogInline(string message) => Console.Write(message);

        public void LogInfo(string message) => Log($"[INFO] {message}");
        public void LogSuccess(string message) => Log($"[SUCCESS] {message}");
        public void LogError(string message) => Log($"[ERROR] {message}");
    }
}
