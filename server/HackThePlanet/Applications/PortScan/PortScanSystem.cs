namespace HackThePlanet
{
    using PrimitiveEngine;

    
    [EntitySystem(UpdateType = UpdateType.FixedUpdate)]
    public class PortScanSystem : EntityComponentProcessingSystem<PortScanComponent>
    {
        private const long Delay = 20000;


        public override void Process(
            Entity portscanEntity,
            PortScanComponent portScanComponent)
        {
            if (portScanComponent.TicksSinceLastUpdate < Delay)
            {
                portScanComponent.TicksSinceLastUpdate += Game.Time.ElapsedTime;
                return;
            }

            PlayerComponent player = Game.World.GetEntityById(portScanComponent.OriginEntityId)?
                .GetComponent<PlayerComponent>();
            ComputerComponent targetComputer = Game.World.GetEntityById(portScanComponent.TargetEntityId)?
                .GetComponent<ComputerComponent>();

            if (!ValidateHost(
                    portscanEntity: portscanEntity, 
                    targetComputer: targetComputer, 
                    player: player))
            {
                return;
            }
                
            ScanCurrentPort(
                portScanComponent: portScanComponent, 
                targetComputer: targetComputer, 
                player: player);

            if (!IncrementCurrentPort(portScanComponent))
            {
                FinishPortScan(
                    portscanEntity: portscanEntity, 
                    player: player);
            }
        }


        private static void FinishPortScan(Entity portscanEntity, PlayerComponent player)
        {
            Game.SendMessageToClient(
                player.Id,
                TerminalUpdateMessage.Create("Finished port scan"));
            Game.World.EntityManager.Remove(portscanEntity);
        }


        private static bool IncrementCurrentPort(PortScanComponent portScanComponent)
        {
            if (portScanComponent.CurrentPort != EnumExtensions.GetLast<Port>())
            {
                portScanComponent.CurrentPort = portScanComponent.CurrentPort.Next();
                return true;
            }

            return false;
        }


        private static void ScanCurrentPort(
            PortScanComponent portScanComponent,
            ComputerComponent targetComputer,
            PlayerComponent player)
        {
            foreach (
                int processEntityId
                in targetComputer.RunningApplications.Values)
            {
                IServerComponent serverComponent = Game.World.GetEntityById(processEntityId)?
                                                       .Components?[0] as IServerComponent;

                if (serverComponent != null
                    && serverComponent.Port == portScanComponent.CurrentPort)
                {
                    portScanComponent.OpenPorts.Add(serverComponent.Port);

                    if (player != null)
                    {
                        Game.SendMessageToClient(
                            player.Id, 
                            TerminalUpdateMessage.Create($"Found open port: {(int)serverComponent.Port} "
                                                         + $"[{serverComponent.Port.ToString().ToUpper()}]"));
                    }
                        
                    // TODO: Component update message per port found

                    break;
                }
            }
        }


        private static bool ValidateHost(Entity portscanEntity, ComputerComponent targetComputer, PlayerComponent player)
        {
            if (targetComputer == null)
            {
                Game.SendMessageToClient(
                    player.Id,
                    TerminalUpdateMessage.Create($"Timed out connecting to host."));
                Game.World.EntityManager.Remove(portscanEntity);
                return false;
            }

            return true;
        }
    }
}