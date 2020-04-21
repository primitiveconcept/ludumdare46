namespace HackThePlanet
{
	using System.Collections.Generic;
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

			return InitiateSshCrack(ipAddress, session);
		}


		private string InitiateSshCrack(long ipAddress, GameEndpoint connection)
		{
			ComputerComponent targetComputer = Computer.Find(ipAddress);
			if (targetComputer == null)
				return $"[{ipAddress.ToIPString()}] Could not locate";

			Entity targetEntity = targetComputer.GetEntity();
			Entity playerEntity = connection.PlayerEntity;
			 
			NetworkAccessComponent playerAccess = playerEntity.NetworkAccessComponent();
			AccessOptions accessOptions = playerAccess.AccessOptions[targetEntity.Id];
			
			if (!targetComputer.OpenPorts.Contains(Port.Ssh))
			{
				return $"[{ipAddress.ToIPString()}] No SSH service detected";	
			}
			
			// It's possible the player hasn't portscanned yet, and is
			// doing this manually.
			// We allow for it.
			if (!accessOptions.PortAccessability.ContainsKey(Port.Ssh)
				|| accessOptions.PortAccessability[Port.Ssh] == AccessLevel.Unknown)
			{
				accessOptions.PortAccessability.Add(Port.Ssh, AccessLevel.Known);
			}

			var sshAccessibility = accessOptions.PortAccessability[Port.Ssh];
			if (accessOptions.PortAccessability[Port.Ssh] != AccessLevel.Known)
			{
				return $"[{ipAddress.ToIPString()}] SSH authorization already cracked";
			}
			
			
					
			Entity sshCrackEntity = Game.World.CreateEntity();
			SshCrackComponent sshCrackComponent = new SshCrackComponent();
			// ReSharper disable once PossibleNullReferenceException
			sshCrackComponent.Progress = 0;
			sshCrackComponent.InitiatingEntity = playerEntity.Id;
			sshCrackComponent.TargetEntity = targetEntity.Id; 
			sshCrackEntity.AddComponent(sshCrackComponent);

			DeviceState targetDevice = new DeviceState();
			targetDevice.ip = ipAddress.ToIPString();
			targetDevice.status = "sshcrack (0%)";
			targetDevice.commands = new string[0];

			Process process = new Process();
			process.Command = this.Name;
			process.Ram = 1;
			process.Status = "cracking"; 
			playerEntity.GetComponent<ComputerComponent>().AddProcess(process);

			connection.PlayerComponent.QueueDeviceUpdate(targetDevice);
			connection.PlayerComponent.QueueProcessUpdate(targetComputer);

			return $"[{ipAddress.ToIPString()}] {this.Name} started..."; 
		}
	}
}