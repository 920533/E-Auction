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
