namespace GraphLibrary
{
    internal class WGraphAM <T>: Graph<T>, IWGraph<T> where T : notnull
    {
        protected int[,]? AdjacencyMatrix { get; set; }
        public WGraphAM() 
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
        }

        public WGraphAM(WGraphLN<T> graph) 
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
        }

        public WGraphAM(WGraphAM<T> graph) 
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
            if (graph.AdjacencyMatrix == null)
                throw new Exception("The adjacency matrix was null!!!");
            AdjacencyMatrix = new int[graph.AdjacencyMatrix.GetLength(0), graph.AdjacencyMatrix.GetLength(1)];
            for (int i = 0; i < graph.AdjacencyMatrix.GetLength(0); i++)
                for (int j = 0; j < graph.AdjacencyMatrix.GetLength(1); j++)
                    AdjacencyMatrix[i,j] = graph.AdjacencyMatrix[i,j];
        }

        public WGraphAM(WGraphLE<T> graph) 
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
        }

        public WGraphAM(int[,] adjacencyMatrix)
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();

            AdjacencyMatrix = new int[adjacencyMatrix.GetLength(0), adjacencyMatrix.GetLength(1)];

            for (int i = 0; i < adjacencyMatrix.GetLength(0); i++)
                for (int j = 0; j < adjacencyMatrix.GetLength(1); j++)
                    AdjacencyMatrix[i,j] = adjacencyMatrix[i,j];
        }

        public WGraphAM(List<((int vertexFrom, int vertexTo), int weight)> listOfEdges)
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
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
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (AdjacencyMatrix == null)
                throw new Exception("The adjacency matrix was null!!!");
            var neighbours = new List<int>();
            for (int i = 0; i < Vertices.Count; i++)
                if (AdjacencyMatrix[vertex, i] != 0)
                        neighbours.Add(i);
            return neighbours;
        }

        public ITree<T> BreadthTraverse(T? root)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            ITree<T> tree = new TreeLP<T>(root, Vertices.Count);
            bool[] visited = new bool[Vertices.Count];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(VertexIndices[root]);
            var previousVertex = -1;
            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (!visited[currentVertex])
                {
                    var adjecentvertices = GetNeighbours(currentVertex);

                    foreach (var vertex in adjecentvertices)
                        queue.Enqueue(vertex);

                    foreach (var vertex in adjecentvertices)
                        tree.AddVertex(Vertices[vertex], Vertices[currentVertex]);
                    visited[currentVertex] = true;
                }
                previousVertex = currentVertex;
            }
            return tree;
        }

        public ITree<T> DepthTraverse(T? root)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (root == null)
                throw new Exception("Root cannot be null!!!");
            ITree<T> tree = new TreeLP<T>(root, Vertices.Count);
            bool[] visited = new bool[Vertices.Count];

            Stack<int> stack = new Stack<int>();
            stack.Push(VertexIndices[root]);
            var previousVertex = -1;
            while (stack.Count > 0)
            {
                var currentVertex = stack.Pop();

                if (visited[currentVertex])
                    continue;
                
                var adjecentvertices = GetNeighbours(currentVertex).Where(v => !visited[v]).ToList();
                visited[currentVertex] = true;

                foreach (var vertex in adjecentvertices)
                    stack.Push(vertex);

                if (previousVertex != -1)
                    tree.AddVertex(Vertices[currentVertex], Vertices[previousVertex]);

                previousVertex = currentVertex;  
            }
            return tree;
        }

        public bool BreadthSearch(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (!HasVertex(from) || !HasVertex(to))
                throw new Exception("Source and destination must be within the graph!!!");
            bool[] visited = new bool[Vertices.Count];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(VertexIndices[from]);
            var previousVertex = -1;
            while (queue.Count > 0)
            {
                var currentVertex = queue.Dequeue();
                if (currentVertex == VertexIndices[to])
                    return true;
                if (!visited[currentVertex])
                {
                    var adjecentvertices = GetNeighbours(currentVertex);

                    foreach (var vertex in adjecentvertices)
                        queue.Enqueue(vertex);

                    visited[currentVertex] = true;
                }
                previousVertex = currentVertex;
            }
            return false;
        }

        public bool DepthSearch(T from, T to)
        {
            if (!HasVertex(from) || !HasVertex(to))
                throw new Exception("Source and destination must be within the graph!!!");
                if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            bool[] visited = new bool[Vertices.Count];

            Stack<int> stack = new Stack<int>();
            stack.Push(VertexIndices[from]);
            while (stack.Count > 0)
            {
                var currentVertex = stack.Pop();
                if (currentVertex == VertexIndices[to])
                    return true;

                if (visited[currentVertex])
                    continue;
                
                var adjecentvertices = GetNeighbours(currentVertex).Where(v => !visited[v]).ToList();
                visited[currentVertex] = true;

                foreach (var vertex in adjecentvertices)
                    stack.Push(vertex);
            }
            return false;
        }
        
        private int minDistance(Dictionary<T, int> weights, bool[] visited)
        {
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");

            int min = int.MaxValue, min_index = -1;
            for (int v = 0; v < Vertices.Count; v++)
                if (visited[v] == false && weights[Vertices[v]] <= min) 
                {
                    min = weights[Vertices[v]];
                    min_index = v;
                }
    
            return min_index;
        }

        public void ShortestDistance(T root, ref Dictionary<T, int> weigths, ref ITree<T> paths)
        {
            if (AdjacencyMatrix == null)
                throw new Exception("The adjacency matrix was null!!!");
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            weigths = new Dictionary<T, int>();
            paths = new TreeLP<T>(root, Vertices.Count);
            bool[] visited = new bool[Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++) 
            {
                weigths[Vertices[i]] = int.MaxValue; // Ideally it must be infinity
                visited[i] = false;
            }

            weigths[root] = 0;

            for (int count = 0; count < Vertices.Count; count++) 
            {
                int u = minDistance(weigths, visited);
                
                visited[u] = true;
    
                for (int v = 0; v < Vertices.Count; v++)
                {
                    
                    if (visited[v])
                        continue;

                    if (AdjacencyMatrix[u, v] == 0 || weigths[Vertices[u]] == int.MaxValue)
                        continue;

                    if(weigths[Vertices[u]] + AdjacencyMatrix[u, v]  < weigths[Vertices[v]])
                    {
                        paths.AddVertex(Vertices[v], Vertices[u]);
                        weigths[Vertices[v]] = weigths[Vertices[u]] + AdjacencyMatrix[u, v] ;
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tWeighted adjacency matrix:\n";
            return result;
        }
    }
}