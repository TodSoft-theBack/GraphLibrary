namespace GraphLibrary
{
    internal abstract class Graph<T> where T : notnull
    {
        protected List<T>? Verteces {get; set;}
        protected Dictionary<T, uint>? VertexIndeces {get; set;}

        public void Add_Vertex(T vertex)
        {
            if (VertexIndeces != null && VertexIndeces.ContainsKey(vertex))
                return;
            Verteces?.Add(vertex);
        }
        public void Remove_Vertex(T vertex)
        {
            if (VertexIndeces != null && VertexIndeces.ContainsKey(vertex))
                return;
            Verteces?.Remove(vertex);
        }

        public bool Has_Vertex(T vertex)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was empty!!!");
            return VertexIndeces.ContainsKey(vertex);
        }

        public List<T>? Get_Vertices() => Verteces;
    }
}