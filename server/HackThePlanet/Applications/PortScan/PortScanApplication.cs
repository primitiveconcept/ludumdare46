namespace HackThePlanet
{
    using System.Collections.Generic;


    public class PortScanApplication : IApplication
    {
        private const string PortScan = "portscan";


        #region Properties
        public Port CurrentPort { get; set; }


        public string Name
        {
            get { return PortScan; }
        }


        public List<Port> OpenPorts { get; set; }  = new List<Port>();
        public int OriginEntityId { get; set; }
        public ushort ProcessId { get; set; }

        public float? Progress { get; set; }


        public ushort RamUse
        {
            get { return 1; }
        }


        public float SecondsSinceLastUpdate { get; set; }

        public int TargetEntityId { get; set; }
        public StringReference User { get; set; }
        #endregion
    }
}