namespace HackThePlanet
{
    public interface IApplication
    {
        #region Properties
        ushort ProcessId { get; set; }
        int OriginEntityId { get; set; }
        ushort RamUse { get; }
        #endregion
    }
}