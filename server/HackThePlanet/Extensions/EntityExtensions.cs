namespace HackThePlanet
{
    using PrimitiveEngine;


    public static class EntityExtensions
    {
        public static ComputerComponent Computer(this Entity entity)
        {
            return entity.GetComponent<ComputerComponent>();
        }


        public static PlayerComponent Player(this Entity entity)
        {
            return entity.GetComponent<PlayerComponent>();
        }


        public static NetworkAccessComponent NetworkAccessComponent(this Entity entity)
        {
            return entity.GetComponent<NetworkAccessComponent>();
        }
    }
}