namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public class PortScanApplicationComponent :
        IEntityComponent,
        IApplicationComponent
    {
        public int InitiatingEntity;
        public int TargetEntity;
        public long TicksSinceLastUpdate = 0;

        public int CurrentPort;
        public List<int> OpenPorts;

        
        #region Properties
        public string Command
        {
            get { return "portscan"; }
        }


        public ushort RamUse
        {
            get { return 1; }
        }


        public string Status { get; set; }
        #endregion
    }
}