using MediatR;
using DDD4.Customer.Application.Repositories;
using DDD4.Customer.Domain.Entities;
using DDD4.Customer.Application.CQRS.Queries;

namespace DDD4.Customer.Application.CQRS.Commands.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMediator _mediator;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            //send til eventstore 

            Domain.Entities.Customer entity = await _customerRepository.LoadAsync(request.CustomerId); 

            entity.Create(request.CustomerId,
                request.CustomerName,
                request.AccountName,
                request.DiscordName
                );

            await _customerRepository.SaveAsync(entity);

            //Send til mongo db igennem ICustomerQueryRepository

            var customerCreatedEvent = new CustomerCreated
            {
                entity = entity,
            };

            await _mediator.Send(customerCreatedEvent);
        }
    }
}
