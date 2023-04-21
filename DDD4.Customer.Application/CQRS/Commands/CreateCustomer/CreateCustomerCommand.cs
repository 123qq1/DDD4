using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DDD4.Customer.Domain.Entities;
using MediatR;

namespace DDD4.Customer.Application.CQRS.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DiscordName { get; set; }
        public string AccountName { get; set; }
    }
}