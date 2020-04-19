namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using System.Net;
	using Newtonsoft.Json;
	using PrimitiveEngine;


	[Serializable]
	public class PlayerComponent : IEntityComponent
	{
		public static Dictionary<string, Entity> Index = new Dictionary<string, Entity>();

		public string Id;
		public string Name;

		[JsonIgnore] public GameEndpoint Session;
		[JsonIgnore] public List<string> MessageQueue = new List<string>();


		public void AddTerminalMessage(string message)
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
		
		public static Entity CreateNew(GameEndpoint session)
		{
			// Add new player entity.
			// Temporary until we get a proper "login" sequence going.
			Entity newEntity = session.Game.EntityWorld.CreateEntity();
				
			PlayerComponent playerComponent = new PlayerComponent();
			playerComponent.Session = session;
			playerComponent.Id = Guid.NewGuid().ToString();
			newEntity.AddComponent(playerComponent);

			Index.Add(playerComponent.Id, newEntity);
			
			ComputerComponent computerComponent = new ComputerComponent();
			// TODO: Randomly generate IpAddress.
			computerComponent.IpAddress = IPAddress.Parse("127.0.0.1").Address;
			computerComponent.OpenPorts.Add(Port.Ssh);
			newEntity.AddComponent(computerComponent);

			Console.Out.WriteLine($"Created new player: {playerComponent.Id}");
			
			return newEntity;
		}


		public static Entity Find(string playerId)
		{
			return Index.ContainsKey(playerId) 
						? Index[playerId] 
						: null;
		}
	}
}