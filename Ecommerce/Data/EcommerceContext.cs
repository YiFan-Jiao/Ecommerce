using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Models;

namespace Ecommerce.Data
{
    public class EcommerceContext : DbContext
    {
        public EcommerceContext (DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public DbSet<Ecommerce.Models.Products> Products { get; set; } = default!;

        public DbSet<Ecommerce.Models.Country> Countrys { get; set; } = default!;

        public DbSet<Ecommerce.Models.Cart> Carts { get; set; } = default!;

        public DbSet<Ecommerce.Models.Order> Orders { get; set; } = default!;

    }
}
