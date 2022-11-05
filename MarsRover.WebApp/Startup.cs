using System.Reflection;
using MarsRover.Domain.Constants;
using MarsRover.Infrastructure.Extension;
using MarsRover.Persistance.Seeds;
using MarsRover.WebApp.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarsRover.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuring and registering appSettings.json
            services.RegisterAppSettings(Configuration);

            // Configuring database
            services.ConfigureDatabase(Configuration);
            

            // Configuring CORS policies
            services.ConfigureCorsPolicy(Configuration, PolicyNames.AllowAll);

            // Configuring database
            services.ConfigureDatabase(Configuration);

            // Seeding data
            services.CheckAndSeedData(Program.IsStartedWithMain);

            // Configuring MediatR
            services.AddAndConfigureMediatR();

            // Configuring services
            services.ConfigureService();

            // Configuring HTTP context
            services.AddHttpContextAccessor();

            

            // Adding Exception filter
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AppExceptionFilterAttribute>();
                options.MaxModelValidationErrors = 50;
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    _ => "The field is required.");

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Using CORS policy
            app.UseCors(PolicyNames.AllowAll);

            // Using Custom logging middleware
            //app.ConfigureCustomExceptionMiddleware();

            // Request logging
            app.ConfigureRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
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
