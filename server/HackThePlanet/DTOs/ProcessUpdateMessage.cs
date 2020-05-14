namespace HackThePlanet
{
    using Newtonsoft.Json;


    public class ProcessUpdateMessage : UpdateMessage<ProcessInfo>
    {
        private const string UpdateType = "Process";


        #region Properties
        public override ProcessInfo Payload { get; set; }


        public override string Type
        {
            get { return UpdateType; }
        }
        #endregion


        public static ProcessUpdateMessage Create(
            int playerId, 
            ProcessInfo processInfo,
            IP? ip)
        {
            ProcessUpdateMessage message = new ProcessUpdateMessage();
            message.IP = ip;
            message.Payload = processInfo;

            return message;
        }
    }
}