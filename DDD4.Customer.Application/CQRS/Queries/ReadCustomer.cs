using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.CQRS.Queries
{
    public class ReadCustomer : IRequest
    {
        public Guid customerId { get; set; }
    }
}
