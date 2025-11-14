namespace ConsoleCafe.Domain.Events
{
    public interface IOrderEventSubscriber
    {
        void On(OrderPlaced evt);
    }
}