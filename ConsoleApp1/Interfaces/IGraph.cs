namespace GraphLibrary
{
    public interface IGraph<T> : IGraphPrototype<T>, INotWeighted<T> where T : notnull
    {
        
    }
}