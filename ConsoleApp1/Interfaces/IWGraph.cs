namespace GraphLibrary
{
    public interface IWGraph<T> : IGraphPrototype<T>, IWeighted<T> where T : notnull
    {

    }
}