using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Dot.Net.WebApi
{
    public class Program
    {
        private const string _logTemplate = "[{Timestamp:HH:mm:ss, MMM dd yyyy} {Level:u3}]-{SourceContext}" +
                                   "{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((context, configuration) =>
                    {
                        configuration.ReadFrom.Configuration(context.Configuration, "Logging")
                                     .Enrich.FromLogContext()
                                     .WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate: _logTemplate);

                        if (context.HostingEnvironment.IsProduction())
                            configuration.WriteTo.File(path: @"Log\poseidon-webapi.log",
                                                       rollOnFileSizeLimit: true,
                                                       rollingInterval: RollingInterval.Hour,
                                                       buffered: true,
                                                       restrictedToMinimumLevel: LogEventLevel.Information);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
