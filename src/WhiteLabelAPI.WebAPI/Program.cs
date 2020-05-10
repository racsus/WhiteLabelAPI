using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WhiteLabelAPI
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
                    //TODO. I had to add this line to avoid a problem with the nuget MicroElements.Swashbuckle.FluentValidation
                    //https://github.com/micro-elements/MicroElements.Swashbuckle.FluentValidation/issues/15
                    webBuilder.UseDefaultServiceProvider(options => options.ValidateScopes = false);
                    webBuilder.UseStartup<Startup>();
                });
    }
}
