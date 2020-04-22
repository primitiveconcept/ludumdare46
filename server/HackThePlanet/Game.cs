namespace HackThePlanet
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Logging;
	using PrimitiveEngine;


	/// <summary>
	/// You can essentially think of this as the starting point of the game.
	/// Starts and stops the game loop and websocket server.
	/// You also register endpoints here.
	/// </summary>
	public class Game : ICommonService
	{
		private static Game _instance;

		private IConfiguration configuration;
		private ILogger<Game> logger;

		private GameThread gameThread;
		private GameWebSocket gameWebSocket;
		private Task gameTask;
		private CancellationTokenSource gameCancellationTokenSource  = new CancellationTokenSource();


		#region Constructors
		public Game(
			IConfiguration configuration,
			ILogger<Game> logger)
		{
			_instance = this;
			this.configuration = configuration;
			this.logger = logger;
			LogInfo("Standard service instantiated.");
		}
		#endregion


		#region Properties
		public static GameTime Time
		{
			get { return _instance.gameThread.GameTime; }
		}


		public static EntityWorld World
		{
			get { return _instance.gameThread.EntityWorld; }
		}


		public IConfiguration Configuration
		{
			get { return this.configuration; }
		}


		public ILogger<Game> Logger
		{
			get { return this.logger; }
		}
		#endregion


		public static GameThread GameThread
		{
			get { return _instance.gameThread; }
		}
		
		public static IEnumerable<T> GetComponents<T>()
			where T: IEntityComponent
		{
			return World.EntityManager.GetComponents<T>().Cast<T>();
		}


		public static List<Entity> GetEntities(params int[] entityIds)
		{
			List<Entity> entities = new List<Entity>();
			
			foreach (int entityId in entityIds)
			{
				Entity entity = World.GetEntityById(entityId);
				if (entity != null)
					entities.Add(entity);
			}

			return entities;
		}


		public static Entity GetEntity(int entityId)
		{
			return World.GetEntityById(entityId);
		}


		public static void LogError(string info)
		{
			_instance.logger.LogError(info);
		}


		public static void LogInfo(string info)
		{
			_instance.logger.LogInformation(info);
		}


		public static void LogWarning(string info)
		{
			_instance.logger.LogWarning(info);
		}


		public void OnStart()
		{
			LogInfo("Starting game service.");
			this.gameThread = new GameThread();
			
			CancellationToken cancellationToken = this.gameCancellationTokenSource.Token;
			this.gameTask = Task.Run(
				action: () => this.gameThread.Start(), 
				cancellationToken: cancellationToken);
			
			this.gameWebSocket = new GameWebSocket(this.gameThread);
			RegisterEndpoints();
			this.gameWebSocket.Start();
		}


		public void OnStopped()
		{
			LogInfo("Stopped game service.");
		}


		public void OnStopping()
		{
			LogInfo("Stopping game service.");
			this.gameWebSocket.Stop();
			
			// TODO: Stop the game class cleanly.
			
			this.gameThread.Stop();
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