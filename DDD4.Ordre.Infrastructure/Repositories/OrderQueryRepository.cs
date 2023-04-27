using DDD4.Order.Application.Repositories;
using DDD4.Order.Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Infrastructure.Repositories
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        // mongo db 
        private readonly IMongoCollection<Domain.Entities.Order> mongoCollection;

        public OrderQueryRepository(IOptions<OrdersMongoDbSettings> _ordersMongoDbSettings)
        {
            var mongoClient = new MongoClient(_ordersMongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_ordersMongoDbSettings.Value.DatabaseName);

            mongoCollection = mongoDatabase.GetCollection<Domain.Entities.Order>(_ordersMongoDbSettings.Value.OrdersCollectionName);
        }
        async Task IOrderQueryRepository.Add(Domain.Entities.Order order)
        {

        }
        async Task<Domain.Entities.Order> IOrderQueryRepository.Read(Guid orderId)
        {

        }
    }
}
