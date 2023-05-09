using DDD4.Order.Domain.Events;
using DDD4.Order.Domain.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Domain.Entities
{
    public class Order : Aggregate
    {
        public Guid OrderId { get; private set; }
        public string Name { get; private set; }
        public string DiscordName { get; private set; }
        public string AccountName { get; private set; }
        public ItemValue RoleValue { get; private set; }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case createdOrder x:
                    OnCreated(x);
                    break;
            }
        }

        public void Create(Guid orderId, string customerName, string accountName, string discordName, ItemValue roleValue)
        {
            if (Version >= 0) throw new NotImplementedException();

            Apply(new createdOrder
            {
                OrderId = OrderId,
                Name = customerName,
                AccountName = accountName,
                DiscordName = discordName,
                RoleValue = roleValue
            });
        }

        #region Event Handlers

        private void OnCreated(createdOrder @event)
        {
            OrderId = @event.OrderId;
            Name = @event.Name;
            DiscordName = @event.DiscordName;
            AccountName = @event.AccountName;
            RoleValue = @event.RoleValue;
        }

        #endregion
    }
}
