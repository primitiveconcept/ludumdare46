namespace HackThePlanet.Host
{
    using System;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;


    public class GameEndpoint : WebsocketEndpoint
    {
        private const string SessionCookieKey = "playerId";


        #region Constructors
        public GameEndpoint(WebSocketList connections) : base(connections)
        {
            Game.SubscribeToClientMessages(OnGameStateUpdate);
        }
        #endregion


        public override async Task<string> OnHandshake(HttpRequest request, HttpResponse response)
        {
            string playerId = request.Cookies[SessionCookieKey];
            if (string.IsNullOrEmpty(playerId))
            {
                Console.Out.WriteLine("New player");
                return CreateNewPlayer(response);
            }

            PlayerComponent existingPlayer = Game.Players.GetReadonlyPlayer(playerId);
            
            // Temporary, reject these handshakes in the future.
            if (existingPlayer == null)
            {
                Console.Out.WriteLine("Bad cookie, new player");
                return CreateNewPlayer(response);
            }

            Console.Out.WriteLine("Returning player");
            return existingPlayer.Id;
        }


        public override async Task OnReceiveMessage(
            WebSocket socket, 
            WebSocketReceiveResult result, 
            byte[] buffer)
        {
            string clientId = this.connections.GetSocketId(socket);
            string message = ExtractTextMessage(result, buffer);

            Game.QueueCommand(clientId, message);
        }


        private static string CreateNewPlayer(HttpResponse response)
        {
            PlayerComponent newPlayer = Game.Players.CreateNewPlayer();
            response.Cookies.Append(SessionCookieKey, newPlayer.Id);
                
            return newPlayer.Id;
        }


        private async void OnGameStateUpdate(string playerId, string message)
        {
            await SendMessageAsync(playerId, message);
        }
    }
}