using ConsoleCafe.Domain.Beverages;
using ConsoleCafe.Domain.Beverages.Decorators;
using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.UnitTests.Domain
{
    public class BeverageTests
    {
        [Fact]
        public void Espresso_ShouldHaveCorrectBasePrice()
        {
            var espresso = new Espresso();

            var cost = espresso.Cost();

            Assert.Equal(2.50m, cost);
        }

        [Fact]
        public void Tea_ShouldHaveCorrectBasePrice()
        {
            var tea = new Tea();

            var cost = tea.Cost();

            Assert.Equal(2.00m, cost);
        }

        [Fact]
        public void HotChocolate_ShouldHaveCorrectBasePrice()
        {
            var hotChocolate = new HotChocolate();

            var cost = hotChocolate.Cost();

            Assert.Equal(3.00m, cost);
        }

        [Fact]
        public void MilkDecorator_ShouldAddCostAndDescription()
        {
            var espresso = new Espresso();
            var milkDecorator = new MilkDecorator(espresso);

            var cost = milkDecorator.Cost();
            var description = milkDecorator.Describe();

            Assert.Equal(2.90m, cost);
            Assert.Contains("milk", description);
        }

        [Fact]
        public void SyrupDecorator_ShouldAddCostAndDescription()
        {
            var tea = new Tea();
            var syrupDecorator = new SyrupDecorator(tea, "vanilla");

            var cost = syrupDecorator.Cost();
            var description = syrupDecorator.Describe();

            Assert.Equal(2.50m, cost);
            Assert.Contains("vanilla syrup", description);
        }

        [Fact]
        public void ExtraShotDecorator_ShouldAddCostAndDescription()
        {
            var hotChocolate = new HotChocolate();
            var extraShotDecorator = new ExtraShotDecorator(hotChocolate);

            var cost = extraShotDecorator.Cost();
            var description = extraShotDecorator.Describe();

            Assert.Equal(3.80m, cost);
            Assert.Contains("extra shot", description);
        }

        [Fact]
        public void MultipleDecorators_ShouldCalculateCorrectTotal()
        {
            IBeverage beverage = new Espresso();
            beverage = new MilkDecorator(beverage);
            beverage = new SyrupDecorator(beverage, "vanilla");
            beverage = new ExtraShotDecorator(beverage);

            var cost = beverage.Cost();
            var description = beverage.Describe();

            Assert.Equal(4.20m, cost);
            Assert.Contains("Espresso", description);
            Assert.Contains("milk", description);
            Assert.Contains("vanilla syrup", description);
            Assert.Contains("extra shot", description);
        }
    }
}
