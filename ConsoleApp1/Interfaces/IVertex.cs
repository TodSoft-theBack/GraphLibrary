namespace GraphLibrary
{
    internal interface IVertex<T> where T : notnull
    {
        internal int Count { get; }
        internal List<T>? Vertices { get; }
        internal Dictionary<T, int>? VertexIndices { get; }
    }
}