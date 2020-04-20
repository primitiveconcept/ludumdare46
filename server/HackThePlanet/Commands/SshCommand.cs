namespace HackThePlanet
{
	[Command("ssh")]
	public class SshCommand : Command
	{
		public override string Execute(GameEndpoint session)
		{
			string ip = GetArgument(0);
			string username = GetArgument(1);
			string password = GetArgument(2);
			if (ip == null || username == null || password == null) {
				return "usage: ssh [ip_address] [username] [password]";
			}
			return null;
		}
	}
}