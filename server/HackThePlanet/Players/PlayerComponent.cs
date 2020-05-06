namespace HackThePlanet
{
    using PrimitiveEngine;


    public class PlayerComponent : IEntityComponent
    {
        public string Id;
        public string Name;


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