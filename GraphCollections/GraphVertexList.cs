using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public class GraphVertexList : IGraph, IEnumerable
    {
        public List<Vertex> nodeSet { get; set; }

        public GraphVertexList() : this(null) { }

        public GraphVertexList(List<Vertex> nodeSet)
        {
            if (nodeSet == null)
                this.nodeSet = new List<Vertex>();
            else
                this.nodeSet = nodeSet;
        }

        public int getVerticesCount()
        {
            return nodeSet.Count;
        }



        public void addEdge(string str1, string str2, int num)
        {
            Vertex v1 = FindVertexByValue(str1);
            Vertex v2 = FindVertexByValue(str2);
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
            v1.dist.Add(new Edge(num, v1, v2));

        }

        public void addVertex(string str)
        {
            nodeSet.Add(new Vertex(str));
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
            foreach(Vertex v in nodeSet)
            {
                if(v.data.Equals(str))
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

            foreach (Vertex v in nodeSet)
            {
                edgesToRemove = FindEdgesByVertices(v, nodeToRemove);
                foreach(Edge e in edgesToRemove)
                {
                    v.dist.Remove(e);
                }
            }

            nodeSet.Remove(nodeToRemove);

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
            foreach(Vertex v in nodeSet)
            {
                
                foreach (Edge edge in v.dist)
                {
                    Console.WriteLine(v.data + " -> " + edge.dist + " -> "  + edge.to.data);
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
            foreach (Vertex v in nodeSet)
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
            foreach (Vertex vertex in nodeSet)
            {
                foreach (Edge edge in vertex.dist)
                {
                    if(edge.to.data.Equals(str))
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
            foreach(Vertex v in nodeSet)
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
