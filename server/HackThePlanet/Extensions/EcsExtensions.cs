namespace HackThePlanet
{
    using PrimitiveEngine;


    public static class EcsExtensions
    {
        public static Entity GetEntity<T>(this T component)
            where T: IEntityComponent
        {
            return Game.World.EntityManager.GetComponentEntity(component);
        }
		
        public static T GetSiblingComponent<T>(this IEntityComponent component)
            where T: IEntityComponent
        {
            Entity entity = Game.World.EntityManager.GetComponentEntity(component);
            return entity.GetComponent<T>();
        }
    }
}