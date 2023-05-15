namespace GraphLibrary
{
    internal class WGraphAM <T>: Graph<T>, IWGraph<T> where T : notnull
    {
        protected int[,]? AdjacencyMatrix { get; set; }
        public WGraphAM() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
        }

        public WGraphAM(WGraphLN<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
        }

        public WGraphAM(WGraphAM<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            if (graph.AdjacencyMatrix == null)
                throw new Exception("The adjacency matrix was null!!!");
            AdjacencyMatrix = new int[graph.AdjacencyMatrix.GetLength(0), graph.AdjacencyMatrix.GetLength(1)];
            for (int i = 0; i < graph.AdjacencyMatrix.GetLength(0); i++)
                for (int j = 0; j < graph.AdjacencyMatrix.GetLength(1); j++)
                    AdjacencyMatrix[i,j] = graph.AdjacencyMatrix[i,j];
        }

        public WGraphAM(WGraphLE<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
        }

        public WGraphAM(int[,] adjacencyMatrix)
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();

            AdjacencyMatrix = new int[adjacencyMatrix.GetLength(0), adjacencyMatrix.GetLength(1)];

            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                    AdjacencyMatrix[i,j] = adjacencyMatrix[i,j];
        }

        public WGraphAM(List<((int vertexFrom, int vertexTo), int weight)> listOfEdges)
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
        }
        
        public void AddVertex(T vertex)
        {
            this.Add_Vertex(vertex);
        }

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
        {
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            if (AdjacencyMatrix == null)
                throw new Exception("The adjacency matrix was null!!!");
            var neighbours = new List<int>();
            for (int i = 0; i < Verteces.Count; i++)
                if (AdjacencyMatrix[vertex, i] != 0)
                        neighbours.Add(i);
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
            var previousVertex = -1;
            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (!visited[currentVertex])
                {
                    var adjecentVerteces = GetNeighbours(currentVertex);

                    foreach (var vertex in adjecentVerteces)
                        queue.Enqueue(vertex);

                    foreach (var vertex in adjecentVerteces)
                        tree.AddVertex(Verteces[vertex], Verteces[currentVertex]);
                    visited[currentVertex] = true;
                }
                previousVertex = currentVertex;
            }
            return tree;
        }

        public ITree<T> DepthTraverse(T? root)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            ITree<T> tree = new TreeLP<T>(root, Verteces.Count);
            bool[] visited = new bool[Verteces.Count];
            visited[VertexIndeces[root]] = true;

            Stack<int> stack = new Stack<int>();
            foreach (var neighbour in GetNeighbours(VertexIndeces[root]))
                stack.Push(neighbour);
            
            while (stack.Count > 0)
            {
                var currentVertex = stack.Pop();
                if (!visited[currentVertex])
                {
                    var adjecentVerteces = GetNeighbours(currentVertex);

                    foreach (var vertex in adjecentVerteces)
                        stack.Push(vertex);

                    visited[currentVertex] = true;
                }
            }
            return tree;
        }

        public void ShortestDistance(T root, ref Dictionary<T, int> weigths, ref ITree<T> paths)
        {
            
        }

        public override string ToString()
        {
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tWeighted adjacency matrix:\n";
            return result;
        }
    }
}