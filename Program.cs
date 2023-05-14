using System.Collections.Generic;

namespace GraphLibrary
{
    class GrapthLibrary
    {
        public static void Main(string[] args)
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
            
            Console.WriteLine("\nClosing graph reader...");
        }
    }
}
