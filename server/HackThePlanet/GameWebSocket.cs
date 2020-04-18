namespace HackThePlanet
{
	using Microsoft.Extensions.Logging;
	using WebSocketSharp.Server;

	
	/// <summary>
	/// A wrapper around the web socket server, to inject Game state reference into each.
	/// Also handles starting and stopping of the server.
	/// Don't add endpoint here; do that in GameService (RegisterEndpoints).
	/// </summary>
	public class GameWebSocket
	{
		private static readonly ILogger logger = ApplicationLogging.CreateLogger<GameWebSocket>();
		
		private Game game;
		private HttpServer webSocketServer;
		
		
		public GameWebSocket(Game game)
		{
			logger.LogInformation("Creating game web socket server.");
			this.game = game;
			this.webSocketServer = 
				new HttpServer(31337) 
					{ 
						ReuseAddress = true 
					};
		}

		public void Start()
		{
			logger.LogInformation("Starting game web socket server.");
			this.webSocketServer.Start();
		}


		public void Stop()
		{
			logger.LogInformation("Stopping game web socket server.");
			this.webSocketServer.Stop();
		}


		public void AddEndpoint<T>(string endpointPath)
			where T: WebsocketEndpoint, new()
		{
			logger.LogInformation($"Adding web socket endpoint {endpointPath}");
			this.webSocketServer.AddWebSocketService<T>(
				path: endpointPath, 
				creator: () =>
				{
					T service = new T();
					service.Game = this.game;
					return service;
				});
		}
	}
}