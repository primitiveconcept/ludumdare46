namespace HackThePlanet.Host
{
	using System.IO;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;


	internal class Program
	{
		private static async Task Main(string[] args)
		{
			IHost host = new HostBuilder()
				.ConfigureHostConfiguration(
					configurationHost =>
						{
							configurationHost.SetBasePath(Directory.GetCurrentDirectory());
							configurationHost.AddEnvironmentVariables(prefix: "HACKTHEPLANET_");
							configurationHost.AddCommandLine(args);
						})
				.ConfigureAppConfiguration(
					(hostContext, configurationApp) =>
						{
							configurationApp.SetBasePath(Directory.GetCurrentDirectory());
							configurationApp.AddEnvironmentVariables(prefix: "HACKTHEPLANET_");
							configurationApp.AddJsonFile(
								"appsettings.json",
								true);
							configurationApp.AddJsonFile(
								$"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
								true);
							configurationApp.AddCommandLine(args);
						})
				.ConfigureServices(
					(hostContext, services) =>
						{
							services.AddLogging();
							services.AddHostedService<ServiceHost>();
							services.AddSingleton(typeof(ICommonService), typeof(Game));
						})
				.ConfigureLogging(
					(hostContext, configurationLogging) =>
						{
							configurationLogging.AddConsole();
							configurationLogging.AddDebug();
						})
				.Build();

			await host.RunAsync();
		}
	}
}