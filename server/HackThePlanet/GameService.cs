namespace HackThePlanet
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using Newtonsoft.Json;
	using PrimitiveEngine;


	/// <summary>
	/// You can essentially think of this as the starting point of the game.
	/// Starts and stops the game loop and websocket server.
	/// You also register endpoints here.
	/// </summary>
	public class GameService : ICommonService
	{
		private static GameService _instance;

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
			_instance = this;
			this.configuration = configuration;
			this.logger = logger;
			logger.LogInformation("Standard service instantiated.");
		}
		#endregion


		#region Properties
		public static Game Game
		{
			get { return _instance.game; }
		}


		public IConfiguration Configuration
		{
			get { return this.configuration; }
		}


		public ILogger<GameService> Logger
		{
			get { return this.logger; }
		}
		#endregion


		public static Entity GetEntity(int entityId)
		{
			return Game.EntityWorld.GetEntityById(entityId);
		}


		public void OnStart()
		{
			this.Logger.LogInformation("Starting game service.");
			this.game = new Game();
			
			CancellationToken cancellationToken = this.gameCancellationTokenSource.Token;
			this.gameTask = Task.Run(
				action: () => this.game.Start(), 
				cancellationToken: cancellationToken);
			
			this.gameWebSocket = new GameWebSocket(this.game);
			RegisterEndpoints();
			this.gameWebSocket.Start();
		}


		public void OnStopped()
		{
			this.Logger.LogInformation("Stopped game service.");
		}


		public async void OnStopping()
		{
			this.Logger.LogInformation("Stopping game service.");
			this.gameWebSocket.Stop();
			
			// TODO: Stop the game class cleanly.
			
			this.game.Stop();
			this.gameCancellationTokenSource.Cancel();
		}


		private void RegisterEndpoints()
		{
			this.gameWebSocket.AddEndpoint<EchoEndpoint>("/echo");
			this.gameWebSocket.AddEndpoint<EmoEndpoint>("/linkinpark");
			this.gameWebSocket.AddEndpoint<GameEndpoint>("/game");
			this.gameWebSocket.AddEndpoint<BigBrotherEndpoint>("/bigbrother");
		}
	}
}