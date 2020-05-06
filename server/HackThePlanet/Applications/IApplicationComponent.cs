namespace HackThePlanet
{
    using PrimitiveEngine;


    public interface IApplicationComponent : IEntityComponent
    {
        ComponentReference<ComputerComponent> OriginComputer { get; set; }
        string Command { get; }
        ushort RamUse { get; }
        
    }
}