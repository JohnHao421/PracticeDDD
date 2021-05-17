using FindSelf.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Linq;

namespace FindSelf.Infrastructure.DomainEventBus
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(IEnumerable<Entity> entities);
    }

    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator mediator;

        public DomainEventsDispatcher(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task DispatchEventsAsync(IEnumerable<Entity> entities)
        {
            var domainEvents = entities.SelectMany(x => x.DomainEvents).ToList();

            entities.ToList().ForEach(x => x.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}
