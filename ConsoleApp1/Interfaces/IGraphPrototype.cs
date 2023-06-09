namespace GraphLibrary
{
    internal interface IGraphPrototype<T> : IVertex<T> where T : notnull
    {
        public void AddVertex(T vertex);
        public void RemoveVertex(T vertex);
        public bool HasVertex(T vertex);
        public List<int> GetNeighbours(int vertex);
        public ITree<T> BreadthTraverse(T root);
        public ITree<T> DepthTraverse(T root);
        public bool BreadthSearch(T from, T to);
        public bool DepthSearch(T from, T to);
        public void ShortestDistance(T root, ref Dictionary<T, int> weigths, ref ITree<T> paths);
    }
}