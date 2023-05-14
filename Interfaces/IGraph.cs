namespace GraphLibrary
{
    public interface IGraph<T>
    {
        public void AddVertex(T vertex);
        public void AddEdge(T source, T destination);
        public void RemoveVertex(T vertex);
        public void RemoveEdge(T source, T destination);
        public bool HasVertex(T vertex);
        public bool HasEdge(T source, T destination);
        public List<T>? GetVertices();
        public List<T>? GetNeighbors(T vertex);
    }
}