using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shared.Extensions.Config;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Shared.Extensions
{
    public static class Extension
    {
        public static void MigrateDatabaseAndTables<T>(this IApplicationBuilder app) where T : DbContext
        {
            try
            {
                using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
                var context = (DbContext)serviceScope.ServiceProvider.GetRequiredService<T>();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                //log
            }
        }

        public static T Bind<T>(this IConfiguration configuration) where T : new()
        {
            T response = new T();
            configuration.Bind(typeof(T).Name, response);
            return response;
        }

        public static void RedisRegistration(this IServiceCollection service, RedisSettings redisSettings)
        {
            service.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "SessionAndCache";
                options.Configuration = $"{redisSettings.Host}:{redisSettings.Port}";
            });

            service.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(redisSettings.ExpiresIn));
        }

        public static void JWTRegistration(this IServiceCollection service, JWTSettings jwtSettings)
        {
            var signingKey = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            service.AddAuthentication(o => o.DefaultAuthenticateScheme = jwtSettings.Scheme);

            service.AddAuthentication()
                .AddJwtBearer(jwtSettings.Scheme, x =>
                {
                    x.SaveToken = true;
                    x.RequireHttpsMetadata = false;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(signingKey),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        public static string GetClaim(this ClaimsPrincipal user, string key)
        {
            var identity = user.Identity as ClaimsIdentity;
            return identity?.FindFirst(key)?.Value;
        }
    }
}
