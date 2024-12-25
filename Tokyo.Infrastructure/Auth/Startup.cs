using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tokyo.Core.Middlewares;
using Tokyo.DomainPersistence.Entities;

namespace Tokyo.Infrastructure.Auth
{
    public static class Startup
    {
        public static IServiceCollection InitializeAuth(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDBContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(config["JWTKey:Secret"]);
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = config["JWTKey:ValidIssuer"],
                    ValidAudience = config["JWTKey:ValidAudience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });
            return services;
        }

        public static IApplicationBuilder UseAuth(this WebApplication app)
        {
            app.UseUnauthorizedMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
