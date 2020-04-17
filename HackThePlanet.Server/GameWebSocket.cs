namespace HackThePlanet.Server
{
	using WebSocketSharp.Server;


	public class GameWebSocket
	{
		private Game game;
		private WebSocketServer webSocketServer;
		
		
		public GameWebSocket(Game game)
		{
			this.game = game;
			this.webSocketServer = new WebSocketServer("ws://localhost:31337");
		}

		public void Start()
		{
			this.webSocketServer.Start();
		}


		public void Stop()
		{
			this.webSocketServer.Stop();
		}


		public WebsocketEndpoint AddEndpoint<T>(string endpointPath)
			where T: WebsocketEndpoint, new()
		{
			var websocketEndpoint = this.webSocketServer.AddWebSocketService<T>(endpointPath) as T;
			websocketEndpoint.Game = this.game;

			return websocketEndpoint;
		}
	}
}