using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;

namespace DDD4.Saga.Components.StateMachines
{
    public class CustomerState : SagaStateMachineInstance
    {
        public string CurrentState { get; set; }

        public Guid CorrelationId { get; set; }

    }
}
