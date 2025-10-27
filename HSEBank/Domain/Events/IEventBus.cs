namespace HSEBank.Domain.Events;

public interface IEventBus
{
    void Publish(DomainEvent ev);
    void Subscribe(Action<DomainEvent> handler);
}