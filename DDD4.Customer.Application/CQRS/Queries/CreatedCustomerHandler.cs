using DDD4.Customer.Application.CQRS.Commands.CreateCustomer;
using DDD4.Customer.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.CQRS.Queries
{
    public class CreatedCustomerHandler : IRequestHandler<CustomerCreated>
    {
        private readonly ICustomerQueryRepository _queryRepository;

        public CreatedCustomerHandler(/*ICustomerQueryRepository queryRepository*/)
        {
            //_queryRepository = queryRepository;
        }

        public async Task Handle(CustomerCreated request, CancellationToken cancellationToken)
        {
            //await _queryRepository.Add(request.entity);
        }
    }
}
