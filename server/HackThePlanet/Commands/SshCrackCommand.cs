namespace HackThePlanet
{
	[Command("sshcrack")]
	public class SshCrackCommand : Command
	{
		public override string Execute(GameEndpoint connection)
		{
			string ip = GetArgument(0);
			if (ip == null) {
				return "usage: sshcrack [ip_address]";
			}
			return null;
		}
	}
}