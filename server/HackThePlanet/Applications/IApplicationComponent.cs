namespace HackThePlanet
{
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public interface IApplicationComponent : IEntityComponent
    {
        #region Properties
        int OriginEntityId { get; set; }
        ushort RamUse { get; }
        #endregion
    }


    public static class ApplicationComponent
    {
        public static readonly JsonSerializerSettings Serializer = 
            new JsonSerializerSettings()
                {
                    ContractResolver = new InterfaceContractResolver(typeof(IApplicationComponent))
                };

        
        public static T RunOnComputer<T>(ComputerComponent computer)
            where T: class, IApplicationComponent, new()
        {
            Entity applicationEntity = Game.World.CreateEntity();
            T component = new T();
            applicationEntity.AddComponent(component);

            component.OriginEntityId = computer.GetEntity().Id;
            computer.RunningApplications.Add(computer.GetFreeProcessId(), applicationEntity.Id);
            
            return component;
        }


        public static string ToJson(this IApplicationComponent applicationComponent)
        {
            return JsonConvert.SerializeObject(applicationComponent, Serializer);
        }
    }
}