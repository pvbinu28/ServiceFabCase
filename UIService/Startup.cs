using Business;
using Business.Interfaces;
using DataModels;
using DataModels.CaseModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http;
using UIService.Resolver;

namespace UIService
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

            #region Resgistering services
            services.Configure<DbSettingsModel>(Configuration.GetSection(nameof(DbSettingsModel)));
            services.AddSingleton<IDbSettings>(sp => sp.GetRequiredService<IOptions<DbSettingsModel>>().Value);
            services.AddTransient<IDataAccess<FraudCaseModel>, DataAccess<FraudCaseModel>>();
            services.AddTransient<IDataAccess<TrafficLightModel>, DataAccess<TrafficLightModel>>();
            services.AddSingleton<HttpClient>(new HttpClient());
            services.AddTransient<IProcessResolver, ProcessorResolver>();
            services.AddTransient<IProcessInvoker, ProcessInvoker>();
            #endregion

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseExceptionHandler("/Error");
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501
                spa.Options.SourcePath = "ClientApp";
            });
        }
    }
}
