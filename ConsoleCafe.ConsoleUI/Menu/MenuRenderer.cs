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
            var baseBeverage = $@"
Choose a base beverage:
1) Espresso ({_currencyOptions.Symbol}2.50)
2) Tea ({_currencyOptions.Symbol}2.00)
3) Hot Chocolate ({_currencyOptions.Symbol}3.00)";
            _logger.Log(baseBeverage);
        }

        public void RenderAddOnOptions()
        {
            var addOnOptions = $@"
Add-ons (choose 0 when done):
1) Milk (+{_currencyOptions.Symbol}0.40)
2) Syrup (+{_currencyOptions.Symbol}0.50)
3) Extra shot (+{_currencyOptions.Symbol}0.80)
0) Done";
            _logger.Log(addOnOptions);
        }

        public void RenderPricingOptions()
        {
            var pricingOptions = $@"
Choose pricing policy:
1) Regular
2) Happy Hour (20% discount)";
            _logger.Log(pricingOptions);
        }

        public void RenderReceipt(Guid orderId, DateTimeOffset timestamp, string description, decimal subtotal, decimal total, IPricingStrategy pricingStrategy)
        {
            var discount = subtotal - total;
            var receipt = $@"
=== Receipt ===:
Order {orderId.ToString().Substring(0, 8)}... @ {timestamp}
Items: {description}
Subtotal: {_currencyOptions.Symbol}{subtotal:F2}
{(discount > 0 ? $"\nDiscount: -{_currencyOptions.Symbol}{discount:F2}" : "")};
Pricing: {pricingStrategy.Name}{(pricingStrategy is HappyHourPricing ? " (-20%)" : string.Empty)}
Total: {_currencyOptions.Symbol}{total:F2}";
            _logger.Log(receipt);
        }

        public void RenderAnalytics(int orderCount, decimal totalRevenue)
        {
            var analytics = $@"
Total Orders: {orderCount}
Total Revenue: {_currencyOptions.Symbol}{totalRevenue:F2}
";
            _logger.Log(analytics);

        }

        public void RenderContinueOptions()
        {
            var continueOptions = $@"
What would you like to do?
1) Place another order
0) Exit
";
            _logger.Log(continueOptions);
        }

        public void RenderFarewell()
        {
            _logger.LogSuccess("Thank you for using Cafe Console App!");
        }

        public void RenderError(Exception ex)
        {
            var errorMessage = $@"
An error occurred: {ex.Message}
Press any key to continue...
";
            _logger.LogError(errorMessage);
        }
    }
}
