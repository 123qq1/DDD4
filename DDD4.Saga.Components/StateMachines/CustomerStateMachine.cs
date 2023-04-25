﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Contracts;
using MassTransit;

namespace DDD4.Saga.Components.StateMachines
{
    public class CustomerStateMachine : MassTransitStateMachine<CustomerState>
    {
        public CustomerStateMachine()
        {

            InstanceState(x => x.CurrentState);

            Initially(  
                    When(CustomerRecived)
                        .PublishAsync(context => context.Init<LinkCustomer>(
                            new
                            {

                            }
                            ))
                        .TransitionTo(Linking)
            );

        }

        public State Recived { get; set; }
        public State Linking { get; set; }
        public State Creation { get; set; }
        public State Finished { get; set; }

        public Event<CustomerRecived> CustomerRecived { get; set; }
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
        }
    }
}