using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.SeedData
{
    public static class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            var context = new EcommerceContext(serviceProvider.GetRequiredService<DbContextOptions<EcommerceContext>>());

            context.Database.EnsureDeleted();
            context.Database.Migrate();

             
            if (!context.Products.Any())
            {

                context.Products.AddRange(
                    new Products
                    {
                        GUID = Guid.NewGuid(),
                        Name = "Product 1",
                        Description = "Description for Product 1",
                        AvailableQuantity = 10,
                        PriceCAD = 19.99m
                    },
                    new Products
                    {
                        GUID = Guid.NewGuid(),
                        Name = "Product 2",
                        Description = "Description for Product 2",
                        AvailableQuantity = 15,
                        PriceCAD = 24.99m
                    },
                    new Products
                    {
                        GUID = Guid.NewGuid(),
                        Name = "Product 3",
                        Description = "Description for Product 3",
                        AvailableQuantity = 5,
                        PriceCAD = 9.99m
                    },
                    new Products
                    {
                        GUID = Guid.NewGuid(),
                        Name = "Product 4",
                        Description = "Description for Product 4",
                        AvailableQuantity = 8,
                        PriceCAD = 14.99m
                    },
                    new Products
                    {
                        GUID = Guid.NewGuid(),
                        Name = "Product 5",
                        Description = "Description for Product 5",
                        AvailableQuantity = 12,
                        PriceCAD = 29.99m
                    }
                );
                context.SaveChanges();
            }

            if (!context.Countrys.Any())
            {
                context.Countrys.AddRange(
                    new Country
                    {
                        CountryName = "Canada",
                        ConversionRate = 1.0m, 
                        TaxRate = 0.07
                    },
                    new Country
                    {
                        CountryName = "USA",
                        ConversionRate = 0.80m, 
                        TaxRate = 0.05 
                    },
                    new Country
                    {
                        CountryName = "UK",
                        ConversionRate = 1.25m, 
                        TaxRate = 0.06 
                    }
                );
                context.SaveChanges();
            }

            await context.SaveChangesAsync();
        }
    }
}
