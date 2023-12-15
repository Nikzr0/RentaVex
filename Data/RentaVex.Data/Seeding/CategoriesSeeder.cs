namespace RentaVex.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Vehicles" });

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Properties" });

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Electronics" });

            await dbContext.Categories.AddAsync(new Models.Category { Name = "Camping Equipement" });

            await dbContext.SaveChangesAsync();
        }
    }
}
