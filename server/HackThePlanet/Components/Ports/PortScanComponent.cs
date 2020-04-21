namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class PortScanComponent : 
        IEntityComponent, 
        IProcess
    {
        public int InitiatingEntity;
        public int TargetEntity;
        public long TicksSinceLastUpdate = 0;

        public Port CurrentPort;
        public List<int> OpenPorts;


        #region Properties
        public string Command
        {
            get { return "portscan"; }
        }


        public byte RamUse
        {
            get { return 1; }
        }


        public string Status { get; set; }
        #endregion
    }
}