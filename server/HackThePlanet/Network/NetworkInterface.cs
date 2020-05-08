namespace HackThePlanet
{
    using System.Collections.Generic;


    public class NetworkInterface
    {
        public IP IP;
        public NetworkDeviceComponent HostDevice;
        public List<NetworkConnection> Connections = new List<NetworkConnection>();


        #region Constructors
        public NetworkInterface(NetworkDeviceComponent hostDevice, IP ip)
        {
            this.HostDevice = hostDevice;
            this.IP = ip;
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
    }
}