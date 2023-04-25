using DDD4.Order.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Application.CQRS.Commands.CreateCustomer
{
    public class CreateOrderCommand : IRequest
    {
        public Guid OrderId { get; set; }
        public string CustomerName { get; set; }
        public string DiscordName { get; set; }
        public string AccountName { get; set; }
        public ItemValue RoleValue { get; set; }
    }
}
