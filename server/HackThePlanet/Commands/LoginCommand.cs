namespace HackThePlanet
{
	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(Game game)
		{
			string name = GetArgument(0);
			return $"Logged in as {name}";
		}
	}
}