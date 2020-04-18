namespace HackThePlanet
{
  using WebSocketSharp;
  using Newtonsoft.Json;


  public class EchoEndpoint : WebsocketEndpoint
  {
    protected override void OnMessage(MessageEventArgs message)
    {
      object terminal = new
      {
        Update = "Terminal",
        Payload = new
        {
          Message = "Linksys v1.25.1720  \\nEnter [help](help) for a list of built-in commands."
        }
      };
      Send(JsonConvert.SerializeObject(terminal));
      object resources = new
      {
        Update = "Resources",
        Payload = new
        {
          Bitcoin = "0.083721",
          Devices = new[]{
            new {
              Ip = "199.201.159.1",
              Status = "SSH Crack (40%)",
              Commands = new[]{"[Disconnect](disconnect|199.201.159.1)"}
            },
            new {
              Ip = "8.8.8.8",
              Status = "Disconnected",
              Commands = new[]{"[Portscan](portscan|8.8.8.8)"}
            }
          }
        }
      };
      Send(JsonConvert.SerializeObject(resources));
    }
  }

}