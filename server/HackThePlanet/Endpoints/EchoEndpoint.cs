namespace HackThePlanet
{
  using WebSocketSharp;


  public class EchoEndpoint : WebsocketEndpoint
  {
    protected override void OnMessage(MessageEventArgs message)
    {
      Send("{ \"type\": \"INITIAL_STATE\", \"payload\": { \"nodeCount\": 0, \"Bitcoin\": 1, \"AvailableCommands\": [\"SSH\"], \"knownDevices\": [{ \"ip\": \"199.201.159.1\" }], \"message\": \"Linksys v1.25.1720  \\nEnter [help](help) for a list of built-in commands.\"}}");
    }
  }

}