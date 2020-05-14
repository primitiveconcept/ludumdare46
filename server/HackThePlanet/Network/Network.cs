namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PrimitiveEngine;


    public class Network : IGraph<NetworkInterface>
    {
        private Dictionary<string, IP> domains = new Dictionary<string, IP>();
        private Dictionary<IP, NetworkInterface> publicInterfaces  = 
            new Dictionary<IP, NetworkInterface>();
        private List<NetworkInterface> serviceProviders = new List<NetworkInterface>();


        public NetworkDeviceComponent CreateServiceProvider(IP gatewayIP)
        {
            Entity newIspEntity = Game.World.CreateEntity();
            NetworkDeviceComponent newIspRouter = NetworkDeviceGenerator.GenerateIspRouter(gatewayIP);
            newIspEntity.AddComponent(newIspRouter);
            NetworkInterface newIspNetworkInterface = newIspRouter.GetPublicInterface();

            foreach (NetworkInterface isp in this.serviceProviders)
            {
                isp.EstablishTwoWayLink(newIspNetworkInterface);
            }

            this.serviceProviders.Add(newIspNetworkInterface);

            return newIspRouter;
        }


        public ComputerComponent GetComputer(IP ip, Port port)
        {
            if (!this.publicInterfaces.ContainsKey(ip))
                return null;

            return this.publicInterfaces[ip]
                .GetInterfaceForPort(port)?
                .HostDevice?
                .GetSiblingComponent<ComputerComponent>();
        }


        public ComputerComponent GetComputer(IP ip)
        {
            if (!this.publicInterfaces.ContainsKey(ip))
                return null;

            return this.publicInterfaces[ip]
                .HostDevice?
                .GetSiblingComponent<ComputerComponent>();
        }


        public IList<IGraphNodeConnection<NetworkInterface>> GetConnections(NetworkInterface node)
        {
            return new List<IGraphNodeConnection<NetworkInterface>>(
                node.Connections.Cast<IGraphNodeConnection<NetworkInterface>>());
        }


        public IP? GetDomainIP(string domain)
        {
            domain = domain.ToLower();
            if (!domain.Contains(domain))
                return null;
            
            return this.domains[domain];
        }


        public IP? GetIP(Entity entity)
        {
            NetworkDeviceComponent networkDeviceComponent = entity?.GetComponent<NetworkDeviceComponent>();
            return networkDeviceComponent != null
                       ? networkDeviceComponent.GetPublicInterface().IP
                       : null;
        }


        public IP? GetIP(int entityId)
        {
            return GetIP(Game.World.GetEntityById(entityId));
        }


        public NetworkRoute GetRoute(
            IP sourceIP, 
            IP destinationIP)
        {
            if (!this.publicInterfaces.ContainsKey(sourceIP))
                return null;
            NetworkInterface source = this.publicInterfaces[sourceIP];

            if (destinationIP.IsLoopbackAddress())
            {
                Console.Out.WriteLine("Loopback device");
                return new NetworkRoute(source);
            }
                

            if (!this.publicInterfaces.ContainsKey(destinationIP))
                return null;
            
            NetworkInterface destination = this.publicInterfaces[destinationIP];
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
            this.publicInterfaces.Add(networkInterface.IP, networkInterface);
        }
    }
}