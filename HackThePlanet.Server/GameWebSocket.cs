namespace HackThePlanet.Server
{
	using System.Net;
	using WebSocketSharp.Server;


	public class GameWebSocket
	{
		private Game game;
		private HttpServer webSocketServer;
		
		
		public GameWebSocket(Game game)
		{
			this.game = game;
			this.webSocketServer = 
				new HttpServer(31337) 
					{ 
						ReuseAddress = true 
					};
		}

		public void Start()
		{
			this.webSocketServer.Start();
		}


		public void Stop()
		{
			this.webSocketServer.Stop();
		}


		public void AddEndpoint<T>(string endpointPath)
			where T: WebsocketEndpoint, new()
		{
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