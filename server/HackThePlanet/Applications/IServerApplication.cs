namespace HackThePlanet
{
    using System.Collections.Generic;
    using Newtonsoft.Json;


    public interface IServerApplication : IApplication
    {
        #region Properties
        List<UserAccount> Accounts { get; }
        Port Port { get; }
        #endregion
    }
}