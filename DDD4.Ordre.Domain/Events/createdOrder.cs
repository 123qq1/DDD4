using DDD4.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Domain.Events
{
    public class createdOrder
    {
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public string DiscordName { get; set; }
        public string AccountName { get; set; }
        public ItemValue RoleValue { get; set; }
    }
}
