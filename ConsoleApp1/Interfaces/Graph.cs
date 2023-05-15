namespace GraphLibrary
{
    internal abstract class Graph<T>  where T : notnull
    {
        internal List<T>? Vertices;
        internal Dictionary<T, int>? VertexIndices;

        public void Add_Vertex(T vertex)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            if (VertexIndices.ContainsKey(vertex))
                return;
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
            if (vertex == null)
                throw new Exception("Vertex cannot be null!!!");
            Vertices.Add(vertex);
            VertexIndices.Add(vertex, Vertices.Count - 1);
        }
        public void Remove_Vertex(T vertex)
        {
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was null!!!");
            if (VertexIndices.ContainsKey(vertex))
                return;
            if (Vertices == null)
                throw new Exception("Vertices collection was null!!!");
            if (vertex == null)
                throw new Exception("Vertex cannot be null!!!");
            Vertices.Remove(vertex);
            VertexIndices.Remove(vertex);
        }

        public bool Has_Vertex(T vertex)
        {
            if (vertex == null)
                throw new Exception("Vertex cannot be null!!!");
            if (VertexIndices == null)
                throw new Exception("Vertices dictionary was empty!!!");
            return VertexIndices.ContainsKey(vertex);
        }

        public List<T>? Get_Vertices() => Vertices;
    }
}