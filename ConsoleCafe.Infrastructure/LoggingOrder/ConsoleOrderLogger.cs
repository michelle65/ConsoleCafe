using ConsoleCafe.Domain.Events;

namespace ConsoleCafe.Infrastructure.Logging
{
    public class ConsoleOrderLogger : IOrderEventSubscriber
    {
        private readonly string _currencySymbol;

        public ConsoleOrderLogger(string currencySymbol)
        {
            _currencySymbol = currencySymbol;
        }

        public void On(OrderPlaced evt)
        {
            Console.WriteLine($"[LOG] Order {evt.OrderId} placed at {evt.At}: {evt.Description} - Total: {_currencySymbol}{evt.Total:F2}");
        }
    }
}
