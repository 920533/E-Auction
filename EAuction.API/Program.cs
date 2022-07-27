using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using NLog;
using NLog.Web;
using LogLevel = NLog.LogLevel;
namespace EAuction.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LogFactory logFactory =  NLogBuilder.ConfigureNLog("nlog.config");
            Logger logger = logFactory.GetCurrentClassLogger();
            try
            {
                logger.Log(LogLevel.Debug, "Starting App");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex,"Stopped program due to exception");
               
            }
            finally
            {
                logger.Log(LogLevel.Debug, "Close App");
                LogManager.Shutdown();

            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(loggerBuilder => {
                loggerBuilder.ClearProviders();
            })
            .UseNLog()
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((context, config) => {
                var root = config.Build();
                if (context.HostingEnvironment.EnvironmentName.ToUpperInvariant().Contains("DEVELOPMENT"))
                {
                    return;
                }
                var keyVaultUri = $"https://{root["KeyVault:Vault"]}.vault.azure.net/";
                var ClientId = root["KeyVault.ClientId"];
                var ClientSecret = root["KeyVault.ClientSecret"];
                config.AddAzureKeyVault(keyVaultUri,ClientId,ClientSecret);
        });
    }
}
