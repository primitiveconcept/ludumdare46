namespace HackThePlanet
{
	using System;
	using PrimitiveEngine;
	using WebSocketSharp;
	using WebSocketSharp.Net;
	using Newtonsoft.Json;


	public class GameEndpoint : WebsocketEndpoint
	{
		public const string CookieKey = "playerId";
		public Entity PlayerEntity;
		public PlayerComponent PlayerComponent;


		#region Constructors
		public GameEndpoint()
			: base()
		{
			this.CookiesValidator = ValidateCookies;
		}
		#endregion


		protected override void OnClose(CloseEventArgs e)
		{
			this.PlayerComponent.Session = null;
		}


		protected override void OnMessage(MessageEventArgs message)
		{
			Command command = Command.ParseCommand(message.Data);
			string response = command.Execute(this);

			if (!string.IsNullOrEmpty(response))
			{
				var result = new
				{
					Update = "Terminal",
					Payload = new {
						Message = response,
					}
				};
				Send(JsonConvert.SerializeObject(result));
			}
		}


		private bool ValidateCookies(CookieCollection requestCookies, CookieCollection responseCookies)
		{
			Cookie playerIdCookie = requestCookies[CookieKey];
			
			// No cookies, new player.
			if (playerIdCookie == null)
			{
				this.PlayerEntity = PlayerComponent.CreateNew(this);
				this.PlayerComponent = this.PlayerEntity.GetComponent<PlayerComponent>();
				playerIdCookie = new Cookie(CookieKey, this.PlayerComponent.Id);
			}
			
			// Cookies found, validate.
			else
			{
				this.PlayerEntity = PlayerComponent.Find(playerIdCookie.Value);
				
				// Player found.
				if (this.PlayerEntity != null)
				{
					this.PlayerComponent = this.PlayerEntity.GetComponent<PlayerComponent>();
					Console.Out.WriteLine($"Found existing player.");
					
					// TODO: Login stuff?
				}
				
				// Bad cookie, new player.
				else
				{
					this.PlayerEntity = PlayerComponent.CreateNew(this);
					this.PlayerComponent = this.PlayerEntity.GetComponent<PlayerComponent>();
					playerIdCookie.Value = this.PlayerComponent.Id;
				}
			}
			
			this.PlayerComponent.Session = this;
			responseCookies.Add(playerIdCookie);

			return true;
		}
	}
}