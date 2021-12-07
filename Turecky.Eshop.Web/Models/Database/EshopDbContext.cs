using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Database.Configuration;
using Turecky.Eshop.Web.Models.Entity;
using Turecky.Eshop.Web.Models.Identity;

namespace Turecky.Eshop.Web.Models.Database
{
    public class EshopDbContext : IdentityDbContext<User, Role, int>
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }

        public DbSet<EshopItem> EshopItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public EshopDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new OrderConfiguration());
            
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.GetTableName().Replace("AspNet", String.Empty));
            }
        }
    }
}
