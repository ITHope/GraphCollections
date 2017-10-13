using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    class GraphEdges : IGraph, IEnumerable
    {
        public Dictionary<Edge, List<Vertex>> edgesSet { get; set; }
        public List<Vertex> nodeSet { get; set; }

        public GraphEdges() : this(null) { }

        public GraphEdges(Dictionary<Edge, List<Vertex>> edgesSet)
        {
            if (edgesSet == null)
                this.edgesSet = new Dictionary<Edge, List<Vertex>>();
            else
                this.edgesSet = edgesSet;
        }

        public void addEdge(string str1, string str2, int num)
        {
            List <Vertex> list = new List<Vertex>();
            Edge newEdge = new Edge(num);
            Vertex v1 = FindByValue(str1);
            Vertex v2 = FindByValue(str2);

            if (v1 == null)
            {
                v1 = new Vertex(str1);
                nodeSet.Add(v1);
            }
            if (v2 == null)
            {
                v2 = new Vertex(str2);
                nodeSet.Add(v2);
            }

            list.Add(v1);
            list.Add(v2);
            edgesSet.Add(newEdge, list);

            v1.Neighbors.Add(str2);
            v1.dist.Add(new Edge(num));

        }

        public void addVertex(string str)
        {
            nodeSet.Add(new Vertex(str));
        }

        public int delEdge(string str1, string str2)
        {
            Vertex v1 = FindByValue(str1);
            Vertex v2 = FindByValue(str2);

            if (v1 == null && v2 == null)
                throw new KeyNotFoundException();


            int index = v1.Neighbors.IndexOf(v2.data);
            int res = v1.dist[index].dist;

            v1.Neighbors.RemoveAt(index);
            v1.dist.RemoveAt(index);
            v2.Neighbors.Remove(str1);

            return res;
        }

        private Vertex FindByValue(string str)
        {
            Vertex res = null;
            foreach (Vertex v in nodeSet)
            {
                if (v.data.Equals(str))
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
            Vertex v1 = FindByValue(str1);
            Vertex v2 = FindByValue(str2);

            if (v1 == null && v2 == null)
                throw new KeyNotFoundException();

            int index = v1.Neighbors.IndexOf(v2.data);
            int res = v1.dist[index].dist;

            return res;

        }

        public void print()
        {
            foreach (Vertex v in nodeSet)
            {
                foreach (String n in v.Neighbors)
                {
                    Console.WriteLine(v.data + " -> " + n + " = " + getEdge(v.data, n));
                }
            }
        }

        public void setEdge(string str1, string str2, int num)
        {
            Vertex v1 = FindByValue(str1);
            Vertex v2 = FindByValue(str2);

            if (v1 == null && v2 == null)
                throw new KeyNotFoundException();

            int index = v1.Neighbors.IndexOf(v2.data);
            v1.dist[index].dist = num;

        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)nodeSet).GetEnumerator();
        }
    }
}
