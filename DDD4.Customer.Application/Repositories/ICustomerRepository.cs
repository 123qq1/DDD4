using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.Repositories
{
    public interface ICustomerRepository
    {
        Task SaveAsync(Domain.Entities.Customer customer);
        Task<Domain.Entities.Customer> LoadAsync(Guid customerId);
    }
}
