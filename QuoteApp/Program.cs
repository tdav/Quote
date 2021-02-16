using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Formatting.Json;

namespace QuoteServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(x =>
                    {
                        x.UseStartup<Startup>();
                    })
                .UseSerilog((hostingContext, services, loggerConfiguration) => loggerConfiguration
                       .ReadFrom.Configuration(hostingContext.Configuration)
                       .WriteTo.Console()
                       .WriteTo.File(new JsonFormatter(), "logs/log.json", rollingInterval: RollingInterval.Day));
                      
    }
}
