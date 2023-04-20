using DDD4.Customer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using DDD4.Customer.Application.CQRS.Commands;
using DDD4.Customer.Application.Repositories;

namespace DDD4.Customer.Api.Controllers
{

    [Route("/api/Customer")]
    public class CustomerController : ControllerBase
    {
        //private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerCreateCommand _createCommand;

        public CustomerController(/*ICustomerRepository customerRepository, */ ICustomerCreateCommand createCommand)
        {
            //_customerRepository = customerRepository;
            _createCommand = createCommand;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CustomerCreateRequestDto dto)
        {
            //var customer = await _customerRepository.LoadAsync(dto.CustomerId);

            //customer.Create(
            //    dto.CustomerId,
            //    dto.CustomerName,
            //    dto.DiscordName,
            //    dto.AccountName
            //    );

            //await _customerRepository.SaveAsync(customer);

            await _createCommand.Create(dto);

            return Ok(); 
        }
    }
}
