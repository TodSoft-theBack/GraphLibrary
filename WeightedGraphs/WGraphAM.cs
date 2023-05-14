namespace GraphLibrary
{
    internal class WGraphAM <T>: Graph<T>, IGraph<T> where T : notnull
    {
        protected uint[,]? AdjacencyMatrix { get; set; }
        public WGraphAM() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, uint>();
        }

        public WGraphAM(List<((uint vertexFrom, uint vertexTo), int weight)> listOfEdges)
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, uint>();
        }
        
        public void AddVertex(T vertex)
        {
            this.Add_Vertex(vertex);
        }

        public void AddEdge(T source, T destination)
        {

        }

        public void RemoveVertex(T vertex)
        {
            this.Remove_Vertex(vertex);
        }

        public void RemoveEdge(T source, T destination)
        {

        }

        public bool HasVertex(T vertex) => this.Has_Vertex(vertex);

        public bool HasEdge(T source, T destination)
        {
            return false;
        }

        public List<T>? GetVertices() => this.Get_Vertices();

        public List<T>? GetNeighbors(T vertex)
        {
            return new List<T>();
        }
    }
}