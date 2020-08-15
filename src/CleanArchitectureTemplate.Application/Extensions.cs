using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;

namespace CleanArchitectureTemplate.Application
{
    public static class Extensions
    {
        public static IConveyBuilder AddApplication(this IConveyBuilder builder) 
            => builder
                .AddCommandHandlers()
                .AddEventHandlers()
                .AddQueryHandlers()
                .AddInMemoryCommandDispatcher()
                .AddInMemoryEventDispatcher()
                .AddInMemoryQueryDispatcher();
    }
}
