namespace HackThePlanet.Systems
{
    using PrimitiveEngine;

    
    [EntitySystem(UpdateType = UpdateType.FixedUpdate)]
    public class PortScanSystem : EntityComponentProcessingSystem<PortScanComponent, ComputerComponent>
    {
        private const long Delay = 20000;
        
        
        public override void Process(
            Entity targetEntity, 
            PortScanComponent portScanComponent, 
            ComputerComponent computerComponent)
        {
            if (portScanComponent.elapsedTime < Delay)
            {
                portScanComponent.elapsedTime += Game.Time.ElapsedTime;
                return;
            }

            portScanComponent.elapsedTime = 0;

            Entity initiatingEntity = Game.GetEntity(portScanComponent.InitiatingEntity);
            PlayerComponent initiatingPlayer = initiatingEntity.GetComponent<PlayerComponent>();

            // Found an open port.
            if (computerComponent.OpenPorts.Contains(portScanComponent.CurrentPort))
            {
                NetworkAccessComponent networkAccessComponent = 
                    initiatingEntity.GetComponent<NetworkAccessComponent>();
                networkAccessComponent.AccessOptions[targetEntity.Id]
                    .PortAccessability[portScanComponent.CurrentPort] = AccessLevel.Known;
                
                initiatingPlayer.AddTerminalMessage($"Found open port: {portScanComponent.CurrentPort}");
            }

            // Scan next port.
            if (portScanComponent.CurrentPort != EnumExtensions.GetLast<Port>())
            {
                portScanComponent.CurrentPort = portScanComponent.CurrentPort.Next();
            }
            
            // Done port scanning.
            else
            {
                DeviceUpdateMessage.Device device = new DeviceUpdateMessage.Device();
                device.ip = computerComponent.IpAddress.ToIPString();
                device.status = "Idle";
                device.commands = initiatingEntity.GetComponent<NetworkAccessComponent>().GetAvailableCommands(targetEntity.Id);
                initiatingPlayer.MessageQueue.Add(DeviceUpdateMessage.Create(device.ip, device).ToJson());
                initiatingPlayer.AddTerminalMessage($"Finished port scan of {computerComponent.IpAddress.ToIPString()}");
                targetEntity.RemoveComponent<PortScanComponent>();
            }
        }
    }
}