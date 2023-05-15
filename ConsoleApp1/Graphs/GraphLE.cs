namespace GraphLibrary
{
    internal class GraphLE<T> : Graph<T>, IGraph<T> where T : notnull
    {
        public List<(int vertexFrom, int vertexTo)> ListOfEdges { get; set; }

        public GraphLE() 
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
            ListOfEdges = new List<(int vertexFrom, int vertexTo)>();
        }

        public GraphLE(List<(int vertexFrom, int vertexTo)> listOfEdges)
        {
            Vertices = new List<T>();
            VertexIndices = new Dictionary<T, int>();
            ListOfEdges = new List<(int vertexFrom, int vertexTo)>(listOfEdges);
        }
        
        public void AddVertex(T vertex) => this.Add_Vertex(vertex);

        public void AddEdge(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");

            if (!this.Has_Vertex(from))
                Add_Vertex(from);
            if (!this.Has_Vertex(to))
                Add_Vertex(to);
            ListOfEdges.Add((VertexIndices[from], VertexIndices[to]));
        }

        public void RemoveVertex(T vertex)
        {
            this.Remove_Vertex(vertex);
        }

        public void RemoveEdge(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            var desiredEdge = (VertexIndices[from], VertexIndices[to]);
            int index = ListOfEdges.FindIndex(edge => edge == desiredEdge);

        }

        public bool HasVertex(T vertex) => this.Has_Vertex(vertex);

        public bool HasEdge(T from, T to)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            var desiredEdge = (VertexIndices[from], VertexIndices[to]);
            int index = ListOfEdges.FindIndex(edge => edge == desiredEdge);
            return index != -1;
        }

        public List<int> GetNeighbours(int vertex)
        {
            var neighbours = new List<int>();
            foreach (var edge in ListOfEdges)
            {
                if (edge.vertexFrom == vertex)
                    neighbours.Add(edge.vertexTo);
            }
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

        public void ShortestDistance(T root, ref Dictionary<T, int> weigths, ref ITree<T> paths)
        {
            if (VertexIndices == null)
                throw new Exception("vertices dictionary was null!!!");
            if (Vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (root == null || !HasVertex(root))
                throw new Exception("Root must be within the graph!!!");
            bool[] visited = new bool[Vertices.Count];
            paths = new TreeLP<T>(root, Vertices.Count);
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

                    visited[currentVertex] = true;
                }
                previousVertex = currentVertex;
            }
        }
        
        public override string ToString()
        {
            string result = $"{new string('-', 10)}Oriented graph{new string('-', 10)}\n\tList of edges:\n";
            foreach (var edge  in ListOfEdges)
                result+= $"({edge.vertexFrom}, {edge.vertexFrom})\n";
            return result;
        }
    }
}

