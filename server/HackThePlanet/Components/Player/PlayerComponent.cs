namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using Newtonsoft.Json;
	using PrimitiveEngine;


	[Serializable]
	public class PlayerComponent : IEntityComponent
	{
		public string Id;
		public string Name;
		public List<string> MessageQueue = new List<string>();

		[JsonIgnore] public GameEndpoint Session;


		public void QueueDeviceUpdate(Device device)
		{
			DeviceUpdateMessage deviceUpdateMessage = new DeviceUpdateMessage();
			deviceUpdateMessage.payload =
				new
					{
						devices = new[] { device }
					};

			this.MessageQueue.Add(deviceUpdateMessage.ToJson());
		}
		
		public void QueueTerminalMessage(string message)
		{
			var result = new
							{
								Update = "Terminal",
								Payload = new {
													Message = message,
												}
							};
			this.MessageQueue.Add(JsonConvert.SerializeObject(result));
		}
	}
}