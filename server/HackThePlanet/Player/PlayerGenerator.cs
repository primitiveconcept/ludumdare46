namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using PrimitiveEngine;


    public static class PlayerGenerator
    {
        internal static Entity GeneratePlayerEntity()
        {
            Entity playerEntity = Game.World.CreateEntity();
            
            // Player component
            PlayerComponent player = new PlayerComponent();
            player.Id = playerEntity.Id;
            player.Name = "root";
            player.KnownIPs.Add(
                "127.0.0.1", 
                new List<AvailableIPActions>
                    {
                        AvailableIPActions.Portscan, 
                        AvailableIPActions.Ssh
                    });
            playerEntity.AddComponent(player);

            // Player computer
            ComputerComponent playerComputer = ComputerGenerator.Generate();
            playerEntity.AddComponent(playerComputer);

            // Player network device
            NetworkDeviceComponent networkDevice = new NetworkDeviceComponent();
            IP playerIP = IPGenerator.GenerateRandomPublic();
            NetworkInterface networkInterface = new NetworkInterface(networkDevice, playerIP);
            NetworkInterface loopbackInterface = new NetworkInterface(networkDevice, "127.0.0.1");
            networkDevice.AddNetworkInterface(networkInterface);
            networkDevice.AddNetworkInterface(loopbackInterface);
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
            newIsp.GetPublicInterface().EstablishTwoWayLink(networkInterface);
            Console.Out.WriteLine($"New ISP IP: {ispIP}");

            return playerEntity;
        }
    }
}