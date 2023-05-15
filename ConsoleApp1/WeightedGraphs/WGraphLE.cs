namespace GraphLibrary
{
    internal class WGraphLE<T> : Graph<T>, IWGraph<T> where T : notnull
    {
        int IVertex<T>.Count { get { return Vertices is null ? 0 : Vertices.Count;} }
        List<T>? IVertex<T>.Vertices => Vertices;
        Dictionary<T, int>? IVertex<T>.VertexIndices => VertexIndices;
        public List<((int vertexFrom, int vertexTo) vertices, int weight)> ListOfEdges { get; set; }

        public WGraphLE() 
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>();
        }


        public WGraphLE(IWGraph<T> graph) 
        {
            if (graph.VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            if (graph.Vertices == null)
                throw new Exception("Vertices collection was null!!!");
            Vertices = new List<T>(graph.Vertices);
            VertexIndices = new Dictionary<T, int>(graph.VertexIndices);
            ListOfEdges = new List<((int vertexFrom, int vertexTo) vertices, int weight)>();
            for (int u = 0; u < Vertices.Count; u++)
                for (int v = 0; v < Vertices.Count; v++)
                    if (HasEdge(Vertices[u], Vertices[v]))
                        ListOfEdges.Add(((u, v), graph.GetWeight(Vertices[u], Vertices[v])));
        }

        public WGraphLE(List<((int vertexFrom, int vertexTo), int weight)> listOfEdges)
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
            ListOfEdges = new List<((int vertexFrom, int vertexTo), int weight)>(listOfEdges);
        }
        
        public void AddVertex(T vertex) => this.Add_Vertex(vertex);

        public void AddEdge(T from, T to, int weight)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");

            if (!this.Has_Vertex(from))
                Add_Vertex(from);
            if (!this.Has_Vertex(to))
                Add_Vertex(to);
            ListOfEdges.Add(((VertexIndices[from], VertexIndices[to]), weight));
        }

        public void RemoveVertex(T vertex)
        {
            this.Remove_Vertex(vertex);
        }

        public void RemoveEdge(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            var desiredEdge = (VertexIndices[from], VertexIndices[to]);
            int index = ListOfEdges.FindIndex(edge => edge.vertices == desiredEdge);
            if (index != -1)
                ListOfEdges.RemoveAt(index);       
        }

        public int GetWeight(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            if (!HasVertex(from) || !HasVertex(to))
                throw new Exception("Source and destination must be within the graph!!!");
            var edge = ListOfEdges.Where(edge => edge.vertices == (VertexIndices[from], VertexIndices[to])).ToList();
            return edge.Count == 1 ? edge.FirstOrDefault().weight : 0;
        }

        public bool HasVertex(T vertex) => this.Has_Vertex(vertex);

        public bool HasEdge(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            var desiredEdge = (VertexIndices[from], VertexIndices[to]);
            int index = ListOfEdges.FindIndex(edge => edge.vertices == desiredEdge);
            return index != -1;
        }

        public List<int> GetNeighbours(int vertex)
        {
            var neighbours = new List<int>();
            foreach (var edge in ListOfEdges)
            {
                if (edge.vertices.vertexFrom == vertex)
                    neighbours.Add(edge.vertices.vertexTo);
                else if (edge.vertices.vertexTo == vertex)
                    neighbours.Add(edge.vertices.vertexFrom);
            }
            return neighbours;
        }

        public ITree<T> BreadthTraverse(T? root)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
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
                throw new Exception("Vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
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
                throw new Exception("Vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
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
                throw new Exception("Vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
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
                throw new Exception("Vertices collection was null!!!");

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
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
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
                    var current = ListOfEdges.Where(edge => edge.vertices == (u , v)).ToList();
                    var currentWeight = int.MaxValue;
                    
                    if (current.Count != 0)
                        currentWeight = current.FirstOrDefault().weight;
                    
                    if (visited[v])
                        continue;

                    if (currentWeight == 0 || weigths[Vertices[u]] == int.MaxValue)
                        continue;

                    if(weigths[Vertices[u]] + currentWeight < weigths[Vertices[v]])
                    {
                        paths.AddVertex(Vertices[v], Vertices[u]);
                        weigths[Vertices[v]] = weigths[Vertices[u]] + currentWeight;
                    }
                }
            }
        }

        public override string ToString()
        {
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tWeighted list of edges:\n";
            foreach (var edge  in ListOfEdges)
                result+= $"({edge.vertices.vertexFrom}, {edge.vertices.vertexFrom}) -> {edge.weight}\n";
            return result;
        }
    }
}

