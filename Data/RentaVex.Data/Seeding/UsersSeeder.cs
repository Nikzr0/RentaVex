namespace RentaVex.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using RentaVex.Data.Models;

    public class UsersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            if (dbContext.Users.Any())
            {
                return;
            }

            await SeedUserAsync(userManager, "user1@gmail.com", "Username1", "password123");
            await SeedUserAsync(userManager, "user2@gmail.com", "Username2", "password456");
            await SeedUserAsync(userManager, "user3@gmail.com", "Username3", "password789");

            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedUserAsync(UserManager<User> userManager, string email, string username, string password)
        {
            var user = new User
            {
                Email = email,
                UserName = username,
            };

            var result = await userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // Additional user-specific seeding logic -->> optional
            }
            else
            {
                throw new Exception($"Error creating user: {string.Join(", ", result.Errors)}");
            }
        }
    }
}
