using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Customer.Domain.Events
{
    public class createdCustomer
    {
        public Guid customerId { get; set; }
        public string customerName { get; set; }
        public string discordName { get; set; }
        public string accountName { get; set; }
    }
}
