namespace GraphLibrary
{
    public interface IWGraph<T>
    {
        public void AddVertex(T vertex);
        public void AddEdge(T from, T to, int weight);
        public void RemoveVertex(T vertex);
        public void RemoveEdge(T from, T to);
        public bool HasVertex(T vertex);
        public bool HasEdge(T from, T to);
        public List<T>? GetVertices();
        public List<T>? GetNeighbors(T vertex);
    }
}