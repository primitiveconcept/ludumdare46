namespace HackThePlanet
{
    using System.Collections.Generic;
    using System.Linq;
    using PrimitiveEngine;


    public class Network : IGraph<NetworkInterface>
    {
        private Dictionary<string, IP> domains = new Dictionary<string, IP>();
        private Dictionary<IP, NetworkInterface> networkInterfaces  = 
            new Dictionary<IP, NetworkInterface>();
        private List<NetworkInterface> serviceProviders = new List<NetworkInterface>();


        public NetworkDeviceComponent CreateServiceProvider(IP gatewayIP)
        {
            Entity newIspEntity = Game.World.CreateEntity();
            NetworkDeviceComponent newIspRouter = NetworkDeviceGenerator.GenerateIspRouter(gatewayIP);
            newIspEntity.AddComponent(newIspRouter);
            NetworkInterface newIspNetworkInterface = newIspRouter.GetMainInterface();

            foreach (NetworkInterface isp in this.serviceProviders)
            {
                isp.EstablishTwoWayLink(newIspNetworkInterface);
            }

            this.serviceProviders.Add(newIspNetworkInterface);

            return newIspRouter;
        }


        public IList<IGraphNodeConnection<NetworkInterface>> GetConnections(NetworkInterface node)
        {
            return new List<IGraphNodeConnection<NetworkInterface>>(
                node.Connections.Cast<IGraphNodeConnection<NetworkInterface>>());
        }


        public IP GetDomainIP(string domain)
        {
            domain = domain.ToLower();
            if (!domain.Contains(domain))
                return default;
            
            return this.domains[domain];
        }


        public NetworkRoute GetRoute(
            IP sourceIP, 
            IP destinationIP)
        {
            if (!this.networkInterfaces.ContainsKey(sourceIP))
                return null;
            if (!this.networkInterfaces.ContainsKey(destinationIP))
                return null;
            
            NetworkInterface source = this.networkInterfaces[sourceIP];
            NetworkInterface destination = this.networkInterfaces[destinationIP];
            NetworkRoute route = new NetworkRoute(this, source, destination);

            return route;

        }


        public void RegisterDomain(string domainAddress, IP mappedIP)
        {
            domainAddress = domainAddress.ToLower();
            if (!this.domains.ContainsKey(domainAddress))
                this.domains.Add(domainAddress, mappedIP);
        }


        public void RegisterNetworkInterface(NetworkInterface networkInterface)
        {
            this.networkInterfaces.Add(networkInterface.IP, networkInterface);
        }
    }
}