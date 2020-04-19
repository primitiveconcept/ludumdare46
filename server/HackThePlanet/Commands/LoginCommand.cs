namespace HackThePlanet
{
	using HackThePlanet;


	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(GameEndpoint connection)
		{
			GameEndpoint gameEndpoint = connection as GameEndpoint;
			string name = GetArgument(0);
			
			// First time login
			if (string.IsNullOrEmpty(connection.PlayerComponent.Name))
			{
				// Must provide a name the first time.
				if (string.IsNullOrEmpty(name))
					return $"Login requires a username";
				
				// Set player name.
				else
					connection.PlayerComponent.Name = name;
			}
			else
			{
				if (!string.IsNullOrEmpty(name)
					&& connection.PlayerComponent.Name != name)
				{
					return "Authenticating with public key \"imported-133+ssh-key\"\n" 
							+ $"Invalid username [{name}] -- attempt has been logged";
				}
			}
			
			GetDeviceStates(connection);
			return "Authenticating with public key \"imported-133+ssh-key\"\n"
					+ $"Logged in as {name}";
		}


		private void GetDeviceStates(GameEndpoint gameEndpoint)
		{
			DeviceUpdateMessage deviceUpdateMessage = DeviceUpdateMessage.Create(gameEndpoint.PlayerEntity.NetworkAccessComponent());
			gameEndpoint.PlayerComponent.MessageQueue.Add(deviceUpdateMessage.ToString());
		}
	}
}