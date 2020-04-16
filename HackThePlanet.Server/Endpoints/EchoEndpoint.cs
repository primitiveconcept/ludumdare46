namespace HackThePlanet.Server.Endpoints
{
	using WebSocketSharp;
	
	
	public class EchoEndpoint : WebsocketEndpoint
	{
		protected override void OnMessage(MessageEventArgs message)
		{
			// Echo
			Send("Echo: " + message.Data);
		}
	}
	
}