namespace HackThePlanet
{
    using System.Collections.Generic;
    using System.Text;


    [Command("traceroute")]
    public class TraceRouteCommand : Command
    {
        public override string Execute(int playerId)
        {
            NetworkInterface playerNetworkInterface = Game.Players.GetPlayer(playerId)?
                .GetSiblingComponent<NetworkDeviceComponent>()?
                .GetPublicInterface();

            if (playerNetworkInterface == null)
                return "No network device found.";
            
            IP playerIP = playerNetworkInterface.IP;
            IP destinationIP = GetArgument(0);
            
            List<NetworkInterface> route = Game.Internet.GetRoute(playerIP, destinationIP);
            
            if (route == null)
                return $"Timed out connecting to {destinationIP}";
            
            StringBuilder result = new StringBuilder();
            result.Append($"Tracing route to {destinationIP} over a maximum of 30 hops<br>");
            result.Append(StringExtensions.RepeatCharacter('-', result.ToString().Length - 4) + "<br>");
            
            result.Append("<table>");
            result.Append("<tr><th>HOP</th><th>RTT1</th><th>RTT2</th><th>RTT3</th><th>IP</th></tr>");
            for (int hop = 0; hop < route.Count; hop++)
            {
                // Reference: Hop RTT1 RTT2 IP
                result.Append($"<tr><td>{hop + 1}</td><td>30ms</td><td>30ms</td><td>30ms</td><td>{route[hop].IP}</td></tr>");
            }
            result.Append("<table>");

            return result.ToString();
        }
    }
}