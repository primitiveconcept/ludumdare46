namespace HackThePlanet
{
    using PrimitiveEngine;


    public interface IProcess : IEntityComponent
    {
        string Command { get; }
        byte RamUse { get; }
        string Status { get; set; }
    }
}