namespace HackThePlanet
{
    public class WiFiScanApplication : IApplication
    {
        public string Name
        {
            get { return "wifiscan"; }
        }
        public int OriginEntityId { get; set; }
        public ushort ProcessId { get; set; }
        public float? Progress { get; }
        public ushort RamUse { get; }
        public StringReference User { get; set; }
    }
}