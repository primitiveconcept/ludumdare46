namespace HackThePlanet
{
    using System.Collections.Generic;
	using System.Linq;
	using PrimitiveEngine;


	[Command("portscan")]
	public class PortscanCommand : Command
	{
		public override string Execute(int playerId)
		{
			IP targetIP = GetArgument(0);
			if (!targetIP.IsValid)
			{
				return $"Failed to resolve \"{GetArgument(0)}\".<br>"
						+ $"WARNING: No targets were specified, so 0 hosts were scanned.<br>"
						+ $"portscan done: 0 IP addresses (0 hosts up) scanned in 0.05 seconds";
			}

			// TODO: ComputerComponent should be specified as an optional argument.
			PlayerComponent player = Game.Players.GetPlayer(playerId);
			ComputerComponent playerComputer = player.GetSiblingComponent<ComputerComponent>();
			NetworkDeviceComponent playerNetworkDevice = player.GetSiblingComponent<NetworkDeviceComponent>();
			IP playerIP = playerNetworkDevice.GetMainInterface().IP;

			NetworkRoute networkRoute = Game.Internet.GetRoute(playerIP, targetIP);
			IList<NetworkInterface> shortestRoute = networkRoute?.GetShortest();

			if (shortestRoute == null)
			{
				return $"Timed out connecting to host.";
			}

			PortScanApplication portscanComponent =
				ProcessPool<PortScanApplication>.RunApplication(playerComputer);
			portscanComponent.OriginEntityId = player.GetEntity();
			portscanComponent.TargetEntityId = networkRoute.ToNode.HostDevice.GetEntity();
			
			// TODO: Return "started" response.
			return null;
		}
	}
}