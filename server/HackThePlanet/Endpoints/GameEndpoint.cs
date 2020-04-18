namespace HackThePlanet
{
	using WebSocketSharp;


	public class GameEndpoint : WebsocketEndpoint
	{
		protected override void OnMessage(MessageEventArgs message)
		{
			Command command = Command.ParseCommand(message.Data);
			string response = command.Execute(this.Game);

			if (!string.IsNullOrEmpty(response))
			{
				Send(response);
			}
		}
	}
}