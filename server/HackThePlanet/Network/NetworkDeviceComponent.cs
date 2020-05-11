namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class NetworkDeviceComponent : 
        IEntityComponent
    {
        private Dictionary<IP, NetworkInterface> networkInterfaces = new Dictionary<IP, NetworkInterface>();


        public NetworkInterface GetPublic()
        {
            foreach (NetworkInterface networkInterface in this.networkInterfaces.Values)
            {
                if (networkInterface.IP.IsValidPublicAddress())
                    return networkInterface;
            }

            return null;
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