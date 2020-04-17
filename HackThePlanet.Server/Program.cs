namespace HackThePlanet.Server
{
	using System;
	using System.Threading.Tasks;
	using HackThePlanet.Server.Endpoints;
	using WebSocketSharp.Server;


	class Program
	{
		static void Main(string[] args)
		{
			Game game = new Game();
			Task task = Task.Run(() => game.Start());
			
			var gameWebSocket = new GameWebSocket(game);
			gameWebSocket.AddEndpoint<EchoEndpoint>("/echo");
			gameWebSocket.AddEndpoint<EmoEndpoint>("/linkinpark");
			gameWebSocket.Start();
			
			Console.ReadKey(true);
			Console.Out.WriteLine("Stopping...");
			Console.Out.WriteLine("Listening...");
			gameWebSocket.Stop();
		}
	}
}