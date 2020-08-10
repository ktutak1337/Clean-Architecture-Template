using System.Collections.Generic;
using System.Linq;

namespace CleanArchitectureTemplate.Core.BuildingBlocks
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        public int Version { get; protected set; }

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (!_domainEvents.Any())
            {
                Version++;
            }

            _domainEvents.Add(domainEvent);
        }

        public void ClearEvents() => _domainEvents.Clear();
    }
}
