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

        public List<T>? GetVertices() => this.Get_Vertices();

        public List<T>? GetNeighbors(T vertex)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");

            List<T> neighbours = new List<T>();
            int vertexIndex = VertexIndeces[vertex];

            foreach (var edge in ListOfEdges)
                if (edge.vertexFrom == vertexIndex)
                    neighbours.Add(Verteces[edge.vertexTo]);
                else if (edge.vertexTo == vertexIndex)
                    neighbours.Add(Verteces[edge.vertexFrom]);

            return neighbours;
        }
    }
}

