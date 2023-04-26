using DDD4.Order.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.CQRS.Queries
{
    public class ReadOrdersHandler : IRequestHandler<ReadOrder>
    {
        private readonly IOrderQueryRepository _orderQueryRepository;
        private readonly Mediator _mediator;
        public ReadOrdersHandler(IOrderQueryRepository orderQueryRepository, Mediator mediator)
        {
            _orderQueryRepository = orderQueryRepository;
            _mediator = mediator;
        }

        public async Task Handle(ReadOrder request, CancellationToken cancellationToken)
        {
            var order = await _orderQueryRepository.Read(request.orderId);

            await _mediator.Send(new OrderRead { entity = order });
        }
    }
}
