namespace HackThePlanet
{
	using System.Text;
	using Newtonsoft.Json;
	using PrimitiveEngine;
	using WebSocketSharp;
	using WebSocketSharp.Server;


	public class BigBrotherEndpoint : WebSocketBehavior
	{
		protected override void OnMessage(MessageEventArgs message)
		{
			if (message.Data == "players")
			{
				Bag<Entity> entities = Game.World.EntityManager
					.GetEntities(Aspect.One(typeof(PlayerComponent)));
				StringBuilder response = new StringBuilder();
				response.AppendLine("Player List:");
				foreach (Entity entity in entities)
				{
					response.Append($"Enity: {entity.Id} -- ");
					ComputerComponent computerComponent = entity.GetComponent<ComputerComponent>();
					response.AppendLine(computerComponent.IpAddress.ToIPString());
					response.AppendLine(JsonConvert.SerializeObject(entity.GetComponent<PlayerComponent>()));
					response.AppendLine();
				}

				Send(response.ToString());
			}
		}
	}
}