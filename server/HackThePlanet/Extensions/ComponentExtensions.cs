namespace HackThePlanet
{
    using PrimitiveEngine;


    public static class ComponentExtensions
    {
        public static Entity GetEntity<T>(this T component)
            where T: IEntityComponent
        {
            return Game.World.EntityManager.GetComponentEntity(component);
        }
    }
}