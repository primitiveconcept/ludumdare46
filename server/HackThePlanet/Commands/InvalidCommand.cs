namespace HackThePlanet
{
	public class InvalidCommand : Command
	{
		public override string Execute(Game game)
		{
			return "Not a valid command.";
		}
	}
}