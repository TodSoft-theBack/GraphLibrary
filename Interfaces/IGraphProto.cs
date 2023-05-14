namespace GraphLibrary
{
    public interface IGraphPrototype<T>
    {
        public void AddVertex(T vertex);
        public void RemoveVertex(T vertex);
        public bool HasVertex(T vertex);
        public List<int> GetNeighbours(int vertex);
        public ITree<T> BreadthTraverse(T root);
        public ITree<T> DepthTraverse(T root);
        public void ShortestDistance(T root, ref List<int> weigths, ref ITree<T> paths);
    }
}