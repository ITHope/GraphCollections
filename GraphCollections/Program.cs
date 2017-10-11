using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            IGraph graph = new GraphL();

            graph.addVertex("querty");
            graph.addVertex("QUERTY");
            graph.addVertex("123456");

            graph.addEdge("querty", "123456", 5);
            graph.addEdge("qwerty", "QWERTY", 7);
            graph.addEdge("QWERTY", "123456", 42);

            graph.print();
        }
    }
}
