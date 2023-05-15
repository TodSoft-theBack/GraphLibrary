namespace GraphLibrary
{
    internal class GraphLN<T> : Graph<T>, IWGraph<T> where T : notnull
    {
        protected List<List<int>> ListOfNeighbours { get; set; }
        public GraphLN() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<int>>();
        }

        public GraphLN(GraphLN<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<int>>();
        }

        public GraphLN(GraphAM<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<int>>();
        }

        public GraphLN(GraphLE<T> graph) 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, int>();
            ListOfNeighbours = new List<List<int>>();
        }

        
        public GraphLN(List<List<int>> listOfNeighbours) : this()
        {
            ListOfNeighbours = new List<List<int>>(listOfNeighbours);
        }

        public GraphLN(List<(int vertexFrom, int vertexTo)> listOfEdges) : this()
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
        public List<int> GetNeighbours(int vertex) => ListOfNeighbours[vertex];
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
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tWeighted list of neighbours:\n";
            return result;
        }
    }
}