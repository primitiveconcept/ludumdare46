#pragma warning disable 0618

namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using PrimitiveEngine;


    public static class Player
    {
        public static Dictionary<string, Entity> Index = new Dictionary<string, Entity>();
        
        public static Entity CreateNew(GameEndpoint session)
        {
            // Add new player entity.
            // Temporary until we get a proper "login" sequence going.
            Entity playerEntity = Game.World.CreateEntity();
				
            PlayerComponent playerComponent = new PlayerComponent();
            playerComponent.Id = Guid.NewGuid().ToString();
            playerEntity.AddComponent(playerComponent);

            Index.Add(playerComponent.Id, playerEntity);
			
            ComputerComponent computerComponent = new ComputerComponent();
            computerComponent.IpAddress = IPExtensions.Generate();
            computerComponent.OpenPorts.Add(Port.Ssh);
            computerComponent.OpenPorts.Add(Port.Ftp);
            computerComponent.OpenPorts.Add(Port.Http);
            computerComponent.OpenPorts.Add(Port.Ssl);
            computerComponent.OpenPorts.Add(Port.Smtp);
            computerComponent.OpenPorts.Add(Port.Imap);
            playerEntity.AddComponent(computerComponent);
			
            NetworkAccessComponent networkAccessComponent = new NetworkAccessComponent();
            networkAccessComponent.AccessOptions.Add(playerEntity.Id, new AccessOptions());
            playerEntity.AddComponent(networkAccessComponent);
			
            Console.Out.WriteLine($"Created new player: {playerComponent.Id}");
			
            return playerEntity;
        }


        public static Entity Find(string playerId)
        {
            return Index.ContainsKey(playerId) 
                       ? Index[playerId] 
                       : null;
        }
    }
}