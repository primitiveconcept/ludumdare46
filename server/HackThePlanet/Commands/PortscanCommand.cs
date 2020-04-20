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
				return $"Usage: {this.Name} [ip_address]";
			}
			
			// Locate computer with given IP address.
			long ipAddress;
			try
			{
				ipAddress = ipArgument.ToIPLong();
			}
			catch
			{
				return "Invalid IP address format";
			}

			return InitiatePortScan(ipAddress, session);
		}


		private string InitiatePortScan(long ipAddress, GameEndpoint connection)
		{
			ComputerComponent targetComputer = Computer.Find(ipAddress);
			if (targetComputer == null)
				return $"[{ipAddress.ToIPString()}] Could not locate";

			Entity targetEntity = targetComputer.GetEntity();
			Entity playerEntity = connection.PlayerEntity;
			 
			NetworkAccessComponent playerAccess = playerEntity.NetworkAccessComponent();
			AccessOptions accessOptions = playerAccess.AccessOptions[targetEntity.Id];
			if (accessOptions.PortAccessability.Keys.Count > 0)
				return $"[{ipAddress.ToIPString()}] Already been portscanned";  
			
			accessOptions.PortAccessability = 
				new Dictionary<Port, AccessLevel>();
					
			Entity portscanEntity = Game.World.CreateEntity();
			PortScanComponent portScanComponent = new PortScanComponent();
			// ReSharper disable once PossibleNullReferenceException
			portScanComponent.InitiatingEntity = playerEntity.Id;
			portScanComponent.TargetEntity = targetEntity.Id; 
			portscanEntity.AddComponent(portScanComponent);

			DeviceState targetDevice = new DeviceState();
			targetDevice.ip = ipAddress.ToIPString();
			targetDevice.status = "portscanning";
			targetDevice.commands = new string[0];

			Process process = new Process();
			process.Command = this.Name;
			process.Ram = 1;
			process.Status = "portscanning";
			playerEntity.GetComponent<ComputerComponent>().AddProcess(process);

			connection.PlayerComponent.QueueDeviceUpdate(targetDevice);
			connection.PlayerComponent.QueueProcessUpdate(targetComputer);

			return $"[{ipAddress.ToIPString()}] {this.Name} started..."; 
		}
	}
}