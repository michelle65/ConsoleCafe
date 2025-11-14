using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Factories.Interfaces
{
    public interface IBeverageFactory
    {
        IBeverage Create(string key);
    }
}