#if (mongo)
using System;
using System.Linq;
using System.Collections.Generic;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
#if (mediatr)
using MediatR;
using System.Threading;
#else
using Convey.CQRS.Queries;
#endif
using Convey.Persistence.MongoDB;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Infrastructure.Mappings;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Queries.Handlers
{
    #if(mediatr)
    public class GetOrdersHandler : IRequestHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public GetOrdersHandler(IMongoRepository<OrderDocument, Guid> repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> Handle(GetOrders query, CancellationToken cancellationToken)
            => (await _repository.FindAsync(_ => true))
                .Select(order => order.AsDto());
    }
    #else
    public class GetOrdersHandler : IQueryHandler<GetOrders, IEnumerable<OrderDto>>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public GetOrdersHandler(IMongoRepository<OrderDocument, Guid> repository)
            => _repository = repository;

        public async Task<IEnumerable<OrderDto>> HandleAsync(GetOrders query)
            => (await _repository.FindAsync(_ => true))
                .Select(order => order.AsDto());
    }
    #endif


}
#endif
