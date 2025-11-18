using ConsoleCafe.Application.Services;
using ConsoleCafe.Domain.Pricing;
using ConsoleCafe.Domain.Pricing.Interfaces;
using ConsoleCafe.Infrastructure.Analytics;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class ConsoleMenu
    {
        private readonly OrderService _orderService;
        private readonly InMemoryOrderAnalytics _analytics;
        private readonly OrderInputCollector _orderInputCollector;
        private readonly MenuRenderer _renderer;
        private readonly MenuInputReader _reader;

        public ConsoleMenu(
            OrderService orderService,
            InMemoryOrderAnalytics analytics,
            OrderInputCollector orderInputCollector,
            MenuRenderer renderer,
            MenuInputReader reader)
        {
            _orderService = orderService;
            _analytics = analytics;
            _orderInputCollector = orderInputCollector;
            _renderer = renderer;
            _reader = reader;
        }

        public void Run()
        {
            var continueOrdering = true;

            while (continueOrdering)
            {
                try
                {
                    var orderRequest = _orderInputCollector.Collect();

                    var orderResult = _orderService.ProcessOrder(
                        orderRequest.BeverageType,
                        [.. orderRequest.AddOns],
                        [.. orderRequest.AddOnFlavors],
                        orderRequest.PricingStrategyType);

                    var pricingStrategy = ResolvePricingStrategy(orderRequest.PricingStrategyType);

                    _renderer.RenderReceipt(orderResult.OrderId, orderResult.Timestamp, orderResult.Description, orderResult.Subtotal, orderResult.Total, pricingStrategy);
                    _renderer.RenderAnalytics(_analytics.OrderCount, _analytics.TotalRevenue);

                    continueOrdering = AskToContinue();
                }
                catch (Exception ex)
                {
                    _renderer.RenderError(ex);
                    Console.ReadKey();
                }
            }

            _renderer.RenderFarewell();
        }

        private bool AskToContinue()
        {
            _renderer.RenderContinueOptions();
            var continueChoice = _reader.ReadOption("Enter your choice (0-1): ", ["0", "1"]);
            return continueChoice != "0";
        }
        private IPricingStrategy ResolvePricingStrategy(PricingStrategyType pricingStrategyType)
        {
            return pricingStrategyType switch
            {
                PricingStrategyType.HappyHour => new HappyHourPricing(),
                _ => new RegularPricing()
            };
        }
    }
}
