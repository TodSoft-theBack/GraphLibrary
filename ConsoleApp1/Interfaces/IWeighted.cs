namespace GraphLibrary
{
    public interface IWeighted<T>
    {
        public void AddEdge(T from, T to, int weight);
        public void RemoveEdge(T from, T to);
        public bool HasEdge(T from, T to);
    }
}