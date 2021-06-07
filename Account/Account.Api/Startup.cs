using Account.Api.Mapper;
using Account.Business;
using Account.Business.Implementations;
using Account.DataAccess;
using Account.Repository;
using Account.Repository.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Shared.Extensions;
using Shared.Extensions.Config;
using Shared.Helper;
using Shared.Helper.Config;

namespace Account.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Account.Api", Version = "v1" }));

            var jwtSettings = Configuration.Bind<JWTSettings>();
            services.JWTRegistration(jwtSettings);
            services.AddSingleton(jwtSettings);

            var postgreSettings = Configuration.Bind<PostgreSettings>();
            var accountDBConnecton = Helper.GetPostgreDatabaseConnection(postgreSettings, "Account");
            services.AddDbContextPool<AccountDBContext>(options => options.UseNpgsql(accountDBConnecton).EnableSensitiveDataLogging());

            var redisSettings = Configuration.Bind<RedisSettings>();
            services.RedisRegistration(redisSettings);

            services.AddScoped<IRegisterRepository, RegisterRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IBusinessManager, BusinessManager>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Account.Api v1"));
            }

            app.MigrateDatabaseAndTables<AccountDBContext>();

            MyMapper.MapsterInit();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
