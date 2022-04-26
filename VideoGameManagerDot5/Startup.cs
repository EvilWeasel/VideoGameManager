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
            // >>> Register Entity Framework
            /// Connection string needs to be defined inside the Configuration-object,
            /// located inside appsettings.json
            ///            "ConnectionStrings": {
            ///                "DefaultConnection" : "Server=(localDb)\\dev;Database=AddressBook;Trusted_Connection=true"
            ///  },
            services.AddDbContext<VideoGameDataContext>(options => 
                options.UseSqlServer(
                    Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddControllers();
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
        ///     that should be added as middleware here...
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
