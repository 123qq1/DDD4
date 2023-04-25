using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Order.Application.CQRS.Commands.CreateCustomer; //ødelagt namespace (y)

namespace DDD4.Order.Application.CQRS.Commands
{
    public interface IOrderCreateCommand
    {
        Task Create(CreateOrderCommand createOrderCommand); // skal den overhovedet være her???
    }
}
