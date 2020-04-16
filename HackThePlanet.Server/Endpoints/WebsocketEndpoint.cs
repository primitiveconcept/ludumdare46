namespace HackThePlanet.Server.Endpoints
{
	using WebSocketSharp.Server;


	public abstract class WebsocketEndpoint : WebSocketBehavior
	{
		public Game Game { get; set; }
	}
}