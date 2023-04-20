using DDD4.Customer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.CQRS.Queries
{
    public class CustomerCreated : IRequest
    {
        public Domain.Entities.Customer? entity { get; set; }
    }
}
