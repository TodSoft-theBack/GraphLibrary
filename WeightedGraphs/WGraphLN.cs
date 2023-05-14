namespace GraphLibrary
{
    internal class WGraphLN<T> : Graph<T>, IWGraph<T> where T : notnull
    {
        protected List<List<(int vertexIndex, int weigth)>> ListOfNeighbours { get; set; }
        public WGraphLN() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<(int vertexIndex, int weigth)>>();
        }

        public WGraphLN(WGraphLN<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<(int vertexIndex, int weigth)>>();
        }

        public WGraphLN(WGraphAM<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<(int vertexIndex, int weigth)>>();
        }

        public WGraphLN(WGraphLE<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<(int vertexIndex, int weigth)>>();
        }

        
        public WGraphLN(List<List<(int vertexIndex, int weigth)>> listOfNeighbours) : this()
        {
            ListOfNeighbours = new List<List<(int vertexIndex, int weigth)>>(listOfNeighbours);
        }

        public WGraphLN(List<((int vertexFrom, int vertexTo), int weight)> listOfEdges) : this()
        {

        }
        
        public void AddVertex(T vertex) => this.Add_Vertex(vertex);

        public void AddEdge(T from, T to, int weight)
        {

        }

        public void RemoveVertex(T vertex)
        {
            this.Remove_Vertex(vertex);
        }

        public void RemoveEdge(T from, T to)
        {

        }

        public bool HasVertex(T vertex) => this.Has_Vertex(vertex);

        public bool HasEdge(T from, T to)
        {
            return false;
        }
        public List<int> GetNeighbours(int vertex)
            => ListOfNeighbours[vertex].Select(neighbour => neighbour.vertexIndex).ToList();
        public ITree<T> BreadthTraverse(T? root)
        {
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            return new TreeLP<T>(root, Verteces.Count);
        }
        public ITree<T> DepthTraverse(T? root)
        {
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            return new TreeLP<T>(root, Verteces.Count);
        }
        public void ShortestDistance(T root, ref List<int> weigths, ref ITree<T> paths)
        {
            
        }

        public override string ToString()
        {
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tWeighted list of neighbours:\n";
            return result;
        }
    }
}