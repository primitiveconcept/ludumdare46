namespace HackThePlanet
{
    using System;
    using PrimitiveEngine;


    public static class PlayerGenerator
    {
        internal static PlayerComponent Generate()
        {
            Entity playerEntity = Game.World.CreateEntity();
            
            // Player component
            PlayerComponent player = new PlayerComponent();
            player.Id = Guid.NewGuid().ToString();
            player.Name = String.Empty;
            playerEntity.AddComponent(player);

            // Player computer
            ComputerComponent playerComputer = ComputerGenerator.Generate();
            playerEntity.AddComponent(playerComputer);

            // Player network device
            NetworkDeviceComponent networkDevice = new NetworkDeviceComponent();
            IP playerIP = IPGenerator.GenerateRandom();
            NetworkInterface networkInterface = new NetworkInterface(playerIP);
            networkDevice.AddNetworkInterface(networkInterface);
            playerEntity.AddComponent(networkDevice);
            Console.Out.WriteLine($"New Player IP: {playerIP}");
            
            // Create a new ISP for the player if there isn't an existing one close enough.
            // TODO: Find closest ISP, just creates a new one for all players right now.
            IP ispIP = IPGenerator.GenerateGatewayAddressFor(playerIP);
            NetworkDeviceComponent newIsp = Game.GlobalNetwork.CreateIsp(ispIP);
            newIsp.GetMainInterface().EstablishTwoWayLink(networkInterface);
            Console.Out.WriteLine($"New ISP IP: {ispIP}");

            return player;
        }
    }
}