namespace HackThePlanet
{
	using System.Collections.Generic;
	using PrimitiveEngine;


	[Command("portscan")]
	public class PortscanCommand : Command
	{
		public override string Execute(GameEndpoint session)
		{
			string ipArgument = GetArgument(0);
			if (ipArgument == null) {
				return "usage: portscan [ip_address]";
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
			
			
			if (!InitiatePortScan(ipAddress, session))
				return "could not locate provided IP address";
			
			return $"[{ipArgument}] portscan started...";
		}


		private static bool InitiatePortScan(long ipAddress, GameEndpoint connection)
		{
			ComputerComponent targetComputer = Computer.Find(ipAddress);
			if (targetComputer == null)
				return false;

			Entity targetEntity = targetComputer.GetEntity();
			 
			NetworkAccessComponent playerAccess = connection.PlayerEntity.NetworkAccessComponent();
			playerAccess.AccessOptions[targetEntity.Id].PortAccessability = 
				new Dictionary<Port, AccessLevel>();
					
			Entity portscanEntity = Game.World.CreateEntity();
			PortScanComponent portScanComponent = new PortScanComponent();
			// ReSharper disable once PossibleNullReferenceException
			portScanComponent.InitiatingEntity = connection.PlayerEntity.Id;
			portScanComponent.TargetEntity = targetEntity.Id;
			portscanEntity.AddComponent(portScanComponent);

			Device targetDevice = new Device();
			targetDevice.ip = ipAddress.ToIPString();
			targetDevice.status = "portscanning";
			targetDevice.commands = new string[0];

			connection.PlayerComponent.QueueDeviceUpdate(targetDevice);

			return true;
		}
	}
}