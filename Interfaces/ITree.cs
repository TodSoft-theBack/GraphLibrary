namespace GraphLibrary
{
    public interface ITree<T>
    {
        public void AddVertex(T vertex, T parent);
    }
}