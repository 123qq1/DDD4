using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Contracts;
using MassTransit;

namespace DDD4.Saga.Components.Consumers
{
    public class LinkCustomer : IConsumer<Contracts.LinkCustomer>
    {
        public Task Consume(ConsumeContext<Contracts.LinkCustomer> context)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
