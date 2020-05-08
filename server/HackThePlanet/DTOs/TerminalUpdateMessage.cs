namespace HackThePlanet
{
    using Newtonsoft.Json;


    // TODO: Temporary structure until we move to the new payload system.
    public class TerminalUpdateMessage
    {
        public string Update = "Terminal";
        public object Payload;


        public static string Create(string message)
        {
            TerminalUpdateMessage update = new TerminalUpdateMessage();
            update.Payload = new { Message = message };
            return JsonConvert.SerializeObject(update);
        }
    }
}