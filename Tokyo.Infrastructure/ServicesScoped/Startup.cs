using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tokyo.Core.Interfaces;
using Tokyo.Core.Services;

namespace Tokyo.Infrastructure.ServicesScoped
{
    public static class Startup
    {
        public static IServiceCollection InitializeRequiredService(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
