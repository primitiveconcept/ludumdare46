namespace HackThePlanet
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public class PlayerStateMessage
    {
        public IP? PlayerIP;
        public IEnumerable<ProcessInfo> RunningProcesses;

        public static string Create(int playerId)
        {
            Entity playerEntity = Game.World.GetEntityById(playerId);
            if (playerEntity == null)
                return null;
         
            PlayerStateMessage message = new PlayerStateMessage();
            message.PlayerIP = Game.Internet.GetIP(playerEntity);

            ComputerComponent playerComputer = playerEntity.GetComponent<ComputerComponent>();
            List<ProcessInfo> runningProcesses = new List<ProcessInfo>();
            foreach (IApplication application in playerComputer.RunningApplications.Values)
            {
                ProcessInfo processInfo = new ProcessInfo(application);
                runningProcesses.Add(processInfo);
            }

            message.RunningProcesses = runningProcesses;

            return JsonConvert.SerializeObject(message);
        }
    }
}