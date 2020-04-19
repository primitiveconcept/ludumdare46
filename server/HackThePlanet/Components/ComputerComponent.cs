namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using PrimitiveEngine;


	[Serializable]
	public class ComputerComponent : IEntityComponent
	{
		public long IpAddress;
		public byte MaxRam;
		public byte UsedRam;

		public List<Port> OpenPorts = new List<Port>();
	}
}