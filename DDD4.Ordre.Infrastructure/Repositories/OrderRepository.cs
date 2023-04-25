using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using EventStore.Client;
using System.Threading.Tasks;
using DDD4.Order.Application.Repositories;

namespace DDD4.Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository 
    {
        private readonly EventStoreClient _eventStoreClient;
        public OrderRepository(EventStoreClient eventStoreClient)
        {
            _eventStoreClient = eventStoreClient;
        }
        async Task IOrderRepository.SaveAsync(Domain.Entities.Order order)
        {
            var events = order.GetChanges()
                .Select(@event => new EventData(
                    Uuid.NewUuid(),
                    @event.GetType().Name,
                    Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event)),
                    Encoding.UTF8.GetBytes(@event.GetType().FullName ?? throw new InvalidOperationException())
                    ))
                .ToArray();

            if (!events.Any()) return;

            var streamName = GetStreamName(order, order.OrderId);

            await _eventStoreClient.AppendToStreamAsync(streamName, StreamState.Any, events);
        }

        async Task<Domain.Entities.Order> IOrderRepository.LoadAsync(Guid orderId)
        {
            if (orderId == Guid.Empty) throw new ArgumentException("Value cannot be null or whitespace.", nameof(orderId));

            var order = new Domain.Entities.Order();
            var streamName = GetStreamName(order, orderId);

            var events = _eventStoreClient.ReadStreamAsync(Direction.Forwards, streamName, StreamPosition.Start);

            if (events.ReadState.Result == ReadState.StreamNotFound) { return order; }

            var loadedFromStore = await events.ToListAsync();

            if (!loadedFromStore.Any()) return order;


            var loadedEvents = loadedFromStore.Select(@event =>
                JsonSerializer.Deserialize(Encoding.UTF8.GetString(@event.OriginalEvent.Data.ToArray()),
                Type.GetType(Encoding.UTF8.GetString(@event.OriginalEvent.Metadata.ToArray())) ?? typeof(object))
            );

            order.Load(
                loadedFromStore.Select(a => a.Event).Last().EventNumber.ToInt64(),
                loadedEvents);

            return order;
        }


        private string GetStreamName<T>(T type, Guid aggregateId)
        {
            return $"{type.GetType().Name}-{aggregateId}";
        }
    }
}
