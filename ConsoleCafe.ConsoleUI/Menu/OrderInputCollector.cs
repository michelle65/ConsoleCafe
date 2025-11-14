using ConsoleCafe.Domain.Pricing;
using ConsoleCafe.Domain.Pricing.Interfaces;

namespace ConsoleCafe.ConsoleUI.Menu
{
    public class OrderInputCollector
    {
        private readonly MenuRenderer _renderer;
        private readonly MenuInputReader _reader;

        public OrderInputCollector(MenuRenderer renderer, MenuInputReader reader)
        {
            _renderer = renderer;
            _reader = reader;
        }

        public OrderRequest Collect()
        {
            _renderer.RenderHeader();
            _renderer.RenderBeverageOptions();

            var beverageChoice = _reader.ReadOption("Enter your choice (1-3): ", new[] { "1", "2", "3" });
            var beverageType = beverageChoice switch
            {
                "1" => "espresso",
                "2" => "tea",
                "3" => "hotchocolate",
                _ => "espresso"
            };

            var addOns = new List<string>();
            var addOnFlavors = new List<string>();

            var addingAddOns = true;
            while (addingAddOns)
            {
                _renderer.RenderAddOnOptions();
                var addOnChoice = _reader.ReadOption("Enter your choice (0-3): ", new[] { "0", "1", "2", "3" });

                switch (addOnChoice)
                {
                    case "0":
                        addingAddOns = false;
                        break;
                    case "1":
                        addOns.Add("milk");
                        addOnFlavors.Add(string.Empty);
                        break;
                    case "2":
                        addOns.Add("syrup");
                        var flavor = _reader.ReadText("Enter syrup flavor (e.g., vanilla): ", "vanilla");
                        addOnFlavors.Add(flavor);
                        break;
                    case "3":
                        addOns.Add("extrashot");
                        addOnFlavors.Add(string.Empty);
                        break;
                }
            }

            _renderer.RenderPricingOptions();
            var pricingChoice = _reader.ReadOption("Enter your choice (1-2): ", new[] { "1", "2" });
            IPricingStrategy pricingStrategy = pricingChoice is "2"
                ? new HappyHourPricing()
                : new RegularPricing();

            return new OrderRequest
            {
                BeverageType = beverageType,
                AddOns = addOns,
                AddOnFlavors = addOnFlavors,
                PricingStrategy = pricingStrategy
            };
        }
    }
}