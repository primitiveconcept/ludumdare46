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
			}
			else
			{
				return null;
			}
			
			GetDeviceStates(connection);
			return null;
		}


		private void GetDeviceStates(GameEndpoint gameEndpoint)
		{
			DeviceUpdateMessage deviceUpdateMessage = DeviceUpdateMessage.Create(gameEndpoint.PlayerEntity.NetworkAccessComponent());
			gameEndpoint.PlayerComponent.MessageQueue.Add(deviceUpdateMessage.ToJson());
		}
	}
}