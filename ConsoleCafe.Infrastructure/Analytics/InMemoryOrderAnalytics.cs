using ConsoleCafe.Domain.Events;

namespace ConsoleCafe.Infrastructure.Analytics
{
    public class InMemoryOrderAnalytics : IOrderEventSubscriber
    {
        public int OrderCount { get; private set; }
        public decimal TotalRevenue { get; private set; }

        public void On(OrderPlaced evt)
        {
            OrderCount++;
            TotalRevenue += evt.Total;
        }

        public void Reset()
        {
            OrderCount = 0;
            TotalRevenue = 0m;
        }
    }
}