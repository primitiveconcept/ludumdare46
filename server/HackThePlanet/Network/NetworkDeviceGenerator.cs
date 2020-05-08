namespace HackThePlanet
{
    using PrimitiveEngine;


    public static class NetworkDeviceGenerator
    {
        public static NetworkDeviceComponent GenerateIspRouter(IP gatewayIP)
        {
            NetworkDeviceComponent router = new NetworkDeviceComponent();
            NetworkInterface gatewayInterface = 
                new NetworkInterface(router, gatewayIP);
            router.AddNetworkInterface(gatewayInterface);
            
            return router;
        }
    }
}