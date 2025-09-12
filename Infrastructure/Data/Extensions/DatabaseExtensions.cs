
using Domain.Sequrity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<CustomIdentityUser>>();
            await dbContext.Database.MigrateAsync();

            await SeedData(dbContext, userManager);

        }
        private static async Task SeedData(ApplicationDbContext dbContext, UserManager<CustomIdentityUser> userManager)
        {
            await SeedTopicsAsync(dbContext);
            await SeedIdentityUserAsync(dbContext, userManager);
        }

        private static async Task SeedTopicsAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Topics.AnyAsync())
            {
                await dbContext.Topics.AddRangeAsync(InitialData.Topics);
                await dbContext.SaveChangesAsync();
            }
        }
        private static async Task SeedIdentityUserAsync(ApplicationDbContext dbContext, UserManager<CustomIdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                foreach ( var user in InitialData.Users)
                {
                    await userManager.CreateAsync(user, "1111");
                }
            }
        }
    }
}
