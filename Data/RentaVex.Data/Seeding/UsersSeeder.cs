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

            await dbContext.Users.AddAsync(new Models.User { Username = "Selena", Password = "SelenaProdanovapassword1", Email = "selena@gmail.com" });
            await dbContext.Users.AddAsync(new Models.User { Username = "Nikola", Password = "NIko2301321028", Email = "n@gpepelovmail.com" });

            await dbContext.SaveChangesAsync();
        }
    }
}