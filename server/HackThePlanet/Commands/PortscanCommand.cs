namespace HackThePlanet
{
	using System.Collections.Generic;
	using PrimitiveEngine;


	[Command("portscan")]
	public class PortscanCommand : Command
	{
		public override string Execute(GameEndpoint connection)
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
			
			
			if (!InitiatePortScan(ipAddress, connection))
				return "could not locate provided IP address";
			
			return null;
		}


		private bool InitiatePortScan(long ipAddress, GameEndpoint connection)
		{
			foreach (Entity entity in Game.World.EntityManager.GetEntities(Aspect.One(typeof(ComputerComponent))))
			{
				ComputerComponent computerComponent = entity.GetComponent<ComputerComponent>();
				if (computerComponent != null
					&& computerComponent.IpAddress == ipAddress)
				{
					NetworkAccessComponent networkAccessComponent = connection.PlayerEntity.NetworkAccessComponent();
					var portAccessibility = new Dictionary<Port, AccessLevel>();
					networkAccessComponent.AccessOptions[entity.Id].PortAccessability = portAccessibility;
					
					PortScanComponent portScanComponent = new PortScanComponent();
					// ReSharper disable once PossibleNullReferenceException
					portScanComponent.InitiatingEntity = connection.PlayerEntity.Id;
					entity.AddComponent(portScanComponent);

					DeviceUpdateMessage.Device targetDevice = new DeviceUpdateMessage.Device();
					targetDevice.ip = ipAddress.ToIPString();
					targetDevice.status = "Port Scanning";
					targetDevice.commands = new string[0];
					
					connection.PlayerComponent.MessageQueue.Add(
						DeviceUpdateMessage.Create(ipAddress.ToIPString(), targetDevice).ToJson());
					return true;
				}
			}

			return false;
		}
	}
}