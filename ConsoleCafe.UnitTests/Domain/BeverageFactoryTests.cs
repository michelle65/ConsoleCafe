using ConsoleCafe.Domain.Beverages;
using ConsoleCafe.Infrastructure.Factories;

namespace ConsoleCafe.UnitTests.Domain
{
    public class BeverageFactoryTests
    {
        private readonly BeverageFactory _factory = new();

        [Theory]
        [InlineData("espresso", typeof(Espresso))]
        [InlineData("tea", typeof(Tea))]
        [InlineData("hotchocolate", typeof(HotChocolate))]
        public void Create_ShouldReturnExpectedType(string key, Type expectedType)
        {
            var beverage = _factory.Create(key);

            Assert.IsType(expectedType, beverage);
        }

        [Fact]
        public void Create_ShouldThrowForUnknownKey()
        {
            Assert.Throws<ArgumentException>(() => _factory.Create("unknown"));
        }
    }
}