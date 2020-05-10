namespace HackThePlanet
{
    public class SshApplication : IApplication
    {
        public ushort ProcessId { get; set; }
        public int OriginEntityId { get; set; }
        public int TargetEntityId { get; set; }
        public ushort RamUse
        {
            get { return 4; }
        }
        public StringReference User { get; set; }
    }
}