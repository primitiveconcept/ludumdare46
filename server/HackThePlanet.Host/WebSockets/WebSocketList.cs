namespace HackThePlanet.Host
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Net.WebSockets;
    using System.Threading;
    using System.Threading.Tasks;


    public class WebSocketList
    {
        private ConcurrentDictionary<int, WebSocket> sockets = new ConcurrentDictionary<int, WebSocket>();


        #region Properties
        public ConcurrentDictionary<int, WebSocket> Sockets
        {
            get { return this.sockets; }
        }
        #endregion


        public void AddSocket(int id, WebSocket socket)
        {
            this.sockets.TryAdd(id, socket);
        }


        public WebSocket GetSocket(int id)
        {
            return this.sockets.TryGetValue(id, out WebSocket socket) 
                       ? socket 
                       : null;
        }


        public int GetSocketId(WebSocket socket)
        {
            foreach (KeyValuePair<int, WebSocket> entry in this.sockets)
            {
                if (entry.Value == socket)
                    return entry.Key;
            }

            return 0;
        }


        public async Task RemoveSocket(WebSocket socket)
        {
            await RemoveSocket(GetSocketId(socket));
        }


        public async Task RemoveSocket(int id)
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