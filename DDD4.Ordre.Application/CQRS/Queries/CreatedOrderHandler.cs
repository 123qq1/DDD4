using MediatR;
using DDD4.Order.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.CQRS.Queries
{
    internal class CreatedOrderHandler : IRequestHandler<OrderCreated>
    {
        private readonly IOrderQueryRepository _queryRepository;

        CreatedOrderHandler(IOrderQueryRepository queryRepository)
        {
            _queryRepository = queryRepository;
        }

        public async Task Handle(OrderCreated request, CancellationToken cancellationToken)
        {
            await _queryRepository.Add(request.entity);
        }
    }
}
