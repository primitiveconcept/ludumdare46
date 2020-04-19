namespace HackThePlanet
{
	using System;
	using WebSocketSharp;
	using WebSocketSharp.Net;


	public class GameEndpoint : WebsocketEndpoint
	{
		#region Constructors
		public GameEndpoint()
			: base()
		{
			this.CookiesValidator =
				ValidateCookies;
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
			if (requestCookies.Count < 1)
			{
				responseCookies.Add(new Cookie("uuid", Guid.NewGuid().ToString()));
			}
			else
			{
				foreach (Cookie cookie in requestCookies)
				{
					if (cookie.Name == "uuid")
					{
					}
				}
			}

			return true;
		}
	}
}