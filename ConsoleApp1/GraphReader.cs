namespace GraphLibrary
{
    /// <summary>
    /// An example class for testing the capabilities of the GraphLibrary
    /// </summary>
    class GrapthReader
    {

        public void RunReader()
        {
            Console.WriteLine("Starting graph reader...\n");

            Console.Write("Enter an edge: ");
            string? line = Console.ReadLine()?.Trim();
            IGraph<char> graph = new GraphLE<char>();
            /* The graph used for testing is '1' < - > '2' < - > '3' < - > '4'  and '2' < - >' 4' */
            while (!string.IsNullOrEmpty(line) && line != "\n")
            {
                char[] vertices = line.Split().Select(str => str[0]).ToArray();
                graph.AddEdge(vertices[0], vertices[1]);
                Console.Write("Enter an edge: ");
                line = Console.ReadLine()?.Trim();
            }
            char root = '3';
            /* Given the example graph this produces 3 -> 2, 4 ; 2 -> 1 ; 1 -> ; 4 -> ; */
            Console.WriteLine(graph.BreadthTraverse(root));
            /* Given the example graph this produces 3 -> 4 ; 4 -> 2 ; 2 -> 1 ; 1 -> ;*/            
            Console.WriteLine(graph.DepthTraverse(root));

            ITree<char> shortestPath = new TreeLP<char>(root, 1);
            Dictionary<char, int> distances = new Dictionary<char, int>();
            graph.ShortestDistance(root, ref distances, ref shortestPath);

            Console.WriteLine(shortestPath);

            foreach (var kvp in distances)
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");

            IGraph<char> graph1 = new GraphAM<char>(graph);
            IGraph<char> graph2 = new GraphLN<char>(graph);

            Console.WriteLine("\nClosing graph reader...");
        }
        public void RunReaderWeighted()
        {
            Console.WriteLine("Starting graph reader...\n");

            Console.Write("Enter an edge: ");
            string? line = Console.ReadLine()?.Trim();
            IWGraph<char> graph = new WGraphLE<char>();
            /* The graph used for testing is '1' < - > '2' < - > '3' < - > '4'  and '2' < - >' 4' */
            while (!string.IsNullOrEmpty(line) && line != "\n")
            {
                char[] vertices = line.Split().Select(str => str[0]).ToArray();
                graph.AddEdge(vertices[0], vertices[1], vertices[2] - '0');
                Console.Write("Enter an edge: ");
                line = Console.ReadLine()?.Trim();
            }
            char root = '3';
            /* Given the example graph this produces 3 -> 2, 4 ; 2 -> 1 ; 1 -> ; 4 -> ; */
            Console.WriteLine(graph.BreadthTraverse(root));
            /* Given the example graph this produces 3 -> 4 ; 4 -> 2 ; 2 -> 1 ; 1 -> ;*/            
            Console.WriteLine(graph.DepthTraverse(root));

            ITree<char> shortestPath = new TreeLP<char>(root, 1);
            Dictionary<char, int> distances = new Dictionary<char, int>();
            graph.ShortestDistance(root, ref distances, ref shortestPath);

            Console.WriteLine(shortestPath);

            foreach (var kvp in distances)
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");

            IWGraph<char> graph1 = new WGraphAM<char>(graph);
            IWGraph<char> graph2 = new WGraphLN<char>(graph);

            Console.WriteLine("\nClosing graph reader...");
        }
    }
}
