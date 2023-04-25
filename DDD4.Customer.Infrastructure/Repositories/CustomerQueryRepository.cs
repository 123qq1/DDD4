using DDD4.Customer.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
//using MongoDB.Driver;

namespace DDD4.Customer.Infrastructure.Repositories
{
    public class CustomerQueryRepository : ICustomerQueryRepository
    {
        // mongo db 
        async Task ICustomerQueryRepository.Add(Domain.Entities.Customer customer)
        {

            //MongoClient mongoClient = new MongoClient("connstring");
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
