namespace ConsoleCafe.ConsoleUI.Logging.Interfaces
{
    public interface ILogger
    {
        void Log(string message);

        void LogInline(string message);
    }
}