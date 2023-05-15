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
            /* Given the example graph this produces 3 -> 2, 4 ; 2 -> 1 ; 1 -> ; 4 -> ; */
            Console.WriteLine(graph.BreadthTraverse('3'));
            /* Given the example graph this produces 3 -> 4 ; 4 -> 2 ; 2 -> 1 ; 1 -> ;*/            
            Console.WriteLine(graph.DepthTraverse('3'));
            Console.WriteLine("\nClosing graph reader...");
        }
    }
}
