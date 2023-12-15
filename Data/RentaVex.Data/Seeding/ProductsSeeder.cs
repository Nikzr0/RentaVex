//namespace RentaVex.Data.Seeding
//{
//    using System;
//    using System.Linq;
//    using System.Threading.Tasks;

//    using RentaVex.Data.Models;

//    public class ProductsSeeder : ISeeder
//    {
//        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
//        {
//            if (dbContext.Products.Any())
//            {
//                return;
//            }

//            var user = dbContext.Users.FirstOrDefault();

//            var category = dbContext.Categories.FirstOrDefault();

//            await dbContext.Products.AddAsync(new Product
//            {
//                Name = "Photo",
//                Description = "A really beautiful painting!",
//                Price = 150.00m,
//                Location = "Plovdiv",
//                Contact = "0893452019",
//                Category = category,
//                UserId = user.Id.ToString(), // Check!
//                IsForSale = true,
//                IsForRent = false,
//                PickupTime = DateTime.Now,
//                ReturnTime = DateTime.Now.AddDays(7),
//                CourierDelivery = true,
//                Condition = ConditionType.New,
//                IsWarned = true,
//                WarningMessage = "No warnings",
//            });

//            await dbContext.SaveChangesAsync();
//        }
//    }
//}
