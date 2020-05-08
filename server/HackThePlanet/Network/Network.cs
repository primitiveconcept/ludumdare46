namespace HackThePlanet
{
    using System.Collections.Generic;
    using System.Linq;
    using PrimitiveEngine;


    public class Network : IGraph<NetworkInterface>
    {
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


        public NetworkRoute GetRoute(
            IP sourceIP, 
            IP destinationIP)
        {
            NetworkInterface source = this.networkInterfaces[sourceIP];
            NetworkInterface destination = this.networkInterfaces[destinationIP];
            NetworkRoute route = new NetworkRoute(this, source, destination);

            return route;

        }


        public void RegisterNetworkInterface(NetworkInterface networkInterface)
        {
            this.networkInterfaces.Add(networkInterface.IP, networkInterface);
        }


        // TODO: Temporary, for testing. Remove later.
        public void Seed()
        {
            for (int i = 0; i < 3; i++)
            {
                Entity playerEntity = PlayerGenerator.GeneratePlayerEntity();
                ComputerComponent playerComputer = playerEntity.GetComponent<ComputerComponent>();
                
                // TODO: Temporary, for testing
                SshServerComponent sshServer = 
                    ApplicationComponent.RunOnComputer<SshServerComponent>(playerComputer);
                sshServer.Accounts.Add(new UserAccount()
                                           {
                                               UserName = "root",
                                               Password = "god",
                                               AccessLevel = AccessLevel.Root
                                           });
            }
        }
    }
}