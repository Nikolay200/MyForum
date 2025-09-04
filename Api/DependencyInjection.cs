
namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication application) 
        {
            // Configure the HTTP request pipeline.
            if (application.Environment.IsDevelopment())
            {
                application.MapOpenApi();
            }

            application.UseHttpsRedirection();

            application.UseAuthorization();

            application.MapControllers();
            return application;
        }
    }
}
