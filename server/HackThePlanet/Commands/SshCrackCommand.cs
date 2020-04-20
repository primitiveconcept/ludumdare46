namespace HackThePlanet
{
	using PrimitiveEngine;


	[Command("sshcrack")]
	public class SshCrackCommand : Command
	{
		public override string Execute(GameEndpoint session)
		{
			string ipArgument = GetArgument(0);
			if (ipArgument == null) {
				return "usage: sshcrack [ip_address]";
			}
			
			// Locate computer with given IP address.
			long ipAddress;
			try
			{
				ipAddress = ipArgument.ToIPLong();
			}
			catch
			{
				return "invalid IP address format";
			}
			
			if (!InitiateSshCrack(ipAddress, session))
				return "could not locate provided IP address";
			
			return $"[{ipAddress}] Running sshcrack algorithms...";
		}


		private static bool InitiateSshCrack(long ipAddress, GameEndpoint connection)
		{
			ComputerComponent targetComputer = Computer.Find(ipAddress);
			if (targetComputer == null)
				return false;
			
			Entity targetEntity = targetComputer.GetEntity();
			
			
			DeviceState targetDevice = new DeviceState();
			targetDevice.ip = ipAddress.ToIPString();
			targetDevice.status = "sshcrack (0%)";
			targetDevice.commands = new string[0];
			
			connection.PlayerComponent.QueueDeviceUpdate(targetDevice);

			return true;
		}
	}
}