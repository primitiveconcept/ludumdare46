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
                
                processPool.KillProcess(process.ProcessId);
                originComputer.RunningApplications.Remove(process.ProcessId);
            }
        }


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

            PlayerComponent player = originComputer.GetSiblingComponent<PlayerComponent>();
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
                FinishPortScan(
                    portscanEntity: originEntity, 
                    player: player);
                return false;
            }

            return true;
        }


        private static void FinishPortScan(Entity portscanEntity, PlayerComponent player)
        {
            Game.SendMessageToClient(
                player.Id,
                TerminalUpdateMessage.Create("Finished port scan"));
            
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

            // TODO: Remove once client IP can handle process updates. 
            Game.SendMessageToClient(
                player.Id,
                TerminalUpdateMessage.Create(
                    $"Found open port: {(int)serverApplication.Port} "
                    + $"[{serverApplication.Port.ToString().ToUpper()}]"));
        }


        private static bool ValidateHost(
            ComputerComponent targetComputer, 
            PlayerComponent player)
        {
            if (targetComputer == null)
            {
                Game.SendMessageToClient(
                    player.Id,
                    TerminalUpdateMessage.Create($"Timed out connecting to host."));
                return false;
            }

            return true;
        }
    }
}