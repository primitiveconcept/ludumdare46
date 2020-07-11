namespace HackThePlanet
{
    using Newtonsoft.Json;


    // TODO: Temporary structure until we move to the new payload system.
    public class TerminalUpdateMessage
    {
        public string Update = "Terminal";
        public object Payload;


        public TerminalUpdateMessage(string message)
        {
            this.Payload = new { Message = message }; 
        }


        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}