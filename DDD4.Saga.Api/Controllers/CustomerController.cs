using DDD4.Contracts;
using DDD4.Saga.Api.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DDD4.Saga.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public CustomerController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/<CustomerController>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public void Post(CreateCustomerDto customerDto)
        {
            var customerRecivedEvent = new 
            {
                CustomerId = customerDto.CustomerId,
                CustomerName = customerDto.CustomerName,
                DiscordName = customerDto.DiscordName,
                AccountName = customerDto.AccountName,
            };

            _publishEndpoint.Publish<CustomerRecived>(customerRecivedEvent);
        }

    }
}
