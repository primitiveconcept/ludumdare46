namespace HackThePlanet.Server
{
	using WebSocketSharp.Server;


	public abstract class WebsocketEndpoint : WebSocketBehavior
	{
		public Game Game { get; set; }
	}
}