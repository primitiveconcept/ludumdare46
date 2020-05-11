namespace HackThePlanet
{
    public class ProcessInfo
    {
        #region Properties
        public string Name { get; set; }
        public IP? IP { get; set; }
        public ushort ProcessId { get; set; }
        public float? Progress { get; set; }
        public ushort RamUse { get; set; }
        
        #endregion
        
        public ProcessInfo (IApplication application)
        {
            this.Name = application.Name;
            this.ProcessId = application.ProcessId;
            this.Progress = application.Progress;
            this.RamUse = application.RamUse;

            this.IP = Game.Internet.GetIP(application.OriginEntityId);
        }
    }
}