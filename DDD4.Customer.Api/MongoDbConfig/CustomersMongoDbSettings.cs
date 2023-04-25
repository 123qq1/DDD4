namespace DDD4.Customer.Api.MongoDbConfig
{
    public class CustomersMongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!; 
        public string CustomersCollectionName { get; set; } = null!;
    }
}
