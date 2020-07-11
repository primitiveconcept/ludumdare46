namespace HackThePlanet
{
    using System;
    using PrimitiveEngine;

    
    [EntitySystem(UpdateType = UpdateType.FixedUpdate)]
    public class PortScanSystem : EntityComponentProcessingSystem<
        ProcessPool<PortScanApplication>, 
        ComputerComponent>
    {
        private const float Delay = 0.5f; // In seconds


        public override void Process(
            Entity originEntity, 
            ProcessPool<PortScanApplication> processPool,
            ComputerComponent originComputer)
        {
            foreach (PortScanApplication process in processPool)
            {
                if (Process(originEntity, process, originComputer))
                    continue;
             
                // Clean up if application is finished.
                processPool.KillProcess(process.ProcessId);
                originComputer.RunningApplications.Remove(process.ProcessId);
            }
        }


        /// <summary>
        /// Process each porstcan application.
        /// </summary>
        /// <returns>Whether system should continue processing after this iteration.</returns>
        public bool Process(
            Entity originEntity,
            PortScanApplication portScan,
            ComputerComponent originComputer)
        {
            if (portScan.SecondsSinceLastUpdate < Delay)
            {
                portScan.SecondsSinceLastUpdate += Game.Time.ElapsedSeconds;
                return true;
            }

            PlayerComponent player = originEntity.GetComponent<PlayerComponent>();
            ComputerComponent targetComputer = Game.World.GetEntityById(portScan.TargetEntityId)?
                .GetComponent<ComputerComponent>();

            if (!ValidateHost(
                    targetComputer: targetComputer, 
                    player: player))
            {
                return false;
            }
                
            ScanCurrentPort(
                portScan: portScan, 
                targetComputer: targetComputer, 
                player: player);

            if (!IncrementCurrentPort(portScan))
            {
                
                return false;
            }

            return true;
        }


        private static bool IncrementCurrentPort(PortScanApplication portScanComponent)
        {
            if (portScanComponent.CurrentPort != EnumExtensions.GetLast<Port>())
            {
                portScanComponent.CurrentPort = portScanComponent.CurrentPort.Next();
                Array portsToScan = Enum.GetValues(typeof(Port));
                float portIterationNumber = Array.IndexOf(portsToScan, portScanComponent.CurrentPort) + 1;
                portScanComponent.Progress = portIterationNumber / portsToScan.Length;
                return true;
            }

            portScanComponent.Progress = 1;
            
            return false;
        }


        private static void ScanCurrentPort(
            PortScanApplication portScan,
            ComputerComponent targetComputer,
            PlayerComponent player)
        {
            foreach (
                IApplication application
                in targetComputer.RunningApplications.Values)
            {
                IServerApplication serverApplication = application as IServerApplication;

                if (serverApplication != null
                    && serverApplication.Port == portScan.CurrentPort)
                {
                    portScan.OpenPorts.Add(serverApplication.Port);
                    SendPlayerUpdate(
                        portScan: portScan, 
                        player: player, 
                        serverApplication: serverApplication);
                    break;
                }
            }
        }


        private static void SendPlayerUpdate(
            PortScanApplication portScan,
            PlayerComponent player,
            IServerApplication serverApplication)
        {
            if (player == null)
                return;
            
            ProcessInfo processInfo = new ProcessInfo(portScan);
            ProcessUpdateMessage processUpdate =
                ProcessUpdateMessage.Create(
                    player.Id,
                    processInfo,
                    player.GetPublicIP());
            processUpdate.Message = $"Found open port: {(int)serverApplication.Port} "
                                    + $"[{serverApplication.Port.ToString().ToUpper()}]";
            
            Game.SendMessageToClient(player.Id, processUpdate.ToJson());

            // TODO: Have debug config use this. 
            //Game.SendMessageToClient(
            //    player.Id,
            //    TerminalUpdateMessage.Create(
            //        $"Found open port: {(int)serverApplication.Port} "
            //        + $"[{serverApplication.Port.ToString().ToUpper()}]"));
        }


        private static bool ValidateHost(
            ComputerComponent targetComputer, 
            PlayerComponent player)
        {
            if (targetComputer == null)
            {
                Game.SendMessageToClient(
                    player.Id,
                    new TerminalUpdateMessage($"Timed out connecting to host.").ToJson());
                return false;
            }

            return true;
        }
    }
}