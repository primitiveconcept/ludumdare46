namespace HackThePlanet
{
    using Newtonsoft.Json;


    public class ProcessUpdateMessage : UpdateMessage<ProcessInfo>
    {
        #region Properties
        public override ProcessInfo Payload { get; set; }


        public override string UpdateType
        {
            get { return "ProcessUpdate"; }
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