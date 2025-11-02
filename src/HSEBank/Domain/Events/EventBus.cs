namespace HSEBank.Domain.Events;

public class EventBus : IEventBus
{
    private readonly List<Action<DomainEvent>> _handlers = new();
    public void Publish(DomainEvent ev)
    {
        foreach (var h in _handlers.ToArray())
        {
            h(ev);
        }
    }
    public void Subscribe(Action<DomainEvent> handler) => _handlers.Add(handler);
}