namespace HackThePlanet
{
	using HackThePlanet;


	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
		{
			string name = GetArgument(0);
			GetDeviceStates(connection);
			return $"Logged in as {name}";
		}


		private void GetDeviceStates(WebsocketEndpoint connection)
		{
			GameEndpoint gameEndpoint = connection as GameEndpoint;
			DeviceUpdateMessage deviceUpdateMessage = DeviceUpdateMessage.Create(gameEndpoint.PlayerEntity.NetworkAccessComponent());
			gameEndpoint.PlayerComponent.MessageQueue.Add(deviceUpdateMessage.ToString());
		}
	}
}