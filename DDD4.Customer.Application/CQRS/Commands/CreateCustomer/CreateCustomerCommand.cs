using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace DDD4.Customer.Application.CQRS.Commands.CreateCustomer
{
    public sealed record CreateCustomerCommand(
        Guid CustomerId,
        string CustomerName,
        string DiscordName,
        string AccountName) : IRequest;
}