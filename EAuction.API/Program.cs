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

namespace EAuction.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
            //.ConfigureAppConfiguration((context, config) => {
            //    var root = config.Build();
            //    if (context.HostingEnvironment.EnvironmentName.ToUpperInvariant().Contains("LOCAL"))
            //    {
            //        return;
            //    }
            //    var keyVaultUri = $"https://{root["KeyVaultSettings:VaultName"]}.vault.azure.net/";
            //    var keyVaultReloadTimeout = Convert.ToInt32($"{root["KeyVaultSettings.ReloadTimeout"]}");
            //    config.AddAzureKeyVault(
            //       new Uri(keyVaultUri),
            //       new DefaultAzureCredential(),
            //       new AzureKeyVaultConfigurationOptions
            //       {
            //           ReloadInterval = TimeSpan.FromMinutes(keyVaultReloadTimeout)
            //       }) ;
            //});
    }
}
