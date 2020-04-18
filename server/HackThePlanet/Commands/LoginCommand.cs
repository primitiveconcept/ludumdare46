namespace HackThePlanet
{
	[Command("internal_login")]
	public class LoginCommand : Command
	{
		public override string Execute(Game game)
		{
			string name = this.Arguments[0];
			return "Not implemented yet";
		}
	}
}