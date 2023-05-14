using System.Collections.Generic;

namespace GraphLibrary
{
    class GrapthLibrary
    {
        public static void Main(string[] args)
        {
            List<(uint firstVertex, uint secondVertex)> listOfEdges = new List<(uint firstVertex, uint secondVertex)>();
            Console.WriteLine("Starting graph reader...\n");

            Console.Write("Enter an edge: ");
            string? line = Console.ReadLine()?.Trim();
            while (!string.IsNullOrEmpty(line) && line != "\n")
            {
                uint[] numbers = line.Split().Select(uint.Parse).ToArray();
                listOfEdges.Add((numbers[0], numbers[1]));
                Console.Write("Enter an edge: ");
                line = Console.ReadLine()?.Trim();
            }

            IGraph<int> graph = new GraphLE<int>(listOfEdges);
            Console.WriteLine("\nClosing graph reader...");
        }
    }
}
