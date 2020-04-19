namespace HackThePlanet
{
	public class InvalidCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
		{
			return $"{this.Name}: command not found";
		}
	}
}