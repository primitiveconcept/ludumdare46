namespace HackThePlanet
{
    using System;
    using Newtonsoft.Json;


    public interface IApplication
    {
        #region Properties
        string Name { get; }
        int OriginEntityId { get; set; }
        ushort ProcessId { get; set; }
        float? Progress { get; }
        ushort RamUse { get; }
        StringReference User { get; set; }
        #endregion
    }
}