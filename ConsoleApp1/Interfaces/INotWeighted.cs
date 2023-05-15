namespace GraphLibrary
{
    public interface INotWeighted<T>
    {
        public void AddEdge(T from, T to);
        public void RemoveEdge(T from, T to);
        public bool HasEdge(T from, T to);
    }
}