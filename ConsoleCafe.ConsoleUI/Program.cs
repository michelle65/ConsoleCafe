using ConsoleCafe.ConsoleUI.Composition;

namespace ConsoleCafe.ConsoleUI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var menu = ApplicationComposition.Compose();
            menu.Run();
        }
    }
}