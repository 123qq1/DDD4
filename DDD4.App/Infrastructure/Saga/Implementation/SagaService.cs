using DDD4.App.Infrastructure.Saga.Contract;
using DDD4.App.Infrastructure.Saga.Contract.Dto;

namespace DDD4.App.Infrastructure.Saga.Implementation
{
    public class SagaService : ISagaService
    {
        private readonly HttpClient _httpClient;

        public SagaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        async Task ISagaService.CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var res = await _httpClient.PostAsJsonAsync($"api/Customer", createCustomerDto);

            if (res.IsSuccessStatusCode) return;

            var message = await res.Content.ReadAsStringAsync();
            throw new Exception(message);
        }
    }
}
