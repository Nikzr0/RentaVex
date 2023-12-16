namespace RentaVex.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using RentaVex.Data.Models;

    public class ImagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Images.Any())
            {
                return;
            }

            var productId = dbContext.Products.FirstOrDefault()?.Id;
            if (productId == null)
            {
                return;
            }

            var user = dbContext.Users.FirstOrDefault();
            if (user == null)
            {
                return;
            }

            await dbContext.Images.AddAsync(new Image
            {
                AddedByUser = user.Id.ToString(), // Problem
                ImageUrl = "/images/products/b27f9d81-1cbb-4df3-9b20-53e0b254bd6c.jpg",
                Extention = ".jpg",
                ProductId = productId.Value,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
