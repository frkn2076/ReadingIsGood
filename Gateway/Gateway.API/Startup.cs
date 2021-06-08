using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.Extensions;
using Shared.Extensions.Config;

namespace Gateway.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(/*c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway.Api", Version = "v1" })*/);

            var jwtSettings = Configuration.Bind<JWTSettings>();
            services.JWTRegistration(jwtSettings);

            services.AddSwaggerForOcelot(Configuration,
                (o) =>
                {
                    o.GenerateDocsForAggregates = true;
                });

            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway.Api v1"));
            }

            app.UseSwaggerForOcelotUI(opt => opt.PathToSwaggerGenerator = "/swagger/docs");

            app.UseRouting();

            app.UseAuthentication();

            app.UseOcelot();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
