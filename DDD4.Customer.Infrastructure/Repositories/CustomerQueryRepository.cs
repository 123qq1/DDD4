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
using DDD4.Customer.Infrastructure.Config;

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

            mongoCollection = mongoDatabase.GetCollection<Domain.Entities.Customer>(_customersMongoDbSettings.Value.CustomersCollectionName);
        }


        // Insert event into mongoDb
        async Task ICustomerQueryRepository.Add(Domain.Entities.Customer customer)
        {

            //MongoClient mongoClient = new MongoClient("connstring");

            //var documentCustomer = new BsonDocument
            //{
            //    { "Id", customer.Id },
            //    { "Name", customer.Name },
            //    { "DiscordName", customer.DiscordName },
            //    { "AccountName", customer.AccountName },
            //};

            await mongoCollection.InsertOneAsync(customer);
        }

        async Task<Domain.Entities.Customer> ICustomerQueryRepository.Read(Guid customerId) 
        { 
            var cursor = await mongoCollection.FindAsync(x => x.Id == customerId);
            return cursor.Single();
            
            /*
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
            */
        }
    }
}
