namespace GraphLibrary
{
    internal class TreeLP<T> : Tree<T>, ITree<T> where T : notnull
    {
        static int ROOT_STATE  = -1;
        protected int[] ListOfParents { get; set;}
        public T Root {get; protected set;}

        public TreeLP(T root, int capacity)
        {
            Verteces = new List<T>(capacity);
            VertexIndeces = new Dictionary<T, int>(capacity);
            ListOfParents = new int[capacity];
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
            if (Has_Vertex(vertex))
                return;
            Add_Vertex(vertex);
            ListOfParents[VertexIndeces[vertex]] = VertexIndeces[parent];
        }

        private List<int> GetConnections(int vertex)
        {
            var connected = new List<int>();
            for (int i = 0; i < ListOfParents.Length; i++)
                if (i != vertex && ListOfParents[i] == vertex)
                        connected.Add(i);
            return connected;
        }

        public override string ToString()
        {
            if (VertexIndeces == null)
                throw new Exception("Verteces dictionary was null!!!");
            if (Verteces == null)
                throw new Exception("Verteces collection was null!!!");
            string result = string.Empty;
            var connected =  GetConnections(VertexIndeces[Root]);
            result += $"{Root} -> {string.Join(", ", connected.Select(conn => Verteces[conn]))}\n";
            foreach (var vertex in connected)
                result += $"{Verteces[vertex]} -> {string.Join(", ", GetConnections(vertex).Select(conn => Verteces[conn]))}\n";
            return result;
        }
    }
}