namespace HackThePlanet
{
    using PrimitiveEngine;

    
    [EntitySystem(UpdateType = UpdateType.FixedUpdate)]
    public class PortScanSystem : EntityComponentProcessingSystem<PortScanApplicationComponent>
    {
        private const long Delay = 20000;


        public override void Process(
            Entity portscanEntity,
            PortScanApplicationComponent portScanComponent)
        {
            BlackBoard
        }
    }
}