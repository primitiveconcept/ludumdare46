namespace HackThePlanet
{
    public class BashApplication : IShell
    {
        private const string Bash = "bash";


        #region Properties
        public UserAccount ActiveUser { get; set; }


        public string Name
        {
            get { return Bash; }
        }


        public int OriginEntityId { get; set; }
        public ushort ProcessId { get; set; }


        public float? Progress
        {
            get { return null; }
        }


        public ushort RamUse
        {
            get { return 15; }
        }


        public StringReference User { get; set; }
        #endregion
    }
}