using Api.Sequrity.Services;
using Domain.Sequrity;
using Infrastructure.Data.DataBaseContext;

namespace Api.Sequrity.Extensions
{
    public static class IdentityOptionExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<CustomIdentityUser>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 1;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication();
            services.AddScoped<IJwtSecurityService, JwtSecurityService>();
            return services;
        }
    }
}
