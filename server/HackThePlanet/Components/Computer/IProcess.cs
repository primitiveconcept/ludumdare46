namespace HackThePlanet
{
    using PrimitiveEngine;


    public interface IProcess : IEntityComponent
    {
        string Command { get; }
        ushort RamUse { get; }
        string Status { get; set; }
    }
}