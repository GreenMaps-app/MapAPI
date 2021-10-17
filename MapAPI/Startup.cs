using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Microsoft.EntityFrameworkCore;
using MapAPI.Models;
using MapAPI.Helpers;
using MapAPI.Models.Repository;

// ensure that NuGet is set up properly on your project
// right click on MapAPI and click Manage NuGet packages
// make sure that https://www.nuget.org/api/v2/ is part of a source

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
        public void ConfigureServices(IServiceCollection services)
        {

            /*
             * If local, should be getting the string from Connection.Config, though if
             * deployed to production using the GitHub-Azure CD, then it will get this information
             * through the connection strings configuration within the Web API.
             * The GetSqlConnectionString helper handles this resolving.
             * Also, if running on localhost, ensure that the connection string is added to hackathonDBConn 
             * in Connection.Config, though make sure to remove it before pushing to GitHub as we don't want
             * this information in the repo.
             */
            string connStr = DBHelpers.GetSqlConnectionString("hackathonDBConn");

            services.AddEntityFrameworkSqlServer().AddDbContext<hackathon_dbContext>(options => options.UseSqlServer(connStr));
            services.AddScoped<IMapLocationRepository, MapLocationRepository>();

            services.AddControllers();

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
