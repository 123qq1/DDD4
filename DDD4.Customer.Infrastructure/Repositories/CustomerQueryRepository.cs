using DDD4.Customer.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using DDD4.Customer.Domain.Entities;
using Microsoft.Extensions.Options;

namespace DDD4.Customer.Infrastructure.Repositories
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        // mongo db 
        private readonly IMongoCollection<Domain.Entities.Customer> mongoCollection;

        public CustomerQueryRepository(IOptions<CustomersMongoDbSettings> _customersMongoDbSettings)
        {
            var mongoClient = new MongoClient(_customersMongoDbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_customersMongoDbSettings.Value.DatabaseName);

            _customersMongoDbSettings = mongoDatabase.GetCollection<Domain.Entities.Customer>(_customersMongoDbSettings.Value.CustomersCollectionName);
        }


        // Insert event into mongoDb
        async Task ICustomerQueryRepository.Add(Domain.Entities.Customer customer)
        {

            //MongoClient mongoClient = new MongoClient("connstring");

            var db = mongoClient.GetDatabase("CustomersDocuments");
            var collection = db.GetCollection<BsonDocument>();

            var documentCustomer = new BsonDocument
            {
                { "Id", customer.Id },
                { "Name", customer.Name },
                { "DiscordName", customer.DiscordName },
                { "AccountName", customer.AccountName },
            };

            await collection.InsertOneAsync(documentCustomer);
        }
    }
}
