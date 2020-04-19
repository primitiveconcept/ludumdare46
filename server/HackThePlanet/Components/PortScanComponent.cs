namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class PortScanComponent : IEntityComponent
    {
        public int InitiatingEntity;
        public List<int> OpenPorts;
        public Port CurrentPort;
    }
}