namespace HackThePlanet.Host
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;


    public class WebSocketList
    {
        private ConcurrentDictionary<string, WebSocket> sockets = new ConcurrentDictionary<string, WebSocket>();


        #region Properties
        public ConcurrentDictionary<string, WebSocket> Sockets
        {
            get { return this.sockets; }
        }
        #endregion


        public void AddSocket(string id, WebSocket socket)
        {
            this.sockets.TryAdd(id, socket);
        }


        public WebSocket GetSocket(string id)
        {
            return this.sockets.TryGetValue(id, out WebSocket socket) 
                       ? socket 
                       : null;
        }


        public string GetSocketId(WebSocket socket)
        {
            foreach (KeyValuePair<string, WebSocket> entry in this.sockets)
            {
                if (entry.Value == socket)
                    return entry.Key;
            }

            return null;
        }


        public async Task RemoveSocket(WebSocket socket)
        {
            await RemoveSocket(GetSocketId(socket));
        }


        public async Task RemoveSocket(string id)
        {
            if (this.sockets.TryRemove(id, out WebSocket socket))
            {
                await socket.CloseAsync(
                    closeStatus: WebSocketCloseStatus.NormalClosure,
                    statusDescription: "Closed by the ConnectionManager",
                    cancellationToken: CancellationToken.None);
            }
        }
    }
}