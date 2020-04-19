namespace HackThePlanet
{
	using System;
	using HackThePlanet;
	using PrimitiveEngine;
	using WebSocketSharp;
	using WebSocketSharp.Net;


	public class GameEndpoint : WebsocketEndpoint
	{
		public const string CookieKey = "playerId";
		
		#region Constructors
		public GameEndpoint()
			: base()
		{
			this.CookiesValidator = ValidateCookies;
		}
		#endregion


		protected override void OnMessage(MessageEventArgs message)
		{
			Command command = Command.ParseCommand(message.Data);
			string response = command.Execute(this);

			if (!string.IsNullOrEmpty(response))
			{
				Send(response);
			}
		}


		private bool ValidateCookies(CookieCollection requestCookies, CookieCollection responseCookies)
		{
			Cookie playerIdCookie = requestCookies[CookieKey];
			
			// No cookies, new player.
			if (playerIdCookie == null)
			{
				Entity newPlayer = Player.CreateNew(this.Game);
				string newPlayerId = newPlayer.GetComponent<Player>().Id;
				playerIdCookie = new Cookie(CookieKey, newPlayerId);
			}
			
			// Cookies found, validate.
			else
			{
				
				Entity playerEntity = Player.Find(playerIdCookie.Value);
				
				// Player found.
				if (playerEntity != null)
				{
					Console.Out.WriteLine($"Found existing player.");
					// TODO: Login stuff.
				}
				
				// Bad cookie, new player.
				else
				{
					Entity newPlayer = Player.CreateNew(this.Game);
					string newPlayerId = newPlayer.GetComponent<Player>().Id;
					playerIdCookie.Value = newPlayerId;
				}
			}
			
			responseCookies.Add(playerIdCookie);

			return true;
		}
	}
}