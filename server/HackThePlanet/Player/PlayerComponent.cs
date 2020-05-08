namespace HackThePlanet
{
    using Newtonsoft.Json;
    using PrimitiveEngine;


    public class PlayerComponent : IEntityComponent
    {
        public int Id;
        public string Name;


        #region Properties
        [JsonIgnore]
        public Entity Entity
        {
            get { return Game.World.GetEntityById(this.Id); }
        }
        #endregion


        public PlayerComponent Clone()
        {
            return new PlayerComponent()
                       {
                           Id = this.Id,
                           Name = this.Name
                       };
        }
    }
}