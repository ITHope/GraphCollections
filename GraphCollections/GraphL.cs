using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public class GraphL : IGraph, IEnumerable
    {
        public List<Vertex> nodeSet;

        public GraphL() : this(null) { }

        public GraphL(List<Vertex> nodeSet)
        {
            if (nodeSet == null)
                this.nodeSet = new List<Vertex>();
            else
                this.nodeSet = nodeSet;
        }

        public void addEdge(string str1, string str2, int num)
        {
            FindByValue(str1).Neighbors.Add(str2);
            FindByValue(str1).dist.Add(new Edge(num));
        }

        public void addVertex(string str)
        {
            nodeSet.Add(new Vertex(str));
        }

        public int delEdge(string str1, string str2)
        {
            throw new NotImplementedException();
        }

        private Vertex FindByValue(string str)
        {
            Vertex res = null;
            foreach(Vertex v in nodeSet)
            {
                if(v.data == str)
                {
                    res = v;
                }
            }
            return res;
        }

        public void delVertex(string str)
        {
            // first remove the node from the nodeset
            Vertex nodeToRemove = FindByValue(str);
            if (nodeToRemove == null)
                // node wasn't found
                throw new KeyNotFoundException();

            // otherwise, the node was found
            nodeSet.Remove(nodeToRemove);

            // enumerate through each node in the nodeSet, removing edges to this node
            foreach (Vertex gnode in nodeSet)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove.data);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Neighbors.RemoveAt(index);
                    gnode.dist.RemoveAt(index);
                }
            }
        }

        public int getEdge(string str1, string str2)
        {
            throw new NotImplementedException();
        }

        public void print()
        {
            foreach(Vertex v in nodeSet)
            {
                Console.WriteLine(v.data);
                foreach (Edge e in v.dist)
                {
                    Console.WriteLine(e.dist);
                }
            }
        }

        public void setEdge(string str1, string str2, int num)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)nodeSet).GetEnumerator();
        }
    }
}
