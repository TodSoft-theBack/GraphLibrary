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

        public List<T>? GetVertices() => this.Get_Vertices();

        public List<T>? GetNeighbors(T vertex)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");

            var listOfEdges = ListOfEdges.Select(edge => edge.verteces).ToList();
            List<T> neighbours = new List<T>();
            int vertexIndex = VertexIndeces[vertex];

            foreach (var edge in listOfEdges)
                if (edge.vertexFrom == vertexIndex)
                    neighbours.Add(Verteces[edge.vertexTo]);
                else if (edge.vertexTo == vertexIndex)
                    neighbours.Add(Verteces[edge.vertexFrom]);

            return neighbours;
        }

        public override string ToString()
        {
            string result = $"{new string('-', 10)}Weighted oriented graph{new string('-', 10)}\n\tList of edges:\n";
            foreach (var edge  in ListOfEdges)
                result+= $"({}, {}) -> {}\n";
            return result;
        }
    }
}

