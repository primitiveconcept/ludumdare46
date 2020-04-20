namespace HackThePlanet
{
	public class InvalidCommand : Command
	{
		public override string Execute(GameEndpoint session)
		{
			return $"{this.Name}: command not found";
		}
	}
}