namespace HackThePlanet
{
	using System.Text;
	using Newtonsoft.Json;
	using PrimitiveEngine;
	using WebSocketSharp;


	public class BigBrotherEndpoint : WebsocketEndpoint
	{
		protected override void OnMessage(MessageEventArgs message)
		{
			if (message.Data == "players")
			{
				var entities = this.Game.EntityWorld.EntityManager
					.GetEntities(Aspect.One(typeof(Player)));
				StringBuilder response = new StringBuilder();
				response.AppendLine("Player List:");
				foreach (Entity entity in entities)
				{
					string json = JsonConvert.SerializeObject(entity.GetComponent<Player>());
					response.AppendLine(json);
				}

				Send(response.ToString());
			}
		}
	}
}