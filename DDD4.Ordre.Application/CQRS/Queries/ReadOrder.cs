using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.CQRS.Queries
{
    public class ReadOrder : IRequest
    {
        public Guid orderId { get; set; }
    }
}
