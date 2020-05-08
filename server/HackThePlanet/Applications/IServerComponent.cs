namespace HackThePlanet
{
    using System.Collections.Generic;
    using Newtonsoft.Json;


    public interface IServerComponent : IApplicationComponent
    {
        #region Properties
        List<UserAccount> Accounts { get; }
        Port Port { get; }
        #endregion
    }
    
    
    public static class ServerComponent
    {
        public static readonly JsonSerializerSettings Serializer = 
            new JsonSerializerSettings()
                {
                    ContractResolver = new InterfaceContractResolver(
                        typeof(IApplicationComponent),
                        typeof(IServerComponent))
                };


        public static string ToJson(this IServerComponent serverComponent)
        {
            return JsonConvert.SerializeObject(serverComponent, Serializer);
        }
    }
}