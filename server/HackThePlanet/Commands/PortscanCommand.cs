namespace HackThePlanet
{
	using PrimitiveEngine;


	[Command("portscan")]
	public class PortscanCommand : Command
	{
		public override string Execute(WebsocketEndpoint connection)
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


		private bool InitiatePortScan(long ipAddress, WebsocketEndpoint connection)
		{
			
			GameEndpoint gameEndpoint = connection as GameEndpoint;
			
			foreach (Entity entity in connection.Game.EntityWorld.EntityManager.ActiveEntities)
			{
				ComputerComponent computerComponent = entity.GetComponent<ComputerComponent>();
				if (computerComponent != null
					&& computerComponent.IpAddress == ipAddress)
				{
					PortScanComponent portScanComponent = new PortScanComponent();
					
					// ReSharper disable once PossibleNullReferenceException
					portScanComponent.InitiatingEntity = gameEndpoint.PlayerEntity.Id;
					entity.AddComponent(portScanComponent);
					return true;
				}
			}

			return false;
		}
	}
}