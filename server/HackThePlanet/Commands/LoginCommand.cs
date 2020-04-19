namespace HackThePlanet
{
	using HackThePlanet;


	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
		{
			GameEndpoint gameEndpoint = connection as GameEndpoint;
			string name = GetArgument(0);
			
			// First time login
			if (string.IsNullOrEmpty(gameEndpoint.PlayerComponent.Name))
			{
				// Must provide a name the first time.
				if (string.IsNullOrEmpty(name))
					return $"Invalid login attempt";
				
				// Set player name.
				else
					gameEndpoint.PlayerComponent.Name = name;
			}
			
			GetDeviceStates(gameEndpoint);
			return $"Authenticating with public key \"imported-133+ssh-key\"\n"
					+ $"Logged in as {name}";
		}


		private void GetDeviceStates(GameEndpoint gameEndpoint)
		{
			DeviceUpdateMessage deviceUpdateMessage = DeviceUpdateMessage.Create(gameEndpoint.PlayerEntity.NetworkAccessComponent());
			gameEndpoint.PlayerComponent.MessageQueue.Add(deviceUpdateMessage.ToString());
		}
	}
}