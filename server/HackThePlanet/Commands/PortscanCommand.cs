namespace HackThePlanet
{
    using System.Collections.Generic;
	using PrimitiveEngine;


	[Command("portscan")]
	public class PortscanCommand : Command
	{
		public override string Execute(string playerId)
		{
			IP targetIP = GetArgument(0);
			if (!targetIP.IsValid)
			{
				return $"Failed to resolve \"{GetArgument(0)}\".<br>"
						+ $"WARNING: No targets were specified, so 0 hosts were scanned.<br>"
						+ $"portscan done: 0 IP addresses (0 hosts up) scanned in 0.05 seconds";
			}
			
			
		}
	}
}