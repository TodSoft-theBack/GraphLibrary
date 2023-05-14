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

        public List<T>? GetVertices() => this.Get_Vertices();

        public List<T>? GetNeighbors(T vertex)
        {
            return new List<T>();
        }
    }
}