using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.Repositories
{
    public interface IOrderRepository
    {
        Task SaveAsync(Domain.Entities.Order order);
        Task<Domain.Entities.Order> LoadAsync(Guid orderId);
    }
}
