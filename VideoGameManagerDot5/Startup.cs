using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameManagerDot5.DataAccess;

namespace VideoGameManagerDot5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Here we setup 'Dependency Injection':
        /// We register classes (business logic, like dbAccess) here so we can use it in all other
        /// locations of the server, without having to create the same class every time
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            /// Registering services/dependencies
            /// We use the AddDbContext on the services-object and pass our own context class as the type (which we
            /// derived from the 'DbContext' inside 'VideoGameDataContext')
            /// Inside the function 'AddDbContext' we use a lamda-function to create the connection to the sql-server
            /// with 'UseSqlServer' (could be diffrent for other database-types) and inside the function call,
            /// we just need to provide the sql-connection string, which we get out of the Configuration interface,
            /// which just points to appsettings.json
            // >>> Register Dependencies, eg: Entity Framework
            /// Connection string needs to be defined inside the Configuration-object,
            /// located inside appsettings.json
            ///            "ConnectionStrings": {
            ///                "DefaultConnection" : "Server=(localDb)\\MSSQLLocalDB;Database=AddressBook;Trusted_Connection=true"
            ///  },
            ///  The "MSSQLLocalDB"-Part needs to be specific to the name of the SQL-Server Instance
            ///  You can find out which instance name you have by opening a terminal and
            ///  type the following command 'sqllocaldb i'
            ///  If the instance is not started automatically, you can start it by
            ///  typing `sqllocaldb start <instanceName>`
            services.AddDbContext<VideoGameDataContext>(options => 
                options.UseSqlServer(
                    Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddControllers();
            // This nifty package auto-generates a documentation-site, with which we can call our
            // API-Endpoints without having to use something like postman
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VideoGameManagerDot5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// It sets up the pipeline, which incoming http-requests have to pass through,
        /// to inject middleware. Middleware is used to change the flow of the incoming http-requests,
        /// for example checking for authorization.
        /// Basically: When you need to add fundamental features, you need in multiple places in your app,
        /// where you read or modify the request: that should be added as middleware here...
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Swagger is responsible for the nice, auto-generated debug-ui webinterface
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "VideoGameManagerDot5 v1"));
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
