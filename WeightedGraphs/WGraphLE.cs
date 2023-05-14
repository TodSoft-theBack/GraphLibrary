namespace GraphLibrary
{
    internal class WGraphLE<T> : Graph<T>, IWGraph<T> where T : notnull
    {
        public List<((int vertexFrom, int vertexTo) verteces, int weight)> ListOfEdges { get; set; }

        public WGraphLE() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>();
        }

        public WGraphLE(WGraphLN<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>();
        }

        public WGraphLE(WGraphAM<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>();
        }

        public WGraphLE(WGraphLE<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>();
        }

        public WGraphLE(List<((int vertexFrom, int vertexTo), int weight)> listOfEdges)
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>(listOfEdges);
        }
        
        public void AddVertex(T vertex) => this.Add_Vertex(vertex);

        public void AddEdge(T from, T to, int weight)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");

            if (!this.Has_Vertex(from))
                Add_Vertex(from);
            if (!this.Has_Vertex(to))
                Add_Vertex(to);
            ListOfEdges.Add(((VertexIndeces[from], VertexIndeces[to]), weight));
        }

        public void RemoveVertex(T vertex)
        {
            this.Remove_Vertex(vertex);
        }

        public void RemoveEdge(T from, T to)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            var desiredEdge = (VertexIndeces[from], VertexIndeces[to]);
            int index = ListOfEdges.FindIndex(edge => edge.verteces == desiredEdge);

        }

        public bool HasVertex(T vertex) => this.Has_Vertex(vertex);

        public bool HasEdge(T from, T to)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            var desiredEdge = (VertexIndeces[from], VertexIndeces[to]);
            int index = ListOfEdges.FindIndex(edge => edge.verteces == desiredEdge);
            return index != -1;
        }

        public List<int> GetNeighbours(int vertex)
        {
            var neighbours = new List<int>();
            foreach (var edge in ListOfEdges)
            {
                if (edge.verteces.vertexFrom == vertex)
                    neighbours.Add(edge.verteces.vertexTo);
                else if (edge.verteces.vertexTo == vertex)
                    neighbours.Add(edge.verteces.vertexFrom);
            }
            return neighbours;
        }

        public ITree<T> BreadthTraverse(T? root)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            ITree<T> tree = new TreeLP<T>(root, Verteces.Count);
            bool[] visited = new bool[Verteces.Count];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(VertexIndeces[root]);
            int previousVertex = VertexIndeces[root];
            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (!visited[currentVertex])
                {
                    var adjecentVerteces = GetNeighbours(currentVertex);
                    foreach (var vertex in adjecentVerteces)
                        queue.Enqueue(vertex);
                    visited[currentVertex] = true;
                }
            }
            return tree;
        }
        public ITree<T> DepthTraverse(T? root)
        {
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            ITree<T> tree = new TreeLP<T>(root, Verteces.Count);

            return tree;
        }
        public void ShortestDistance(T root, ref Dictionary<T, int> weigths, ref ITree<T> paths)
        {

        }

        public override string ToString()
        {
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tWeighted list of edges:\n";
            foreach (var edge  in ListOfEdges)
                result+= $"({edge.verteces.vertexFrom}, {edge.verteces.vertexFrom}) -> {edge.weight}\n";
            return result;
        }
    }
}

