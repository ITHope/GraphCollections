using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            IGraph graph = new GraphVertexList();

            //graph.addVertex("querty");
            //graph.addVertex("QUERTY");
            //graph.addVertex("123456");

            //graph.addEdge("querty", "123456", 5);
            //graph.addEdge("qwerty", "QWERTY", 7);
            //graph.addEdge("QWERTY", "123456", 42);


            for (int i = 1; i < 7; ++i)
                graph.addVertex("Vertex" + i);

            graph.addEdge("Vertex1", "Vertex2", 7);
            graph.addEdge("Vertex1", "Vertex3", 9);
            graph.addEdge("Vertex1", "Vertex6", 14);

            graph.addEdge("Vertex2", "Vertex1", 7);
            graph.addEdge("Vertex2", "Vertex3", 10);
            graph.addEdge("Vertex2", "Vertex4", 15);

            graph.addEdge("Vertex3", "Vertex1", 9);
            graph.addEdge("Vertex3", "Vertex2", 10);
            graph.addEdge("Vertex3", "Vertex4", 11);
            graph.addEdge("Vertex3", "Vertex6", 2);

            graph.addEdge("Vertex4", "Vertex2", 15);
            graph.addEdge("Vertex4", "Vertex3", 11);
            graph.addEdge("Vertex4", "Vertex5", 6);

            graph.addEdge("Vertex5", "Vertex4", 6);
            graph.addEdge("Vertex5", "Vertex6", 9);

            graph.addEdge("Vertex6", "Vertex1", 14);
            graph.addEdge("Vertex6", "Vertex3", 2);
            graph.addEdge("Vertex6", "Vertex5", 9);

            Console.WriteLine(graph.MinLength("Vertex1", "Vertex5"));

            //graph.print();

            //graph.delEdge("qwerty", "QWERTY");

            //graph.print();

            Thread.Sleep(3000);
        }
    }
}
