namespace HackThePlanet.Host
{
    using System;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;


    public class WebSocketMiddleware
    {
        private readonly RequestDelegate next;

        private WebsocketEndpoint session;


        #region Constructors
        public WebSocketMiddleware(RequestDelegate next, WebsocketEndpoint session)
        {
            this.next = next;
            this.session = session;
        }
        #endregion


        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            string clientId = await this.session.OnHandshake(context.Request, context.Response);
            if (string.IsNullOrEmpty(clientId))
            {
                return;
            }
                
            WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();
            this.session.Connections.AddSocket(clientId, socket);

            await Receive(
                socket,
                async (result, buffer) =>
                    {
                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            await this.session.OnReceiveMessage(socket, result, buffer);
                            return;
                        }
                        
                        // TODO: Ping-pong to remove disconnected clients.
                        
                        else if (result.MessageType == WebSocketMessageType.Close)
                        {
                            await this.session.OnDisconnected(socket);
                            return;
                        }
                    });
        }


        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            byte[] buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await socket.ReceiveAsync(
                                                    buffer: new ArraySegment<byte>(buffer),
                                                    cancellationToken: CancellationToken.None);
                handleMessage(result, buffer);
            }
        }
    }
}