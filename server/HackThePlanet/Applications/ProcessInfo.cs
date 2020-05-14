namespace HackThePlanet
{
    public class ProcessInfo
    {
        #region Constructors
        public ProcessInfo (IApplication application)
        {
            this.Name = application.Name;
            this.ProcessId = application.ProcessId;
            this.Progress = application.Progress;
            this.RamUse = application.RamUse;

            this.IP = Game.Internet.GetIP(application.OriginEntityId);
        }
        #endregion


        #region Properties
        public IP? IP;
        public string Name;
        public ushort ProcessId;
        public float? Progress;
        public ushort RamUse;
        #endregion
    }
}