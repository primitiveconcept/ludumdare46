namespace HackThePlanet
{
    using System.Collections.Generic;


    public interface IServerComponent : IApplicationComponent
    {
        Port Port { get; set; }
        Dictionary<StringReference, AccessLevel> Accounts { get; set; }
    }
}