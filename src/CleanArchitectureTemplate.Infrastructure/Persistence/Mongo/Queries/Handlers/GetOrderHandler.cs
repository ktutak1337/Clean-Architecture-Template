#if (mongo)
using System;
using System.Threading.Tasks;
using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Queries;
using CleanArchitectureTemplate.Infrastructure.Mappings;
using CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Documents;
#if (mediatr)
using MediatR;
using System.Threading;
#else
using Convey.CQRS.Queries;
#endif
using Convey.Persistence.MongoDB;

namespace CleanArchitectureTemplate.Infrastructure.Persistence.Mongo.Queries.Handlers
{
    #if(mediatr)
    public class GetOrderHandler : IRequestHandler<GetOrder, OrderDto>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public GetOrderHandler(IMongoRepository<OrderDocument, Guid> repository)
            => _repository = repository;

        public async Task<OrderDto> Handle(GetOrder query, CancellationToken cancellationToken)
            => (await _repository.GetAsync(x => x.Id == query.Id))
                ?.AsDto();
    }
    #else
    public class GetOrderHandler : IQueryHandler<GetOrder, OrderDto>
    {
        private readonly IMongoRepository<OrderDocument, Guid> _repository;

        public GetOrderHandler(IMongoRepository<OrderDocument, Guid> repository)
            => _repository = repository;

        public async Task<OrderDto> HandleAsync(GetOrder query)
            => (await _repository.GetAsync(x => x.Id == query.Id))
                ?.AsDto();
    }
    #endif
}
#endif
