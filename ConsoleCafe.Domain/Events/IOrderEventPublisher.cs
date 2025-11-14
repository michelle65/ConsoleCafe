namespace ConsoleCafe.Domain.Events
{
    public interface IOrderEventPublisher
    {
        void Publish(OrderPlaced evt);
    }
}