namespace HackThePlanet
{
	public class InvalidCommand : Command
	{
		public override string Execute(GameEndpoint connection)
		{
			return $"{this.Name}: command not found";
		}
	}
}