using ConsoleCafe.Domain.Beverages;
using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Domain.Beverages.Interfaces;
using ConsoleCafe.Domain.Factories.Interfaces;

namespace ConsoleCafe.Infrastructure.Factories
{
    public class BeverageFactory : IBeverageFactory
    {
        public IBeverage Create(BeverageType type)
        {
            return type switch
            {
                BeverageType.Espresso => new Espresso(),
                BeverageType.Tea => new Tea(),
                BeverageType.HotChocolate => new HotChocolate(),
                _ => throw new ArgumentException($"Unknown beverage type: {type}")
            };
        }
    }
}