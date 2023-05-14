namespace GraphLibrary
{
    class GrapthReader
    {
        public void RunReader()
        {
            Console.WriteLine("Starting graph reader...\n");

            Console.Write("Enter an edge: ");
            string? line = Console.ReadLine()?.Trim();
            IGraph<char> graph = new GraphLE<char>();
            while (!string.IsNullOrEmpty(line) && line != "\n")
            {
                char[] verteces = line.Split().Select(str => str[0]).ToArray();
                graph.AddEdge(verteces[0], verteces[1]);
                Console.Write("Enter an edge: ");
                line = Console.ReadLine()?.Trim();
            }
            var bfs = graph.BreadthTraverse('b');
            var dfs = graph.BreadthTraverse('c');
            Console.WriteLine("\nClosing graph reader...");
        }
    }
}
