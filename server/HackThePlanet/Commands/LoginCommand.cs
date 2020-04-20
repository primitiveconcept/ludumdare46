namespace HackThePlanet
{
	using HackThePlanet;


	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(GameEndpoint session)
		{
			GameEndpoint gameEndpoint = session as GameEndpoint;
			string name = GetArgument(0);
			
			// First time login
			if (string.IsNullOrEmpty(session.PlayerComponent.Name))
			{
				// Must provide a name the first time.
				if (string.IsNullOrEmpty(name))
					return $"Login requires a username";
			}
			else
			{
				return null;
			}
			
			GetDeviceStates(session);
			return null;
		}


		private void GetDeviceStates(GameEndpoint gameEndpoint)
		{
			DeviceUpdateMessage deviceUpdateMessage = DeviceUpdateMessage.Create(gameEndpoint.PlayerEntity.NetworkAccessComponent());
			gameEndpoint.PlayerComponent.MessageQueue.Add(deviceUpdateMessage.ToJson());
		}
	}
}