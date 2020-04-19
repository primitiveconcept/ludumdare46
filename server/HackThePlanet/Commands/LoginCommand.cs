namespace HackThePlanet
{
	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
		{
			string name = GetArgument(0);
			return $"Logged in as {name}";
		}
	}
}