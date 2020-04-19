namespace HackThePlanet
{
	using System;
	using WebSocketSharp;
	using WebSocketSharp.Net;
  using Newtonsoft.Json;


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