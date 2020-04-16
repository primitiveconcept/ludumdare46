namespace HackThePlanet.Server
{
	using System;
	using System.Threading.Tasks;
	using WebSocketSharp.Server;


	class Program
	{
		static void Main(string[] args)
		{
			Game game = new Game();
			var gameWebSocket = new GameWebSocket(game);
			
			Task task = Task.Run(() => game.Start());
			
			Console.ReadKey(true);
			Console.Out.WriteLine("Stopping...");
			Console.Out.WriteLine("Listening...");
			gameWebSocket.Stop();
		}
	}
}