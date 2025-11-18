using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Domain.Pricing;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class OrderRequest
    {
        public BeverageType BeverageType { get; set; } = BeverageType.Espresso;

        public List<AddOnType> AddOns { get; set; } = new();

        public List<string> AddOnFlavors { get; set; } = new();

        public PricingStrategyType PricingStrategyType { get; set; } = PricingStrategyType.Regular;
    }
}
