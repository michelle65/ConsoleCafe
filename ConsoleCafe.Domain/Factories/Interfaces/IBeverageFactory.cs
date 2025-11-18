using ConsoleCafe.Domain.Beverages.Enums;
using ConsoleCafe.Domain.Beverages.Interfaces;

namespace ConsoleCafe.Domain.Factories.Interfaces
{
    public interface IBeverageFactory
    {
        IBeverage Create(BeverageType key);
    }
}