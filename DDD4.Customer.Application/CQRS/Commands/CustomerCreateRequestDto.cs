using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Application.CQRS.Commands
{
    public class CustomerCreateRequestDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DiscordName { get; set; }
        public string AccountName { get; set; }
    }
}
