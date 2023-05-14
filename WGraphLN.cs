namespace GraphLibrary
{
    internal class WGraphLN<T> : Graph<T>, IGraph<T> where T : notnull
    {
        protected List<List<(uint vertexIndex, int weigth)>> ListOfNeighbours { get; set; }
        public WGraphLN() 
        {
            Verteces = new List<T>();
            VertexIndeces = new Dictionary<T, uint>();
            ListOfNeighbours = new List<List<(uint vertexIndex, int weigth)>>();
        }

        public WGraphLN(List<((uint vertexFrom, uint vertexTo), int weight)> listOfEdges) : this()
        {

        }
        
        public void AddVertex(T vertex) => this.Add_Vertex(vertex);

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