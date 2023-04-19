using DDD4.Customer.Domain.Repositories;
using DDD4.Customer.Domain.Entities;
using MediatR;

namespace DDD4.Customer.Application.CQRS.Commands.CreateCustomer
{
    internal sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerIdResult = CustomerId.Create(request.CustomerId);
            var customerNameResult = CustomerName.Create(request.CustomerName);
            var accountNameResult = AccountName.Create(request.AccountName);
            var discordNameResult = DiscordName.Create(request.DiscordName);

            var customer = new Domain.Entities.Customer(
                customerIdResult.Value,
                customerNameResult.Value,
                accountNameResult.Value,
                discordNameResult.Value
                );

            _customerRepository.Add(customer);

            await _unitOfWork.SaveChangesAsync(cancellationToken);  

            return Unit.Value;
        }
    }
}
