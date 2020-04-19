namespace HackThePlanet
{
	[Command("portscan")]
	public class PortscanCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
		{
			string ip = GetArgument(0);
			if (ip == null) {
				return "usage: portscan [ip_address]";
			}
			return null;
		}
	}
}