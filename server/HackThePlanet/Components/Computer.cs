namespace HackThePlanet
{
	using System;
	using System.Net;
	using PrimitiveEngine;


	[Serializable]
	public class Computer : IEntityComponent
	{
		public IPAddress IpAddress;
		public byte MaxRam;
		public byte UsedRam;
	}
}