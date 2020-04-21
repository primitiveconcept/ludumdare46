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


        public static T2 GetSiblingComponent<T1, T2>(this T1 component)
            where T1: IEntityComponent 
            where T2: IEntityComponent
        {
            return component.GetEntity().GetComponent<T2>();
        }
    }
}