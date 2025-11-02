namespace HSEBank.Domain.Events;

public record DomainEvent(string Name, object? Payload);