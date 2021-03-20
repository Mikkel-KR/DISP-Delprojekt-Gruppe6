using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Frontend
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
            services.AddControllersWithViews();

            //Change JSON packager https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-apis/
            services.AddControllers().AddNewtonsoftJson();

            //services.AddDbContext<ApplicationDbContextFrontend>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("localDb")));

            //Enable DIP for a HTTP Client to subsitute calls to DBContex with a HTTP Request
            //services.AddHttpClient();

            _ = services.AddHttpClient("backend", c =>
            {
                var host = Configuration["F20ITONKBACKENDJRT_SERVICE_HOST"];
                var port = Configuration["F20ITONKBACKENDJRT_PORT_8080_TCP_PORT"];
                //c.BaseAddress = new Uri("http://10.24.128.200:32787/"); //local test connection to backend container
                //c.BaseAddress = new Uri("http://10.192.57.127:32787/"); //local test connection to backend container
                c.BaseAddress = new Uri("http://localhost:23089/"); //local IIS backend connection (DEBUG)

                //Remark below not using https but http
                //c.BaseAddress = new Uri("http://" + host + ":" + port + "/"); //Using environment variables
                //c.BaseAddress = new Uri("http://f20itonkbackendjrt:8080/"); //Hard coded K8s Service name
                //c.BaseAddress = new Uri("http://146.148.126.255:8080/");//External K8s Service (LoadBalancer)
                c.DefaultRequestHeaders.Add("Accept", "application/json");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
