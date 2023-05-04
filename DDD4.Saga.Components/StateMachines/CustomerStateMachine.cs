using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Contracts;
using MassTransit;
using MassTransit.Middleware;

namespace DDD4.Saga.Components.StateMachines
{
    public class CustomerStateMachine : 
        MassTransitStateMachine<CustomerState>
    {
        public CustomerStateMachine()
        {

            InstanceState(x => x.CurrentState);

            Event(() => CustomerRecived, x => x.CorrelateById(c => c.Message.CustomerId));
            Event(() => CustomerLinked, x => x.CorrelateById(c => c.Message.CustomerId));


            Initially(  
                    When(CustomerRecived)
                        .PublishAsync(context => context.Init<LinkCustomer>(
                            new
                            {
                                context.Message.CustomerId,
                                context.Message.CustomerName,
                                context.Message.DiscordName,
                                context.Message.AccountName
                            }
                            ))
                        .TransitionTo(Linking)
            );

            During(Linking,
                When(CustomerLinked)
                    .PublishAsync(context => context.Init<CreateCustomer>(
                        new
                        {
                            context.Message.CustomerId,
                            context.Message.CustomerName,
                            context.Message.DiscordName,
                            context.Message.AccountName
                        })
                    )
                    .TransitionTo(Creation)
            );

        }

        public State Linking { get; set; }
        public State Creation { get; set; }
        public State Finished { get; set; }

        public Event<CustomerRecived> CustomerRecived { get; set; }
        public Event<CustomerLinked> CustomerLinked { get; set; }
    }

    public class CustomerStateMachineDefinition :
        SagaDefinition<CustomerState>
    {
        public CustomerStateMachineDefinition()
        {
            ConcurrentMessageLimit = 8;
        }

        protected override void ConfigureSaga(IReceiveEndpointConfigurator endpointConfigurator, ISagaConfigurator<CustomerState> sagaConfigurator)
        {
            sagaConfigurator.UseMessageRetry(r => r.Immediate(5));
            sagaConfigurator.UseInMemoryOutbox();

            var partition = endpointConfigurator.CreatePartitioner(8);

            sagaConfigurator.Message<CustomerRecived>(x => x.UsePartitioner(partition, m => m.Message.CustomerId));

        }
    }
}
