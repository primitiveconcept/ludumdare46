namespace HackThePlanet
{
	using WebSocketSharp;
	
	
	public class EchoEndpoint : WebsocketEndpoint
	{
		protected override void OnMessage(MessageEventArgs message)
		{
			Send("Echo: " + message.Data);
		}
	}
	
}