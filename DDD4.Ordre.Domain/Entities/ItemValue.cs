using DDD4.Order.Domain.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDD4.Order.Domain.Entities
{
    public class ItemValue : BaseValueObject
    {
        public string Role { get; private set; }
        public int RoleCost { get; private set; }

        public ItemValue(string role, int roleCost) 
        {
            if(string.IsNullOrEmpty(role)) throw new ArgumentNullException(nameof(role));

            Role = role;
            RoleCost = roleCost;
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Role;
            yield return RoleCost;
        }
    }
}
