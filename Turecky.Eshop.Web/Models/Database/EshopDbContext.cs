using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Turecky.Eshop.Web.Models.Entity;

namespace Turecky.Eshop.Web.Models.Database
{
    public class EshopDbContext : DbContext
    {
        public DbSet<CarouselItem> CarouselItems { get; set; }

        public DbSet<EshopItem> EshopItems { get; set; }

        public EshopDbContext(DbContextOptions options) : base(options) { }
    }
}
