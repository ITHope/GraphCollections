using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public class GraphVertexArray : IGraph, IEnumerable
    {
        const int size = 25;
        public Vertex[] nodeSet;

        public GraphVertexArray() : this(null) { }

        public GraphVertexArray(Vertex[] nodeSet)
        {
            if (nodeSet == null)
            {
                this.nodeSet = new Vertex[size];
                for (int i=0; i<size; ++i)
                {
                    this.nodeSet[i] = null;
                }
            }
            else
            {
                this.nodeSet = nodeSet;
            }
        }

        public int getVerticesCount()
        {
            int count = 0;
            for (int i = 0; i < size; ++i)
            {
                if (nodeSet[i] != null)
                    count++;
            }
            return count;
        }

        private int FirstNullIndex()
        {
            int index = -1;

            for (int i = 0; i < size; ++i)
            {
                if(nodeSet[i] == null)
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        private List<Vertex> GetAllVertices()
        {
            var list = new List<Vertex>();

            foreach (Vertex v in nodeSet)
            {
                if (v != null)
                {
                    list.Add(v);
                }
            }

            return list;
        }


        public void addEdge(string str1, string str2, int num)
        {
            Vertex v1 = FindVertexByValue(str1);
            Vertex v2 = FindVertexByValue(str2);
            if (v1 == null)
            {
                addVertex(str1);
            }
            if (v2 == null)
            {
                addVertex(str2);
            }
            v1.dist.Add(new Edge(num, v1, v2));
        }



        public void addVertex(string str)
        {
            int index = FirstNullIndex();
            if (index >= 0)
                nodeSet[index] = new Vertex(str);
            else
                throw new KeyNotFoundException();
        }

        public int delEdge(string str1, string str2)
        {
            Vertex v1 = FindVertexByValue(str1);
            Vertex v2 = FindVertexByValue(str2);

            if (v1 == null || v2 == null)
                throw new KeyNotFoundException();



            List<Edge> edgesList = FindEdgesByVertices(v1, v2);

            if (edgesList.Count == 0)
                throw new KeyNotFoundException();

            int res = edgesList[0].dist;
            v1.dist.Remove(edgesList[0]);

            return res;
        }

        private Vertex FindVertexByValue(string str)
        {
            Vertex res = null;
            var vertexList = GetAllVertices();
            foreach (Vertex v in vertexList)
            {
                if (v.data.Equals(str))
                    {
                        res = v;
                        break;
                    }
            }
            return res;
        }

        private List<Edge> FindEdgesByVertices(Vertex from, Vertex to)
        {
            List<Edge> res = new List<Edge>();

            foreach (Edge edge in from.dist)
            {
                if (edge.to.Equals(to))
                {
                    res.Add(edge);
                    break;
                }
            }


            return res;
        }


        public void delVertex(string str)
        {
            Vertex nodeToRemove = FindVertexByValue(str);

            if (nodeToRemove == null)
                throw new KeyNotFoundException();

            List<Edge> edgesToRemove;
            var vertexList = GetAllVertices();

            foreach (Vertex v in vertexList)
            {
                edgesToRemove = FindEdgesByVertices(v, nodeToRemove);
                foreach (Edge e in edgesToRemove)
                {
                    v.dist.Remove(e);
                }
            }

            for (int i=0; i<size; ++i)
            {
                if (nodeSet[i] == nodeToRemove)
                {
                    nodeSet[i] = null;
                    break;
                }
            }
        }

        public int getEdge(string str1, string str2)
        {
            Vertex v1 = FindVertexByValue(str1);
            Vertex v2 = FindVertexByValue(str2);

            if (v1 == null || v2 == null)
                throw new KeyNotFoundException();

            List<Edge> edgesList = FindEdgesByVertices(v1, v2);

            if (edgesList.Count == 0)
                throw new KeyNotFoundException();

            int res = edgesList[0].dist;

            return res;

        }

        public void print()
        {
            var vertexList = GetAllVertices();
            foreach (Vertex v in vertexList)
            {

                foreach (Edge edge in v.dist)
                {
                    Console.WriteLine(v.data + " -> " + edge.dist + " -> " + edge.to.data);
                }
                Console.WriteLine();

            }
        }

        public void setEdge(string str1, string str2, int num)
        {
            Vertex v1 = FindVertexByValue(str1);
            Vertex v2 = FindVertexByValue(str2);

            if (v1 == null && v2 == null)
                throw new KeyNotFoundException();

            List<Edge> edgesList = FindEdgesByVertices(v1, v2);

            if (edgesList.Count == 0)
                throw new KeyNotFoundException();

            edgesList[0].dist = num;

        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)nodeSet).GetEnumerator();
        }

        public int getInputEdgeCount(string str)
        {
            Vertex vertex = FindVertexByValue(str);

            if (vertex == null)
                throw new KeyNotFoundException();

            int count = 0;
            List<Edge> edgesToCount;
            var vertexList = GetAllVertices();
            foreach (Vertex v in vertexList)
            {
                edgesToCount = FindEdgesByVertices(v, vertex);
                count += edgesToCount.Count;
            }
            return count;
        }

        public int getOutputEdgeCount(string str)
        {
            Vertex vertex = FindVertexByValue(str);

            if (vertex == null)
                throw new KeyNotFoundException();

            int count = vertex.dist.Count;
            return count;
        }

        public List<string> getInputVertexNames(string str)
        {
            Vertex vertexCheck = FindVertexByValue(str);

            if (vertexCheck == null)
                throw new KeyNotFoundException();

            var list = new List<string>();
            var vertexList = GetAllVertices();
            foreach (Vertex vertex in vertexList)
            {
                foreach (Edge edge in vertex.dist)
                {
                    if (edge.to.data.Equals(str))
                        list.Add(edge.from.data);
                }
            }
            return list;
        }

        public List<string> getOutputVertexNames(string str)
        {
            Vertex vertex = FindVertexByValue(str);

            if (vertex == null)
                throw new KeyNotFoundException();

            var list = new List<string>();

            foreach (Edge edge in vertex.dist)
            {
                list.Add(edge.to.data);
            }

            return list;
        }

        // Get min of unvisited edges
        private Edge getMinEdge(Vertex v)
        {
            int minDist = -1;
            Edge minEdge = null;
            foreach (Edge edge in v.dist)
            {
                if (!edge.isVisited)
                {
                    if (edge.dist < minDist || minDist < 0)
                    {
                        minDist = edge.dist;
                        minEdge = edge;
                    }
                }
            }

            return minEdge;
        }

        // Get min of edges with unvisited vertices
        private Vertex getMinVertex(Vertex v)
        {
            int minDist = -1;
            Vertex minVertex = null;
            foreach (Edge edge in v.dist)
            {
                if (!edge.to.isVisited)
                {
                    if (edge.dist < minDist || minDist < 0)
                    {
                        minDist = edge.dist;
                        minVertex = edge.to;
                    }
                }
            }

            return minVertex;
        }

        private void GreedyStep(Vertex currentVertex)
        {
            Edge eMin;
            do
            {
                eMin = getMinEdge(currentVertex);
                if (eMin != null)
                {
                    if (eMin.to.length < 0 || eMin.to.length > currentVertex.length + eMin.dist)
                        eMin.to.length = currentVertex.length + eMin.dist;

                    eMin.isVisited = true;
                }
            } while (eMin != null);

            currentVertex.isVisited = true;

            Vertex vMin;
            do
            {
                vMin = getMinVertex(currentVertex);
                if (vMin != null)
                {
                    vMin.isVisited = true;
                    GreedyStep(vMin);
                }
            } while (vMin != null);
        }


        public int MinLength(string str1, string str2)
        {
            List<Vertex> filledList = GetAllVertices();
            foreach (Vertex v in filledList)
            {
                v.init();
            }

            Vertex v1 = FindVertexByValue(str1);
            Vertex v2 = FindVertexByValue(str2);

            if (v1 == null && v2 == null)
                throw new KeyNotFoundException();

            v1.length = 0;

            if (v1 == v2)
                return 0;

            v1.isVisited = true;
            GreedyStep(v1);

            return v2.length;

        }
    }
}
