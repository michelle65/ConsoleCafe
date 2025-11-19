namespace ConsoleCafe.ConsoleUI.Logging.Interfaces
{
    public interface ILogger
    {
        void Log(string message);

        void LogInline(string message);

        void LogInfo(string message);

        void LogSuccess(string message);

        void LogError(string message);
    }
}
