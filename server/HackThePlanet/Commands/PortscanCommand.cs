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
			
			return null;
		}


		private static bool InitiatePortScan(long ipAddress, GameEndpoint connection)
		{
			ComputerComponent computerComponent = Computer.Find(ipAddress);
			if (computerComponent == null)
				return false;

			Entity entity = computerComponent.GetEntity();
			
			NetworkAccessComponent networkAccessComponent = connection.PlayerEntity.NetworkAccessComponent();
			Dictionary<Port, AccessLevel> portAccessibility = new Dictionary<Port, AccessLevel>();
			networkAccessComponent.AccessOptions[entity.Id].PortAccessability = portAccessibility;
					
			PortScanComponent portScanComponent = new PortScanComponent();
			// ReSharper disable once PossibleNullReferenceException
			portScanComponent.InitiatingEntity = connection.PlayerEntity.Id;
			entity.AddComponent(portScanComponent);

			DeviceUpdateMessage.Device targetDevice = new DeviceUpdateMessage.Device();
			targetDevice.ip = ipAddress.ToIPString();
			targetDevice.status = "portscanning";
			targetDevice.commands = new string[0];
					
			connection.PlayerComponent.MessageQueue.Add(
				DeviceUpdateMessage.Create(ipAddress.ToIPString(), targetDevice).ToJson());
			
			return true;
		}
	}
}