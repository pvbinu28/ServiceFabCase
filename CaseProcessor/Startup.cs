using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Business.Commands;
using Business.Interfaces;
using DataModels;
using DataModels.CaseModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CaseProcessor
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
            var tes = Configuration.GetSection(nameof(DbSettingsModel));

            services.Configure<DbSettingsModel>(Configuration.GetSection(nameof(DbSettingsModel)));
            services.AddSingleton<IDbSettings>(sp => sp.GetRequiredService<IOptions<DbSettingsModel>>().Value);

            #region Model Deps
            services.AddTransient<IDataAccess<CaseBaseModel>, DataAccess<CaseBaseModel>>();
            services.AddTransient<IOrderService<CaseBaseModel>, OrderService<CaseBaseModel>>();
            services.AddTransient<ICommandResolver<SaveCommand<CaseBaseModel>, CaseBaseModel>, SaveCommandResolver<SaveCommand<CaseBaseModel>, CaseBaseModel>>();
            services.AddTransient<ICommandResolver<UpdateCommand<CaseBaseModel>, CaseBaseModel>, UpdateCommandResolver<UpdateCommand<CaseBaseModel>, CaseBaseModel>>();
            services.AddTransient<ICommandResolver<DeleteCommand<CaseBaseModel>, CaseBaseModel>, DeleteCommandResolver<DeleteCommand<CaseBaseModel>, CaseBaseModel>>();

            services.AddTransient<IDataAccess<FraudCaseModel>, DataAccess<FraudCaseModel>>();
            services.AddTransient<IOrderService<FraudCaseModel>, OrderService<FraudCaseModel>>();
            services.AddTransient<ICommandResolver<SaveCommand<FraudCaseModel>, FraudCaseModel>, SaveCommandResolver<SaveCommand<FraudCaseModel>, FraudCaseModel>>();
            services.AddTransient<ICommandResolver<UpdateCommand<FraudCaseModel>, FraudCaseModel>, UpdateCommandResolver<UpdateCommand<FraudCaseModel>, FraudCaseModel>>();
            services.AddTransient<ICommandResolver<DeleteCommand<FraudCaseModel>, FraudCaseModel>, DeleteCommandResolver<DeleteCommand<FraudCaseModel>, FraudCaseModel>>();

            services.AddTransient<IDataAccess<TrafficLightModel>, DataAccess<TrafficLightModel>>();
            services.AddTransient<IOrderService<TrafficLightModel>, OrderService<TrafficLightModel>>();
            services.AddTransient<ICommandResolver<SaveCommand<TrafficLightModel>, TrafficLightModel>, SaveCommandResolver<SaveCommand<TrafficLightModel>, TrafficLightModel>>();
            services.AddTransient<ICommandResolver<UpdateCommand<TrafficLightModel>, TrafficLightModel>, UpdateCommandResolver<UpdateCommand<TrafficLightModel>, TrafficLightModel>>();
            services.AddTransient<ICommandResolver<DeleteCommand<TrafficLightModel>, TrafficLightModel>, DeleteCommandResolver<DeleteCommand<TrafficLightModel>, TrafficLightModel>>();

            #endregion

            #region CommandManager
            services.AddTransient<ICommandManager, CommandManager>();
            #endregion

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
