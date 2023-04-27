using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Contracts;
using DDD4.Customer.Application.CQRS.Commands;
using DDD4.Customer.Application.CQRS.Commands.CreateCustomer;
using MassTransit;
using MediatR;

namespace DDD4.Customer.Infrastructure.Consumers
{
    public class CreateCustomerConsumer : IConsumer<CreateCustomer>
    {

        private readonly IMediator _mediator;

        public CreateCustomerConsumer(IMediator command)
        {
            _mediator = command;
        }

        public async Task Consume(ConsumeContext<CreateCustomer> context)
        {

            CreateCustomerCommand command = new CreateCustomerCommand
            {
                CustomerId = context.Message.CustomerId,
                CustomerName = context.Message.CustomerName,
                AccountName = context.Message.AccountName,
                DiscordName = context.Message.DiscordName
            };

            await _mediator.Send(command);
        }
    }
}
