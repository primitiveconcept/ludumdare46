namespace HackThePlanet
{
    using System.Collections.Generic;
    using PrimitiveEngine;


    public static class CollectionExtensions
    {
        public static IEnumerable<T> ToComponentCollection<T>(
            this IEnumerable<ComponentReference<T>> componentReferenceCollection)
            where T: class, IEntityComponent
        {
            List<T> componentCollection = new List<T>();
            foreach (ComponentReference<T> componentReference in componentReferenceCollection)
            {
                componentCollection.Add(componentReference.Component);
            }

            return componentCollection;
        }
    }
}