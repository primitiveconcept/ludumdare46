namespace HackThePlanet
{
    public class BashApplication : IShell
    {
        public ushort ProcessId { get; set; }
        public int OriginEntityId { get; set; }
        public ushort RamUse
        {
            get { return 15; }
        }
        public StringReference User { get; set; }
        public UserAccount ActiveUser { get; set; }
    }
}