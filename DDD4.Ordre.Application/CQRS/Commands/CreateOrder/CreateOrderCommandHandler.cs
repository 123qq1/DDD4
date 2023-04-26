using DDD4.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using DDD4.Order.Application.Repositories;
using DDD4.Order.Domain.Entities;
using DDD4.Order.Application.CQRS.Queries;

namespace DDD4.Order.Application.CQRS.Commands.CreateOrder
{
    internal sealed class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public CreateOrderCommandHandler(
            IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //send til eventstore 

            Domain.Entities.Order entity = await _orderRepository.LoadAsync(request.OrderId);

            entity.Create(request.OrderId,
                request.CustomerName,
                request.AccountName,
                request.DiscordName,
                request.RoleValue
                );

            await _orderRepository.SaveAsync(entity);

            //Send til mongo db igennem ICustomerQueryRepository

            var orderCreatedEvent = new OrderCreated
            {
                entity = entity,
            };

            await _mediator.Send(orderCreatedEvent);
        }
    }
}
