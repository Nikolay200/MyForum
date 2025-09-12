using Api.Exceptions.Handler;
using Api.Sequrity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
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
            services.AddMediatR(config =>config
            .RegisterServicesFromAssembly(typeof(GetTopicsHandler).Assembly));

            services.AddIdentityServices(configuration);
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
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

            application.UseAuthentication();
            application.UseAuthorization();

            application.MapControllers();
            return application;
        }
    }
}
