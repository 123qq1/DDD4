using DDD4.Customer.Api.Config;
using DDD4.Customer.Domain.Entities;
using MongoDB.Driver;

namespace DDD4.Customer.Api.DbContext
{
    public class CustomerMongoDbContext : ICustomerMongoDbContext
    {
        private readonly IMongoDatabase _database;

        public CustomerMongoDbContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _database = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Domain.Entities.Customer> CustomersDocuments => _database.GetCollection<Domain.Entities.Customer>("CustomersDocuments");
    }
}
