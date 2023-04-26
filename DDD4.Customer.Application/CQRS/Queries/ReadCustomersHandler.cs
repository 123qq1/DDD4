using DDD4.Customer.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.CQRS.Queries
{
    public class ReadCustomersHandler : IRequestHandler<ReadCustomer>
    {
        private readonly ICustomerQueryRepository _customerQueryRepository;
        private readonly Mediator _mediator;
        public ReadCustomersHandler(ICustomerQueryRepository customerQueryRepository, Mediator mediator)
        {
            _customerQueryRepository = customerQueryRepository;
            _mediator = mediator;
        }

        public async Task Handle(ReadCustomer request, CancellationToken cancellationToken)
        {
            var customer = await _customerQueryRepository.Read(request.customerId);

            await _mediator.Send(new CustomerRead{entity = customer});
        }
    }
}
