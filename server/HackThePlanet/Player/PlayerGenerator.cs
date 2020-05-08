namespace HackThePlanet
{
    using System;
    using PrimitiveEngine;


    public static class PlayerGenerator
    {
        internal static Entity GeneratePlayerEntity()
        {
            Entity playerEntity = Game.World.CreateEntity();
            
            // Player component
            PlayerComponent player = new PlayerComponent();
            player.Id = playerEntity.Id;
            player.Name = String.Empty;
            playerEntity.AddComponent(player);

            // Player computer
            ComputerComponent playerComputer = ComputerGenerator.Generate();
            playerEntity.AddComponent(playerComputer);

            // Player network device
            NetworkDeviceComponent networkDevice = new NetworkDeviceComponent();
            IP playerIP = IPGenerator.GenerateRandom();
            NetworkInterface networkInterface = new NetworkInterface(networkDevice, playerIP);
            networkDevice.AddNetworkInterface(networkInterface);
            playerEntity.AddComponent(networkDevice);
            Console.Out.WriteLine($"New Player IP: {playerIP}");
            
            // TODO: Temporary, for testing
            SshServerApplication sshServer =
                ProcessPool<SshServerApplication>.RunApplication(playerComputer);
            sshServer.Accounts.Add(new UserAccount()
                                       {
                                           UserName = "root",
                                           Password = "god",
                                           AccessLevel = AccessLevel.Root
                                       });
            
            // Create a new ISP for the player if there isn't an existing one close enough.
            // TODO: Find closest ISP, just creates a new one for all players right now.
            IP ispIP = IPGenerator.GenerateGatewayAddressFor(playerIP);
            NetworkDeviceComponent newIsp = Game.Internet.CreateServiceProvider(ispIP);
            newIsp.GetMainInterface().EstablishTwoWayLink(networkInterface);
            Console.Out.WriteLine($"New ISP IP: {ispIP}");

            return playerEntity;
        }
    }
}