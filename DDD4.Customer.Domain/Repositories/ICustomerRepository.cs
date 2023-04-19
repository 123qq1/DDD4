using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Customer.Domain.Entities;

namespace DDD4.Customer.Domain.Repositories
{
    public interface ICustomerRepository
    {
        void Add(Entities.Customer customer);
    }
}
