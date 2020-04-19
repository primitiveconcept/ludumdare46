namespace HackThePlanet.Systems
{
    using PrimitiveEngine;

    
    [EntitySystem(UpdateType = UpdateType.FixedUpdate)]
    public class PortScanSystem : EntityComponentProcessingSystem<PortScanComponent, ComputerComponent>
    {
        private const int MaxPort = 65535;


        public override void Process(
            Entity entity, 
            PortScanComponent portScanComponent, 
            ComputerComponent computerComponent)
        {
            // TODO: Add a delay.
            Entity initiatingEntity = 
                Game.World.GetEntityById(portScanComponent.InitiatingEntity);
            PlayerComponent initiatingPlayer = initiatingEntity.GetComponent<PlayerComponent>();

            if (computerComponent.OpenPorts.Contains(portScanComponent.CurrentPort))
            {
                initiatingPlayer.AddTerminalMessage($"Found open port: {portScanComponent.CurrentPort}");
            }

            if (portScanComponent.CurrentPort != EnumExtensions.GetLast<Port>())
            {
                portScanComponent.CurrentPort++;
            }
            else
            {
                initiatingPlayer.AddTerminalMessage($"Finished port scan of {computerComponent.IpAddress.ToIPString()}");
                entity.RemoveComponent<PortScanComponent>();
            }
        }
    }
}