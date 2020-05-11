namespace HackThePlanet
{
    public class SshApplication : IApplication
    {
        private const string Ssh = "ssh";


        #region Properties
        public string Name
        {
            get { return Ssh; }
        }


        public int OriginEntityId { get; set; }
        public ushort ProcessId { get; set; }


        public float? Progress
        {
            get { return null; }
        }


        public ushort RamUse
        {
            get { return 4; }
        }


        public int TargetEntityId { get; set; }
        public StringReference User { get; set; }
        #endregion
    }
}