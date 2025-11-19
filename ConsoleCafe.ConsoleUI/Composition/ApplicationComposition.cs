using ConsoleCafe.Application.Eventing;
using ConsoleCafe.Application.Services;
using ConsoleCafe.ConsoleUI.Logging;
using ConsoleCafe.ConsoleUI.Menu;
using ConsoleCafe.ConsoleUI.Options;
using ConsoleCafe.Infrastructure.Analytics;
using ConsoleCafe.Infrastructure.Factories;
using ConsoleCafe.Infrastructure.Logging;

namespace ConsoleCafe.ConsoleUI.Composition
{
    public class ApplicationComposition
    {
        public static ConsoleMenu Compose()
        {
            var beverageFactory = new BeverageFactory();
            var currencyOptions = new CurrencyOptions();
            var consoleLogger = new ConsoleOrderLogger(currencyOptions.Symbol);
            var analytics = new InMemoryOrderAnalytics();
            var eventPublisher = new SimpleOrderEventPublisher([consoleLogger, analytics]);
            var orderService = new OrderService(beverageFactory, eventPublisher);

            var uiLogger = new ConsoleLogger();
            var menuReader = new MenuInputReader(uiLogger);
            var menuRenderer = new MenuRenderer(uiLogger, currencyOptions);
            var orderInputCollector = new OrderInputCollector(menuRenderer, menuReader);

            var menu = new ConsoleMenu(orderService, analytics, orderInputCollector, menuRenderer, menuReader);

            return menu;
        }
    }
}
