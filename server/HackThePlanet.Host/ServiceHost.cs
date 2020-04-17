namespace HackThePlanet.Host
{
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Hosting;
	using Microsoft.Extensions.Logging;


	public class ServiceHost : IHostedService
	{
		#pragma warning disable CS0618
		
		private readonly IApplicationLifetime appLifetime;
		private readonly ILogger<ServiceHost> logger;
		
		private IHostingEnvironment environment;
		private IConfiguration configuration;
		private ICommonService commonService;


		#region Constructors
		public ServiceHost(  
			IConfiguration configuration,  
			IHostingEnvironment environment,  
			ILogger<ServiceHost> logger,  
			IApplicationLifetime appLifetime,  
			ICommonService commonService)  
		{  
			this.configuration = configuration;  
			this.logger = logger;  
			this.appLifetime = appLifetime;  
			this.environment = environment;  
			this.commonService = commonService;  
		}
		#endregion


		public Task StartAsync(CancellationToken cancellationToken)
		{
			this.logger.LogInformation("StartAsync method called.");

			this.appLifetime.ApplicationStarted.Register(OnStarted);
			this.appLifetime.ApplicationStopping.Register(OnStopping);
			this.appLifetime.ApplicationStopped.Register(OnStopped);

			return Task.CompletedTask;
		}


		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}


		private void OnStarted()
		{
			this.commonService.OnStart();
		}


		private void OnStopped()
		{
			this.commonService.OnStopped();
		}


		private void OnStopping()
		{
			this.commonService.OnStopping();
		}
	}
}