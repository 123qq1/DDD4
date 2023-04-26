using DDD4.Customer.Application.CQRS.Queries;
using DDD4.Customer.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.Repositories
{
    public interface ICustomerQueryRepository
    {
        public Task Add(Domain.Entities.Customer customer);
        public Task<Domain.Entities.Customer> Read(Guid customerId);
    }
}
