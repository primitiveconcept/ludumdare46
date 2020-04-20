namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using PrimitiveEngine;

    
    [Serializable]
    public class NetworkAccessComponent : IEntityComponent
    {
        public Dictionary<int, AccessOptions> AccessOptions = new Dictionary<int, AccessOptions>();


        public List<string> GetAvailableCommands(int entityId)
        {
            List<string> commands = new List<string>();
            Entity targetEntity = Game.GetEntity(entityId);
            ComputerComponent targetComputer = targetEntity.GetComponent<ComputerComponent>();
            return this.AccessOptions[entityId].GetAccessOptions(targetComputer.IpAddress.ToIPString());
        }
    }
}