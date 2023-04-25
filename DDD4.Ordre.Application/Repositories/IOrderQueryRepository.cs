using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.Repositories
{
    public interface IOrderQueryRepository
    {
        public Task Add(Domain.Entities.Order order);
    }
}
