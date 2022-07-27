using EAuction.Shared.Models;
using EAuction.Shared.Extensions;
using EAuction.Shared.Helpers;
using EAuction.Shared.Interface;
using EAuction.Shared.Processors;
using EAuction.Shared.Seller;
using EAuction.Shared.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Text;
using EAuction.BusinessLogic.Repository;
using EAuction.BusinessLogic.MessageBroker;
using EAuction.DataAccess;
using EAuction.DataMigration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NLog;

namespace EAuction.API
{
    public class Startup
    {

        public static ServiceInformation ServiceInformation { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var appSettingsJson = File.ReadAllText("appsettings.json");
            ServiceInformation = JsonConvert.DeserializeObject<ServiceInformation>(appSettingsJson);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var description = new StringBuilder();
            description.AppendLine($"Environment : {ServiceInformation.Environment} {Environment.NewLine}");
            description.AppendLine($"Build : {ServiceInformation.Build} {Environment.NewLine}");
            description.AppendLine($"Release : {ServiceInformation.Release} {Environment.NewLine}");

            services.AddApiVersioning(x =>
            {
                x.DefaultApiVersion = new ApiVersion(1, 0);
                x.AssumeDefaultVersionWhenUnspecified = true;
                x.ReportApiVersions = true;
            });
            services.AddAutoMapper(typeof(DtoMapper));
            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
            services.RegisterSwagger();

            services.AddControllers();

            var connectionStrings = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(connectionStrings);
            var connectionString = Configuration.GetConnectionString("EAuctionDB");
            services.AddDbContext<AuctionDbContext>(
                options => options.UseSqlServer(connectionString));
            services
                .AddTransient<IUserRepository, UserRepository>()
                .AddTransient<IProductToBuyerRepository, ProductToBuyerRepository>()
                .AddTransient<IProductService, ProductService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IBuyerProcessor, BuyerProcessor>()
                .AddTransient<ISellerProcessor, SellerProcessor>()
                .AddTransient<IValidator<User>, UserValidator>()
                .AddTransient<IValidator<Product>, ProductValidator>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IProductDataAccess, ProductDataAccess>()
                .AddTransient<IUserDataAccess, UserDataAccess>()
                .AddTransient<IServiceBusMessageProducer, ServiceBusMessageProducer>();

            // 
            services.AddHostedService<ServiceBusSubscriber>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (!env.EnvironmentName.Contains("Development"))
            {
                app.UseAzureAppConfiguration();
            }
            app.UseSwaggerService(env);
            var settings = app.ApplicationServices.GetService<IOptions<ConfigurationSettings>>();
            GlobalDiagnosticsContext.Set("app_name", "EAuction");
            GlobalDiagnosticsContext.Set("env", settings.Value.Env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //run any database migrations on startup
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<AuctionDbContext>();
                context.Database.Migrate();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
