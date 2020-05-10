namespace HackThePlanet
{
    using System.Collections.Generic;
    using System.Net;


    public class NetworkInterface
    {
        public IP IP;
        public NetworkDeviceComponent HostDevice;
        public List<NetworkConnection> Connections = new List<NetworkConnection>();
        public Dictionary<Port, NetworkInterface> PortMappings = new Dictionary<Port, NetworkInterface>();


        #region Constructors
        public NetworkInterface(NetworkDeviceComponent hostDevice, IP ip)
        {
            this.HostDevice = hostDevice;
            this.IP = ip;
            if (this.IP.IsValidPublicAddress())
                Game.Internet.RegisterNetworkInterface(this);
        }
        #endregion


        public void EstablishLinkTo(NetworkInterface destination)
        {
            NetworkConnection networkConnection = new NetworkConnection(this, destination);
            if (!this.Connections.Contains(networkConnection))
                this.Connections.Add(networkConnection);
        }


        public void EstablishTwoWayLink(NetworkInterface networkInterface)
        {
            EstablishLinkTo(networkInterface);
            networkInterface.EstablishLinkTo(this);
        }


        /// <summary>
        /// Add or overwrite a port forward mapping.
        /// </summary>
        /// <param name="port">Port to map.</param>
        /// <param name="networkInterface">NetworkInterface to forward to.</param>
        public void ForwardPort(Port port, NetworkInterface networkInterface)
        {
            if (this.PortMappings.ContainsKey(port))
                this.PortMappings[port] = networkInterface;
            else
                this.PortMappings.Add(port, networkInterface);
        }
        

        public NetworkInterface GetInterfaceForPort(Port port)
        {
            return this.PortMappings.ContainsKey(port) 
                       ? this.PortMappings[port] 
                       : null;
        }
    }
}