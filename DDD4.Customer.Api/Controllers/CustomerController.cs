using DDD4.Customer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using DDD4.Customer.Application.CQRS.Commands;
using DDD4.Customer.Application.Repositories;
using DDD4.Customer.Application.CQRS.Commands.CreateCustomer;
using MediatR;

namespace DDD4.Customer.Api.Controllers
{

    [Route("/api/Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(CreateCustomerCommand createCustomerCommand)
        {
            await _mediator.Send(createCustomerCommand);

            return Ok(); 
        }
    }
}
