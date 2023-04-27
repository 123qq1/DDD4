using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD4.Saga.Components.StateMachines
{
    public class CustomerStateConfiguration : IEntityTypeConfiguration<CustomerState>
    {
        public void Configure(EntityTypeBuilder<CustomerState> builder)
        {
            builder.HasKey(c => c.CorrelationId);
        }
    }
}
