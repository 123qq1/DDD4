using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.CQRS.Commands
{
    public interface IOrderCreateCommand
    {
        Task Create(CreateOrderCommand createOrderCommand);
    }
}
