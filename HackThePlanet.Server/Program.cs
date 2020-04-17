namespace HackThePlanet.Server
{
	using System;
	using System.Threading.Tasks;
	using HackThePlanet.Server;


	internal class Program
	{
		private static void Main(string[] args)
		{
			Game game = new Game();
			Task task = Task.Run(() => game.Start());
			
			GameWebSocket gameWebSocket = new GameWebSocket(game);
			gameWebSocket.AddEndpoint<EchoEndpoint>("/echo");
			gameWebSocket.AddEndpoint<EmoEndpoint>("/linkinpark");
			gameWebSocket.Start();
			
			Console.Out.WriteLine("Server listening. Press any key to exit.");
			Console.ReadKey();
		}
	}
}