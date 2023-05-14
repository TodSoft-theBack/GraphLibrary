namespace GraphLibrary
{
    public interface IGraphPrototype<T>
    {
        public void AddVertex(T vertex);
        public void RemoveVertex(T vertex);
        public bool HasVertex(T vertex);
        public List<T>? GetVertices();
        public List<T>? GetNeighbors(T vertex);
    }
}