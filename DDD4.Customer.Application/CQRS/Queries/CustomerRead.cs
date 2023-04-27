using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DDD4.Customer.Application.CQRS.Queries
{
    public class CustomerRead : IRequest
    {
        public Domain.Entities.Customer? entity { get; set; }
    }
}
