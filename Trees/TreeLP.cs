namespace GraphLibrary
{
    internal class TreeLP<T> : Tree<T>, ITree<T> where T : notnull
    {
        static int ROOT_STATE  = -1;
        protected List<int> ListOfParents { get; set;}
        public T Root {get; protected set;}

        public TreeLP(T root, int capacity)
        {
            Verteces = new List<T>(capacity);
            VertexIndeces = new Dictionary<T, int>(capacity);
            ListOfParents = new List<int>(capacity);
            Root = root;
            Add_Vertex(root);
            ListOfParents[VertexIndeces[Root]] = ROOT_STATE;
        }

        public void AddVertex(T vertex, T parent)
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            if (!Has_Vertex(parent))
                throw new Exception("Invalid parent index!!!");
            Add_Vertex(vertex);
            ListOfParents[VertexIndeces[vertex]] = VertexIndeces[parent];
        }

        public override string ToString()
        {
            return "";
        }
    }
}