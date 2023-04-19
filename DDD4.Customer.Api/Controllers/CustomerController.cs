using DDD4.Customer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using DDD4.Customer.Application.CQRS.Commands;

namespace DDD4.Customer.Api.Controllers
{

    [Route("/api/Customer")]
    public class CustomerController : ControllerBase
    {
        private CustomerRepository _customerRepository;
        private readonly ICustomerCreateCommand _customerCreateCommand;

        public CustomerController(CustomerRepository customerRepository, ICustomerCreateCommand customerCreateCommand)
        {
            _customerRepository = customerRepository;
            _customerCreateCommand = customerCreateCommand;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CustomerCreateRequestDto dto)
        {
            var customer = await _customerRepository.LoadAsync(dto.CustomerId);
            customer.Create(
                dto.CustomerId,
                dto.CustomerName,
                dto.DiscordName,
                dto.AccountName
                );

            await _customerRepository.SaveAsync(customer);

            return Ok(); 
        }
    }
}
