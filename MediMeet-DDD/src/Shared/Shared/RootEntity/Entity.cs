using Shared.EventBus;

namespace Shared.RootEntity;

public class Entity
{
    private List<IDomainEvent> _domainEvents = new();
    
    public ICollection<IDomainEvent> DomainEvents => _domainEvents;
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}