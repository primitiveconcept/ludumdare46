namespace PrimitiveEngine
{
    using System.Collections.Generic;


    public interface IGraph<T>
        where T: class
    {
        IList<IGraphNodeConnection<T>> GetConnections(T node);
    }
}