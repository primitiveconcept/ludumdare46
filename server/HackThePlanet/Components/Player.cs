namespace HackThePlanet
{
	using System;
	using System.Collections.Generic;
	using PrimitiveEngine;


	[Serializable]
	public class Player : IEntityComponent
	{
		public static Dictionary<string, Entity> Index = new Dictionary<string, Entity>();


		public static Entity CreateNew(Game game)
		{
			// Add new player entity.
			// Temporary until we get a proper "login" sequence going.
			Entity newEntity = game.EntityWorld.CreateEntity();
				
			Player player = new Player();
			player.Id = Guid.NewGuid().ToString();
			newEntity.AddComponent(player);

			Index.Add(player.Id, newEntity);

			Console.Out.WriteLine($"Created new player: {player.Id}");
			
			return newEntity;
		}


		public static Entity Find(string playerId)
		{
			return Index.ContainsKey(playerId) 
						? Index[playerId] 
						: null;
		}
		
		
		
		public string Id;
	}
}