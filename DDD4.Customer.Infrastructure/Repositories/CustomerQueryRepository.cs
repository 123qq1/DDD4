using DDD4.Customer.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Infrastructure.Repositories
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        // mongo db 
        async Task ICustomerQueryRepository.Add(Domain.Entities.Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
