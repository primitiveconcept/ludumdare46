namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Text;


    [Command("traceroute")]
    public class TraceRouteCommand : Command
    {
        public override string Execute(int playerId)
        {
            NetworkInterface playerNetworkInterface = Game.Players.GetPlayer(playerId)?
                .GetSiblingComponent<NetworkDeviceComponent>()?
                .GetMainInterface();

            // TODO: Null ref check
            
            IP playerIP = playerNetworkInterface.IP;
         
            IP destinationIP = GetArgument(0);
            IList<NetworkInterface> route = Game.Internet.GetRoute(playerIP, destinationIP).GetShortest();
            
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