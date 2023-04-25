using DDD4.Customer.Domain.Entities;
using MongoDB.Driver;
namespace DDD4.Customer.Api.DbContext
{
    public interface ICustomerMongoDbContext
    {
        IMongoCollection<Domain.Entities.Customer> CustomersDocuments { get;}
    }
}
