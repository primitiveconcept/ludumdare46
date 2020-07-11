namespace HackThePlanet
{
    using PrimitiveEngine;


    public class NetworkConnection : IGraphNodeConnection<NetworkInterface>
    {
        public NetworkInterface Source { get; set; }
        public NetworkInterface Destination { get; set; }

        
        public NetworkConnection(NetworkInterface source, NetworkInterface destination)
        {
            this.Source = source;
            this.Destination = destination;
        }

        // TODO
        public bool CanTraverse()
        {
            return true;
        }
    }
}