namespace RentaVex.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Users.Any())
            {
                return;
            }

            // Check out user seeding in google - problem is password hashing algorithm here
            await dbContext.Users.AddAsync(new Models.ApplicationUser { UserName = "Selena", PasswordHash = "SelenaProdanovapassword1", Email = "selena@gmail.com" });
            await dbContext.Users.AddAsync(new Models.ApplicationUser { UserName = "Nikola", PasswordHash = "NIko2301321028", Email = "n@gpepelovmail.com" });

            await dbContext.SaveChangesAsync();
        }
    }
}