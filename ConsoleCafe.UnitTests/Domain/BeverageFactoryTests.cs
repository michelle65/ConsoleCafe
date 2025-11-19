using ConsoleCafe.Domain.Beverages;
using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Infrastructure.Factories;

namespace ConsoleCafe.UnitTests.Domain
{
    public class BeverageFactoryTests
    {
        private readonly BeverageFactory _factory = new();

        [Theory]
        [InlineData(BeverageType.Espresso, typeof(Espresso))]
        [InlineData(BeverageType.Tea, typeof(Tea))]
        [InlineData(BeverageType.HotChocolate, typeof(HotChocolate))]
        public void Create_ShouldReturnExpectedType(BeverageType key, Type expectedType)
        {
            var beverage = _factory.Create(key);

            Assert.IsType(expectedType, beverage);
        }
    }
}
