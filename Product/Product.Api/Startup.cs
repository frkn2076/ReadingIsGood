using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Product.Api.Mapper;
using Product.Business;
using Product.Business.Implementation;
using Product.DataAccess;
using Product.Repository;
using Product.Repository.Implementation;
using Shared.Extensions;
using Shared.Extensions.Config;
using Shared.Helper;
using Shared.Helper.Config;

namespace Product.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.Api", Version = "v1" }));

            var jwtSettings = Configuration.Bind<JWTSettings>();
            services.JWTRegistration(jwtSettings);

            var postgreSettings = Configuration.Bind<PostgreSettings>();
            var accountDBConnecton = Helper.GetPostgreDatabaseConnection(postgreSettings, "Product");
            services.AddDbContextPool<ProductDBContext>(options => options.UseNpgsql(accountDBConnecton).EnableSensitiveDataLogging());

            var redisSettings = Configuration.Bind<RedisSettings>();
            services.RedisRegistration(redisSettings);

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBusinessManager, BusinessManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product.Api v1"));
            }

            app.MigrateDatabaseAndTables<ProductDBContext>();

            MyMapper.MapsterInit();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
