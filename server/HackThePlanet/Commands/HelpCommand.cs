namespace HackThePlanet
{
	[Command("help")]
	public class HelpCommand : Command
	{
		public override string Execute(GameEndpoint connection)
		{
			return "Available commands:";
		}
	}
}