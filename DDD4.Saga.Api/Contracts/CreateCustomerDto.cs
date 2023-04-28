namespace DDD4.Saga.Api.Contracts
{
    public class CreateCustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DiscordName { get; set; }
        public string AccountName { get; set; }
    }
}
