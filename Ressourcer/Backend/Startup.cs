using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
//using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using F20ITONK.ASPNETCore31.Microservice.Backend.Data;

namespace F20ITONK.ASPNETCore31.Microservice.Backend
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
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        //    Cycles in Domaine model
        //    https://stackoverflow.com/questions/58846765/cycle-in-database
        //https://stackoverflow.com/questions/55787018/net-core-3-preview-4-addnewtonsoftjson-is-not-defined
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //services.AddDbContext<F20ITONKASPNETCore31MicroserviceBackendContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("F20ITONKASPNETCore31MicroserviceBackendContext")));
            services.AddDbContext<F20ITONKASPNETCore31MicroserviceBackendContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("F20ITONKASPNETDockerConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, F20ITONKASPNETCore31MicroserviceBackendContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Do "Update-Database" runtime. Requires an "Add-Migration" done first
            db.Database.Migrate(); 

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; //Hint set swagger in root!
            });

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
