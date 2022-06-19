using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistanceMatrix.Repositories;
using DistanceMatrix.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;

namespace DistanceMatrix
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DistanceMatrix", Version = "v1" });
            });
            services.AddScoped<IEnvironmentRepository, EnvironmentRepository>();
            services.AddScoped<IEnvironmentService, EnvironmentService>();
            services.AddSingleton(GraphDatabase.Driver(
                Configuration.GetSection("NEO4J_URI").Value,
                AuthTokens.Basic(
                    Configuration.GetSection("NEO4J_USER").Value,
                    Configuration.GetSection("NEO4J_PASSWORD").Value
                )
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DistanceMatrix v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}