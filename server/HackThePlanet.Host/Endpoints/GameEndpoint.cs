namespace HackThePlanet.Host
{
    using System;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using PrimitiveEngine;


    public class GameEndpoint : WebsocketEndpoint
    {
        private const string SessionCookieKey = "playerId";


        #region Constructors
        public GameEndpoint(WebSocketList connections) : base(connections)
        {
            Game.SubscribeToClientMessages(OnGameStateUpdate);
        }
        #endregion


        public override async Task<int> OnHandshake(HttpRequest request, HttpResponse response)
        {
            int playerId = 0;
            if (!int.TryParse(request.Cookies[SessionCookieKey], out playerId))
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
            int clientId = this.connections.GetSocketId(socket);
            string message = ExtractTextMessage(result, buffer);

            Game.QueueCommand(clientId, message);
        }


        private static int CreateNewPlayer(HttpResponse response)
        {
            int playerId = Game.Players.CreateNewPlayer().Id;
            response.Cookies.Append(SessionCookieKey, playerId.ToString());
                
            return playerId;
        }


        private async void OnGameStateUpdate(int playerId, string message)
        {
            await SendMessageAsync(playerId, message);
        }
    }
}