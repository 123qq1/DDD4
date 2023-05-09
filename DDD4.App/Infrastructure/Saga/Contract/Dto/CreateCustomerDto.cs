namespace DDD4.App.Infrastructure.Saga.Contract.Dto
{
    public class CreateCustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DiscordName { get; set; }
        public string AccountName { get; set; }
        public string LinkingKey { get; set; }
    }
}
