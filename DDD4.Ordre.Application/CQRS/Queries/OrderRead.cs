using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.CQRS.Queries
{
    internal class OrderRead : IRequest
    {
        public Domain.Entities.Order? entity { get; set; }
    }
}
