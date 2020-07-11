namespace PrimitiveEngine
{
    public interface IGraphNodeConnection<T>
    {
        #region Properties
        T Destination { get; }
        T Source { get; }
        #endregion


        bool CanTraverse();
    }
}