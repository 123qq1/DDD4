using DDD4.Customer.Application.CQRS.Commands.CreateCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.CQRS.Commands
{
    public interface ICustomerCreateCommand
    {
        Task Create(CreateCustomerCommand createCustomerCommand);
    }
}
