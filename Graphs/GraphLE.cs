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
            string result = $"{new string('-', 10)}Oriented graph{new string('-', 10)}\n\tList of edges:\n";
            foreach (var edge  in ListOfEdges)
                result+= $"({edge.vertexFrom}, {edge.vertexFrom})\n";
            return result;
        }
    }
}

