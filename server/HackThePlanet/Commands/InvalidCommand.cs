namespace HackThePlanet
{
	public class InvalidCommand : Command
	{
		public override string Execute(Game game)
		{
			return $"{this.Name}: command not found";
		}
	}
}