namespace HackThePlanet
{
    using System.Collections.Generic;


    public class PortScanApplication : IApplication
    {
        #region Properties
        public Port CurrentPort { get; set; }
        public List<Port> OpenPorts { get; set; }  = new List<Port>();
        public int OriginEntityId { get; set; }
        public ushort ProcessId { get; set; }


        public ushort RamUse
        {
            get { return 1; }
        }


        public int TargetEntityId { get; set; }
        public long TicksSinceLastUpdate { get; set; }
        public StringReference User { get; set; }
        #endregion
    }
}