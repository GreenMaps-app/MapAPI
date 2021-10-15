using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Configuration;

using Microsoft.EntityFrameworkCore;
using MapAPI.Models;

// ensure that NuGet is set up properly on your project
// right click on MapAPI and clikc Manage NuGet packages
// make sure that https://www.nuget.org/api/v2/ is part of a source

// use NuGet to install Swashbuckle.AspNetCore or,
// use command: 'dotnet add package Swashbuckle.AspNetCore --version 5.6.3' for swagger
// Only enter this if the "using Microsoft.OpenApi.Models;" is underlined in red

// additionally, the EntityFrameworkCore requires a package to be installed
// use NuGet for this


namespace MapAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        public static string GetSqlConnectionString(string name)
        {
            string connStr = Environment.GetEnvironmentVariable($"ConnectionStrings:{name}", EnvironmentVariableTarget.Process);
            if (string.IsNullOrEmpty(connStr)) // Azure Functions App Service naming convention
                connStr = Environment.GetEnvironmentVariable($"SQLCONNSTR_{name}", EnvironmentVariableTarget.Process);
            return connStr;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //string connStr = ConfigurationManager.ConnectionStrings["hackathonDBConn"].ConnectionString;
            string connStr = GetSqlConnectionString("hackathonDBConn");
            //Console.WriteLine(connStr);

            services.AddDbContext<DatapointContext>(options => options.UseSqlServer(connStr));

            //services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MapAPI", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MapAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
