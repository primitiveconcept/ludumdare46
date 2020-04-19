namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using PrimitiveEngine;

    
    [Serializable]
    public class NetworkAccessComponent : IEntityComponent
    {
        public Dictionary<int, AccessLevel> KnownEntities = new Dictionary<int, AccessLevel>();
    }
}