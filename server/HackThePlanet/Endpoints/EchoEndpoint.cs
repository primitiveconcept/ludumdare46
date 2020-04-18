namespace HackThePlanet
{
  using WebSocketSharp;


  public class EchoEndpoint : WebsocketEndpoint
  {
    protected override void OnMessage(MessageEventArgs message)
    {
      Send("{ \"type\": \"INITIAL_STATE\", \"payload\": { \"nodeCount\": 0, \"bitcoin\": 1, \"availableCommands\": [\"SSH\"], \"knownDevices\": [{ \"ip\": \"199.201.159.1\" }], \"message\": \"Welcome\"}}");
    }
  }

}