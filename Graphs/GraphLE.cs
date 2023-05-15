namespace GraphLibrary
{
    internal class GraphLE<T> : Graph<T>, IGraph<T> where T : notnull
    {
        public List<(int vertexFrom, int vertexTo)> ListOfEdges { get; set; }

        public GraphLE() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<(int vertexFrom, int vertexTo)>();
        }

        public GraphLE(List<(int vertexFrom, int vertexTo)> listOfEdges)
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfEdges = new List<(int vertexFrom, int vertexTo)>(listOfEdges);
        }
        
        public void AddVertex(T vertex) => this.Add_Vertex(vertex);

        public void AddEdge(T from, T to)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");

            if (!this.Has_Vertex(from))
                Add_Vertex(from);
            if (!this.Has_Vertex(to))
                Add_Vertex(to);
            ListOfEdges.Add((VertexIndeces[from], VertexIndeces[to]));
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
            int index = ListOfEdges.FindIndex(edge => edge == desiredEdge);

        }

        public bool HasVertex(T vertex) => this.Has_Vertex(vertex);

        public bool HasEdge(T from, T to)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            var desiredEdge = (VertexIndeces[from], VertexIndeces[to]);
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

            Stack<int> stack = new Stack<int>();
            stack.Push(VertexIndeces[root]);
            var previousVertex = -1;
            while (stack.Count > 0)
            {
                var currentVertex = stack.Pop();

                if (visited[currentVertex])
                    continue;
                
                var adjecentVerteces = GetNeighbours(currentVertex).Where(v => !visited[v]).ToList();
                visited[currentVertex] = true;

                foreach (var vertex in adjecentVerteces)
                    stack.Push(vertex);

                if (previousVertex != -1)
                    tree.AddVertex(Verteces[currentVertex], Verteces[previousVertex]);

                previousVertex = currentVertex;  
            }
            return tree;
        }

        public void ShortestDistance(T root, ref Dictionary<T, int> weigths, ref ITree<T> paths)
        {
            
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

