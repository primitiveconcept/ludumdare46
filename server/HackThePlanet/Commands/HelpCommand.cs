namespace HackThePlanet
{
	[Command("help")]
	public class HelpCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
		{
			return "Available commands:";
		}
	}
}