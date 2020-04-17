namespace HackThePlanet
{
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;

	public class GameService : ICommonService
	{
		private IConfiguration configuration;
		private ILogger<GameService> logger;

		private Game game;
		private GameWebSocket gameWebSocket;
		private Task gameTask;
		private CancellationTokenSource gameCancellationTokenSource  = new CancellationTokenSource();

		#region Constructors
		public GameService(
			IConfiguration configuration,
			ILogger<GameService> logger)
		{
			this.configuration = configuration;
			this.logger = logger;
			logger.LogInformation("Standard service instantiated.");
		}
		#endregion


		#region Properties
		public IConfiguration Configuration
		{
			get { return this.configuration; }
		}


		public ILogger<GameService> Logger
		{
			get { return this.logger; }
		}
		#endregion


		public void OnStart()
		{
			this.Logger.LogInformation("Starting game service.");
			this.game = new Game();
			
			CancellationToken cancellationToken = this.gameCancellationTokenSource.Token;
			this.gameTask = Task.Run(
				action: () => this.game.Start(), 
				cancellationToken: cancellationToken);
			
			this.gameWebSocket = new GameWebSocket(game);
			this.gameWebSocket.AddEndpoint<EchoEndpoint>("/echo");
			this.gameWebSocket.AddEndpoint<EmoEndpoint>("/linkinpark");
			this.gameWebSocket.Start();
		}


		public void OnStopping()
		{
			this.Logger.LogInformation("Stopping game service.");
			this.gameWebSocket.Stop();
			
			// TODO: Stop the game class cleanly.
			
			this.game.Stop();
			this.gameCancellationTokenSource.Cancel();
		}


		public void OnStopped()
		{
			this.Logger.LogInformation("Stopped game service.");
		}
	}
}