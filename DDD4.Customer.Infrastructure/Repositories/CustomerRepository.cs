using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventStore.Client;
using DDD4.Customer.Domain.Entities;
using DDD4.Customer.Application.Repositories;

namespace DDD4.Customer.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly EventStoreClient _eventStoreClient;

        public CustomerRepository(EventStoreClient eventStoreClient)
        {
            _eventStoreClient = eventStoreClient;
        }

        async Task ICustomerRepository.SaveAsync(Domain.Entities.Customer customer)
        {
            var events = customer.GetChanges()
                .Select(@event => new EventData(
                    Uuid.NewUuid(),
                    @event.GetType().Name,
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)),
                    Encoding.UTF8.GetBytes(@event.GetType().FullName ?? throw new InvalidOperationException())
                    ))
                .ToArray();

            if (!events.Any()) return;

            var streamName = GetStreamName(customer, customer.Id);

            await _eventStoreClient.AppendToStreamAsync(streamName, StreamState.Any, events);
        }

        async Task<Domain.Entities.Customer> ICustomerRepository.LoadAsync(Guid customerId)
        {
            if(customerId == Guid.Empty) throw new ArgumentException("Value cannot be null or whitespace.", nameof(customerId));

            var customer = new Domain.Entities.Customer();
            var streamName = GetStreamName(customer, customerId);

            var events = _eventStoreClient.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);

            if(events.ReadState.Result == ReadState.StreamNotFound) { return customer; }

            var loadedFromStore = await events.ToListAsync(); 

            if(!loadedFromStore.Any()) return customer;


            var loadedEvents = loadedFromStore.Select(@event =>
                JsonSerializer.Deserialize(Encoding.UTF8.GetString(@event.OriginalEvent.Data.ToArray()),
                Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata.ToArray())) ?? typeof(object))
            );

            customer.Load(
                loadedFromStore.Select(a => a.Event).Last().EventNumber.ToInt64(),
                loadedEvents);

            return customer;
        }


        private string GetStreamName<T>(T type, Guid aggregateId) 
        { 
            return $"{type.GetType().Name}-{aggregateId}";
        }
    }
}
