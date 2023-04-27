using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DDD4.Order.Application.CQRS.Queries
{
    internal class OrderCreated : IRequest
    {
        public Domain.Entities.Order? entity { get; set; }
    }
}
