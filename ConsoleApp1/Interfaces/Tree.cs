namespace GraphLibrary
{
    internal abstract class Tree<T> where T : notnull
    {
        protected List<T>? vertices {get; set;}
        protected Dictionary<T, int>? VertexIndeces {get; set;}
        protected void Add_Vertex(T vertex)
        {
            if (VertexIndeces == null)
                throw new Exception("vertices dictionary was null!!!");
            if (VertexIndeces.ContainsKey(vertex))
                return;
            if (vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (vertex == null)
                throw new Exception("Vertex cannot be null!!!");
            vertices.Add(vertex);
            VertexIndeces.Add(vertex, vertices.Count - 1);
        }
        protected void Remove_Vertex(T vertex)
        {
            if (VertexIndeces == null)
                throw new Exception("vertices dictionary was null!!!");
            if (VertexIndeces.ContainsKey(vertex))
                return;
            if (vertices == null)
                throw new Exception("vertices collection was null!!!");
            if (vertex == null)
                throw new Exception("Vertex cannot be null!!!");
            vertices.Remove(vertex);
            VertexIndeces.Remove(vertex);
        }

        protected bool Has_Vertex(T vertex)
        {
            if (vertex == null)
                throw new Exception("Vertex cannot be null!!!");
            if (VertexIndeces == null)
                throw new Exception("vertices dictionary was empty!!!");
            return VertexIndeces.ContainsKey(vertex);
        }

        protected List<T>? Get_Vertices() => vertices;
    }
}