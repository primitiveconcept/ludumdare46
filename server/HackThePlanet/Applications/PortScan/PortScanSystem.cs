namespace HackThePlanet
{
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
                portScanComponent: portScan, 
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
                return true;
            }

            return false;
        }


        private static void ScanCurrentPort(
            PortScanApplication portScanComponent,
            ComputerComponent targetComputer,
            PlayerComponent player)
        {
            foreach (
                IApplication processEntityId
                in targetComputer.RunningApplications.Values)
            {
                IServerApplication serverApplication = processEntityId as IServerApplication;

                if (serverApplication != null
                    && serverApplication.Port == portScanComponent.CurrentPort)
                {
                    portScanComponent.OpenPorts.Add(serverApplication.Port);

                    if (player != null)
                    {
                        Game.SendMessageToClient(
                            player.Id, 
                            TerminalUpdateMessage.Create($"Found open port: {(int)serverApplication.Port} "
                                                         + $"[{serverApplication.Port.ToString().ToUpper()}]"));
                    }
                        
                    // TODO: Component update message per port found

                    break;
                }
            }
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