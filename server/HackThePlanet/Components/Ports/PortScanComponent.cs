namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class PortScanComponent : IEntityComponent
    {
        public int InitiatingEntity;
        public int TargetEntity;
        public long elapsedTime = 0;
        
        public Port CurrentPort;
        public List<int> OpenPorts;
    }
}