using DDD4.App.Infrastructure.Saga.Contract.Dto;

namespace DDD4.App.Infrastructure.Saga.Contract
{
    public interface ISagaService
    {
        public Task CreateCustomer(CreateCustomerDto createCustomerDto);
    }
}
