namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;


	[Serializable]
	public class ComputerComponent : IEntityComponent
	{
		public long IpAddress;
		public byte Ram;

		public List<Port> OpenPorts = new List<Port>();
	}
}