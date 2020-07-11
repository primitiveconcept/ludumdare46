namespace HackThePlanet
{
    public class WiFiSniffApplication : IApplication
    {
        #region Properties
        public string Name
        {
            get { return "wifisniff"; }
        }


        public int OriginEntityId { get; set; }
        public ushort ProcessId { get; set; }
        public float? Progress { get; }
        public ushort RamUse { get; }
        public StringReference User { get; set; }
        #endregion
    }
}