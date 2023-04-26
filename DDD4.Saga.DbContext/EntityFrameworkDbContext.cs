using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDD4.Saga.Components.StateMachines;
using Microsoft.EntityFrameworkCore;


namespace DDD4.Saga.DbContext
{
    public class EntityFrameworkDbContext : Microsoft.EntityFrameworkCore.DbContext
    {


        public EntityFrameworkDbContext(DbContextOptions<EntityFrameworkDbContext> options) : base(options)
        {
                
        }

        public DbSet<CustomerState> CustomerStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerStateConfiguration());
        }

    }
}
