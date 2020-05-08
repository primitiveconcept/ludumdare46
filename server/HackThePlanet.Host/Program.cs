namespace HackThePlanet.Host
{
    using System;
    using System.IO;
    using System.Net;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;


    internal class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();
            IWebHost host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true")
                .ConfigureLogging(ConfigureLogging)
                .UseKestrel(ConfigureKestrel)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();
            host.Run();

            Console.Out.WriteLine("Game started.");
        }


        private static void ConfigureKestrel(KestrelServerOptions options)
        {
            options.Listen(
                IPAddress.Any,
                31337,
                builder =>
                    {
                        
                    });
        }


        private static void ConfigureLogging(ILoggingBuilder factory)
        {
            factory.AddConsole();
            factory.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);
            factory.AddFilter("HackThePlanet", LogLevel.Debug);
        }
    }
}