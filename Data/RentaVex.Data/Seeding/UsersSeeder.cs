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

            await dbContext.Users.AddAsync(new Models.User { Username = "user1", Password = "userpassword1", Email = "user1@gmail.com" });

            await dbContext.SaveChangesAsync();
        }
    }
}