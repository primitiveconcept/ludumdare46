namespace HackThePlanet
{
	using System.Net;
	using System.Text;
	using Microsoft.Extensions.Logging;
	using WebSocketSharp.Server;
	using HttpListenerRequest = WebSocketSharp.Net.HttpListenerRequest;
	using HttpListenerResponse = WebSocketSharp.Net.HttpListenerResponse;


	/// <summary>
	/// A wrapper around the web socket server, to inject Game state reference into each.
	/// Also handles starting and stopping of the server.
	/// Don't add endpoint here; do that in GameService (RegisterEndpoints).
	/// </summary>
	public class GameWebSocket
	{
		private static readonly ILogger logger = ApplicationLogging.CreateLogger<GameWebSocket>();

		private GameThread gameThread;
		private HttpServer webSocketServer;


		#region Constructors
		public GameWebSocket(GameThread gameThread)
		{
			logger.LogInformation("Creating game web socket server.");
			this.gameThread = gameThread;
			this.webSocketServer = 
				new HttpServer(31337) 
					{ 
						ReuseAddress = true,
						KeepClean = false, 
						DocumentRootPath = "wwwroot"
					};

			this.webSocketServer.OnGet += WebSocketServer_OnGet;
		}
		#endregion


		#region Properties
		public HttpServer WebSocketServer
		{
			get { return this.webSocketServer; }
		}
		#endregion


		public void AddEndpoint<T>(string endpointPath)
			where T: WebSocketBehavior, new()
		{
			logger.LogInformation($"Adding web socket endpoint {endpointPath}");
			this.webSocketServer.AddWebSocketService<T>(
				path: endpointPath, 
				creator: () =>
				{
					T service = new T();
					return service;
				});
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


		private void WebSocketServer_OnGet(object sender, HttpRequestEventArgs e)
		{
			HttpListenerRequest request = e.Request;
			HttpListenerResponse response = e.Response;

			string path = request.RawUrl;
			if (path == "/")
				path += "index.html";

			byte[] contents;
			if (!e.TryReadFile(path, out contents))
			{
				response.StatusCode = (int)HttpStatusCode.NotFound;
				return;
			}

			if (path.EndsWith(".html"))
			{
				response.ContentType = "text/html";
				response.ContentEncoding = Encoding.UTF8;
			}
			else if (path.EndsWith(".js"))
			{
				response.ContentType = "application/javascript";
				response.ContentEncoding = Encoding.UTF8;
			}

			response.ContentLength64 = contents.LongLength;
			response.Close(contents, true);
		}
	}
}