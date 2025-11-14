using ConsoleCafe.Domain.Beverages;
using ConsoleCafe.Domain.Beverages.Interfaces;
using ConsoleCafe.Domain.Factories.Interfaces;

namespace ConsoleCafe.Infrastructure.Factories
{
    public class BeverageFactory : IBeverageFactory
    {
        public IBeverage Create(string key)
        {
            return key.ToLower() switch
            {
                "espresso" => new Espresso(),
                "tea" => new Tea(),
                "hotchocolate" => new HotChocolate(),
                _ => throw new ArgumentException($"Unknown beverage type: {key}")
            };
        }
    }
}