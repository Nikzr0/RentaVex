namespace RentaVex.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using RentaVex.Data.Models;

    public class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Products.Any())
            {
                return;
            }

            var category = dbContext.Categories.FirstOrDefault();
            if (category == null)
            {
                return;
            }

            var user = dbContext.Users.FirstOrDefault();
            if (user == null)
            {
                return;
            }

            // Product 1
            var product1 = new Product
            {
                Name = "A tent",
                Description = "A great tent!",
                Price = 150.00m,
                Location = "Plovdiv",
                Contact = "0893452019",
                Category = category,
                UserId = user.Id.ToString(),
                IsForSale = true,
                IsForRent = false,
                PickupTime = DateTime.Now,
                ReturnTime = DateTime.Now.AddDays(7),
                CourierDelivery = true,
                Condition = ConditionType.New,
                IsWarned = true,
                WarningMessage = "No warnings",
            };

            await dbContext.Products.AddAsync(product1);
            await dbContext.SaveChangesAsync();

            var image1 = new Image
            {
                AddedByUser = user.Username,
                ImageUrl = "/images/products/b27f9d81-1cbb-4df3-9b20-53e0b254bd6c.jpg",
                Extention = ".jpg",
                ProductId = product1.Id,
            };

            await dbContext.Images.AddAsync(image1);
            await dbContext.SaveChangesAsync();

            // Product 2
            var product2 = new Product
            {
                Name = "Camping Gear",
                Description = "Essential camping equipment!",
                Price = 200.00m,
                Location = "Sofia",
                Contact = "0888123456",
                Category = category,
                UserId = user.Id.ToString(),
                IsForSale = true,
                IsForRent = false,
                PickupTime = DateTime.Now,
                ReturnTime = DateTime.Now.AddDays(7),
                CourierDelivery = true,
                Condition = ConditionType.Used,
                IsWarned = true,
                WarningMessage = "Example warning message",
            };

            await dbContext.Products.AddAsync(product2);
            await dbContext.SaveChangesAsync();

            var image2 = new Image
            {
                AddedByUser = user.Username,
                ImageUrl = "/images/products/53570dc1-c653-4340-9863-44e323eb9e5f.jpg",
                Extention = ".png",
                ProductId = product2.Id,
            };

            await dbContext.Images.AddAsync(image2);
            await dbContext.SaveChangesAsync();
        }
    }
}
