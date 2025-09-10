using Api.Exceptions.Handler;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            services.AddOpenApi();

            services.AddCors(options =>
            {
                options.AddPolicy("react-policy", policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:3000");
                });
            });
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication application) 
        {
            // Configure the HTTP request pipeline.
            if (application.Environment.IsDevelopment())
            {
                application.MapOpenApi();
            }
            application.UseCors("react-policy");
            application.UseExceptionHandler(options => { });
            application.UseHttpsRedirection();

            application.UseAuthorization();

            application.MapControllers();
            return application;
        }
    }
}
