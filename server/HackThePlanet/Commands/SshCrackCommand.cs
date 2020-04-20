namespace HackThePlanet
{
	[Command("sshcrack")]
	public class SshCrackCommand : Command
	{
		public override string Execute(GameEndpoint session)
		{
			string ipArgument = GetArgument(0);
			if (ipArgument == null) {
				return "usage: sshcrack [ip_address]";
			}
			
			
			return null;
		}
	}
}