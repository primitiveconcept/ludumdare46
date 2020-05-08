namespace HackThePlanet
{
    using System.Collections.Generic;
    using System.Linq;
    using PrimitiveEngine;


    public class NetworkDeviceComponent : 
        IEntityComponent
    {
        private Dictionary<IP, NetworkInterface> networkInterfaces = new Dictionary<IP, NetworkInterface>();


        public NetworkInterface GetMainInterface()
        {
            return this.networkInterfaces.First().Value;
        }
        

        public void AddNetworkInterface(NetworkInterface networkInterface)
        {
            this.networkInterfaces.Add(networkInterface.IP, networkInterface);
        }


        public NetworkInterface FindInterface(IP ip)
        {
            return this.networkInterfaces[ip];
        }
    }
}