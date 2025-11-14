using ConsoleCafe.ConsoleUI.Logging.Interfaces;
using ConsoleCafe.ConsoleUI.Options;
using ConsoleCafe.Domain.Pricing;
using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class MenuRenderer
    {
        private readonly ILogger _logger;
        private readonly CurrencyOptions _currencyOptions;

        public MenuRenderer(ILogger logger, CurrencyOptions currencyOptions)
        {
            _logger = logger;
            _currencyOptions = currencyOptions;
        }

        public void RenderHeader()
        {
            Console.Clear();
            _logger.Log("=== Cafe Console App ===");
            _logger.Log(string.Empty);
        }

        public void RenderBeverageOptions()
        {
            _logger.Log("Choose a base beverage:");
            _logger.Log($"1) Espresso ({_currencyOptions.Symbol}2.50)");
            _logger.Log($"2) Tea ({_currencyOptions.Symbol}2.00)");
            _logger.Log($"3) Hot Chocolate ({_currencyOptions.Symbol}3.00)");
        }

        public void RenderAddOnOptions()
        {
            _logger.Log(string.Empty);
            _logger.Log("Add-ons (choose 0 when done):");
            _logger.Log($"1) Milk (+{_currencyOptions.Symbol}0.40)");
            _logger.Log($"2) Syrup (+{_currencyOptions.Symbol}0.50)");
            _logger.Log($"3) Extra shot (+{_currencyOptions.Symbol}0.80)");
            _logger.Log("0) Done");
        }

        public void RenderPricingOptions()
        {
            _logger.Log(string.Empty);
            _logger.Log("Choose pricing policy:");
            _logger.Log("1) Regular");
            _logger.Log("2) Happy Hour (20% discount)");
        }

        public void RenderReceipt(Guid orderId, DateTimeOffset timestamp, string description, decimal subtotal, decimal total, IPricingStrategy pricingStrategy)
        {
            _logger.Log(string.Empty);
            _logger.Log("=== Receipt ===");
            _logger.Log($"Order {orderId.ToString().Substring(0, 8)}... @ {timestamp}");
            _logger.Log($"Items: {description}");
            _logger.Log($"Subtotal: {_currencyOptions.Symbol}{subtotal:F2}");
            var discount = subtotal - total;
            if (discount > 0)
            {
                _logger.Log($"Discount: -{_currencyOptions.Symbol}{discount:F2}");
            }
            _logger.Log($"Pricing: {pricingStrategy.Name}{(pricingStrategy is HappyHourPricing ? " (-20%)" : string.Empty)}");
            _logger.Log($"Total: {_currencyOptions.Symbol}{total:F2}");
        }

        public void RenderAnalytics(int orderCount, decimal totalRevenue)
        {
            _logger.Log(string.Empty);
            _logger.Log($"Total Orders: {orderCount}");
            _logger.Log($"Total Revenue: {_currencyOptions.Symbol}{totalRevenue:F2}");
        }

        public void RenderContinueOptions()
        {
            _logger.Log(string.Empty);
            _logger.Log("What would you like to do?");
            _logger.Log("1) Place another order");
            _logger.Log("0) Exit");
        }

        public void RenderFarewell()
        {
            _logger.Log("Thank you for using Cafe Console App!");
        }

        public void RenderError(Exception ex)
        {
            _logger.Log($"An error occurred: {ex.Message}");
            _logger.Log("Press any key to continue...");
        }
    }
}