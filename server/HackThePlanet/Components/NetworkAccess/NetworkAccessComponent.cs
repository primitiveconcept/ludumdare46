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
            return GetAvailableCommands(Game.GetEntity(entityId));
        }


        public List<string> GetAvailableCommands(Entity entity)
        {
            List<string> commands = new List<string>();
            ComputerComponent targetComputer = entity.GetComponent<ComputerComponent>();
            return this.AccessOptions[entity.Id].GetAccessOptions(targetComputer.IpAddress.ToIPString());
        }
    }
}