namespace HackThePlanet.Systems
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
            Entity initiatingEntity = Game.GetEntity(portScanComponent.InitiatingEntity);
            Entity targetEntity = Game.GetEntity(portScanComponent.TargetEntity);
            ComputerComponent targetComputer = targetEntity.GetComponent<ComputerComponent>();
            
            if (portScanComponent.TicksSinceLastUpdate < Delay)
            {
                portScanComponent.TicksSinceLastUpdate += Game.Time.ElapsedTime;
                return;
            }

            portScanComponent.TicksSinceLastUpdate = 0;

            PlayerComponent initiatingPlayer = initiatingEntity.GetComponent<PlayerComponent>();

            // Found an open port.
            if (targetComputer.OpenPorts.Contains(portScanComponent.CurrentPort))
            {
                NetworkAccessComponent networkAccessComponent = 
                    initiatingEntity.GetComponent<NetworkAccessComponent>();
                networkAccessComponent.AccessOptions[targetEntity.Id]
                    .PortAccessability[portScanComponent.CurrentPort] = AccessLevel.Known;
                
                initiatingPlayer.QueueTerminalMessage(
                    $"[{targetComputer.IpAddress.ToIPString()}] Found open port: "
                    + $"{portScanComponent.CurrentPort.ToString().ToLower()}");
            }

            // Scan next port.
            if (portScanComponent.CurrentPort != EnumExtensions.GetLast<Port>())
            {
                portScanComponent.CurrentPort = portScanComponent.CurrentPort.Next();
            }
            
            // Done port scanning.
            else
            {
                DeviceState device = new DeviceState();
                device.ip = targetComputer.IpAddress.ToIPString();
                device.status = "idle";
                device.commands = initiatingEntity.NetworkAccessComponent().GetAvailableCommands(targetEntity);
                
                initiatingPlayer.QueueDeviceUpdate(device);
                initiatingPlayer.QueueTerminalMessage($"[{targetComputer.IpAddress.ToIPString()}] Finished port scan");
                
                Game.World.DeleteEntity(portscanEntity);
            }
        }
    }
}