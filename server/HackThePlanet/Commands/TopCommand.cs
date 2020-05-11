namespace HackThePlanet
{
    using System.Text;


    [Command("top")]
    public class TopCommand : Command
    {
        public override string Execute(int playerId)
        {
            ComputerComponent playerComputer = Game.World
                .GetEntityById(playerId)
                .GetComponent<ComputerComponent>();

            // TODO: This should be an ongoing process/application.
            
            StringBuilder result = new StringBuilder();
            result.Append("<table><tr><th>PID</th><th>USER</th><th>RAM</th><th>COMMAND</th></tr>");
            foreach (var process in playerComputer.RunningApplications.Values)
            {
                result.Append($"<tr>"
                              + $"<td>{process.ProcessId}</td>"
                              + $"<td>root</td>"
                              + $"<td>{process.RamUse}</td>"
                              + $"<td>{process.Name}</td>"
                              + $"</tr>");
            }
            result.Append("</table>");

            return result.ToString();
        }
    }
}