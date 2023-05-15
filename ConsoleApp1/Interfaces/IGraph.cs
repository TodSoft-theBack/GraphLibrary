namespace GraphLibrary
{
    internal interface IGraph<T> : IGraphPrototype<T>, INotWeighted<T> where T : notnull
    {
        
    }
}