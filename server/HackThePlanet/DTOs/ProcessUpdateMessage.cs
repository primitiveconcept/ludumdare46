namespace HackThePlanet
{
    public class ProcessUpdateMessage : UpdateMessage<ProcessInfo>
    {
        #region Constructors
        public ProcessUpdateMessage(
            int playerId, 
            ProcessInfo processInfo,
            IP? ip)
        {
            this.IP = ip;
            this.Payload = processInfo;

        }
        #endregion


        #region Properties
        public override ProcessInfo Payload { get; set; }


        public override string UpdateType
        {
            get { return "ProcessUpdate"; }
        }
        #endregion
    }
}