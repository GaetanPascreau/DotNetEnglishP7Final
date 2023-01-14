using System;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Dot.Net.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Add Logging right from the start, by adding the appsettings.json file ( where Serilog is configured) to the configuration at build time.
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Create the (Serilog) logger using the configuration from above.
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            // Add a wrap around the application starter and add a first log using Serilog logger.
            try
            {
                Log.Warning("Application Starting Up on {Date} at {Time} (UTC)", DateTime.UtcNow.ToString("dddd, MMMM d, yyyy", CultureInfo.InvariantCulture), DateTime.UtcNow.ToString("h:mm:ss tt", CultureInfo.InvariantCulture));
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The Application Failed to Start Correctly.");
            }
            // No matter what happened before, we need to close the logger.
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //Use Serilog instead of the built-in logging system
                .UseSerilog()
                //// The commented code below is just in case we want to use the built in .NetCore logging system, and have a control on its configuration.
                //.ConfigureLogging((context, logging) =>
                //{
                //    logging.ClearProviders();
                //    logging.AddConfiguration(context.Configuration.GetSection("Logging"));
                //    logging.AddDebug();
                //    logging.AddConsole();
                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
