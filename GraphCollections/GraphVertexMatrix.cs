using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public class GraphVertexMatrix : IGraph, IEnumerable
    {
        const int size = 25;
        private Vertex[] nodeSet;
        private Edge[,] edgeSet; 

        public GraphVertexMatrix() : this(null, null) { }

        public GraphVertexMatrix(Vertex[] nodeSet, Edge [,] edgeSet)
        {
            if (nodeSet == null)
            {
                this.nodeSet = new Vertex[size];
                for (int i = 0; i < size; ++i)
                {
                    this.nodeSet[i] = null;
                }
            }
            else
            {
                this.nodeSet = nodeSet;
            }


            if (edgeSet == null)
            {
                this.edgeSet = new Edge[size,size];
                for (int i = 0; i < size; ++i)
                {
                    for (int j = 0; j < size; ++j)
                    {
                        this.edgeSet[i,j] = null;
                    }
                }
            }
            else
            {
                this.edgeSet = edgeSet;
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
                if (nodeSet[i] == null)
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

        private int FindVertexIndex(string str)
        {
            int index = -1;

            for (int i = 0; i < size; ++i)
            {
                if (nodeSet[i] != null && nodeSet[i].data == str)
                {
                    index = i;
                    break;
                }
            }

            return index;
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
            //v1.dist.Add(new Edge(num, v1, v2));
            int index1 = FindVertexIndex(str1);
            int index2 = FindVertexIndex(str2);
            edgeSet[index1, index2] = new Edge(num, v1, v2);
        }



        public void addVertex(string str)
        {
            int index = FirstNullIndex();
            if (index >= 0)
            {
                nodeSet[index] = new Vertex(str);
            }
            else
                throw new KeyNotFoundException();
        }

        public int delEdge(string str1, string str2)
        {
            //Vertex v1 = FindVertexByValue(str1);
            //Vertex v2 = FindVertexByValue(str2);

            //if (v1 == null || v2 == null)
            //    throw new KeyNotFoundException();

            int index1 = FindVertexIndex(str1);
            int index2 = FindVertexIndex(str2);

            if (index1 < 0 && index2 < 0)
                throw new KeyNotFoundException();

            if (edgeSet[index1, index2] == null)
                throw new KeyNotFoundException();

            int res = edgeSet[index1, index2].dist;
            edgeSet[index1, index2] = null;


            //List<Edge> edgesList = FindEdgesByVertices(v1, v2);

            //if (edgesList.Count == 0)
            //    throw new KeyNotFoundException();

            //int res = edgesList[0].dist;
            //v1.dist.Remove(edgesList[0]);

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

        //private List<Edge> FindEdgesByVertices(Vertex from, Vertex to)
        //{
        //    List<Edge> res = new List<Edge>();

        //    foreach (Edge edge in from.dist)
        //    {
        //        if (edge.to.Equals(to))
        //        {
        //            res.Add(edge);
        //            break;
        //        }
        //    }


        //    return res;
        //}


        public void delVertex(string str)
        {
            int index = FindVertexIndex(str);

            if (index < 0)
                throw new KeyNotFoundException();

            for (int i = 0; i < size; ++i)
            {
                this.edgeSet[i, index] = null;
                this.edgeSet[index, i] = null;
            }

            nodeSet[index] = null;
        }

        public int getEdge(string str1, string str2)
        {
            int index1 = FindVertexIndex(str1);
            int index2 = FindVertexIndex(str2);

            if (index1 < 0 && index2 < 0)
                throw new KeyNotFoundException();
            
            if (edgeSet[index1, index2] == null)
                throw new KeyNotFoundException();

            int res = edgeSet[index1, index2].dist;
                        
            return res;

        }

        public void print()
        {
            var vertexList = GetAllVertices();
            foreach (Vertex v in vertexList)
            {
                int index = FindVertexIndex(v.data);

                for (int i = 0; i < size; ++i)
                {
                    Edge edge = this.edgeSet[index, i];
                    Vertex outputVertex = nodeSet[i];

                    if (edge == null)
                        throw new KeyNotFoundException();
                    
                    if (outputVertex == null)
                        throw new KeyNotFoundException();


                    Console.WriteLine(v.data + " -> " + edge.dist + " -> " + outputVertex.data);
                }
            }
        }

        public void setEdge(string str1, string str2, int num)
        {
            int index1 = FindVertexIndex(str1);
            int index2 = FindVertexIndex(str2);

            if (index1 < 0 && index2 < 0)
                throw new KeyNotFoundException();
            
            edgeSet[index1, index2].dist = num;

        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)nodeSet).GetEnumerator();
        }

        public int getInputEdgeCount(string str)
        {
            int index = FindVertexIndex(str);
            int count = 0;

            if (index < 0)
                throw new KeyNotFoundException();

            for (int i = 0; i < size; ++i)
            {
                Edge edge = edgeSet[i, index];

                if (edge != null)
                    count++;
                
            }

            return count;
        }

        public int getOutputEdgeCount(string str)
        {
            int index = FindVertexIndex(str);
            int count = 0;

            if (index < 0)
                throw new KeyNotFoundException();

            for (int i = 0; i < size; ++i)
            {
                Edge edge = edgeSet[index, i];

                if (edge != null)
                    count++;

            }

            return count;
        }

        public List<string> getInputVertexNames(string str)
        {
            int index = FindVertexIndex(str);

            if (index < 0)
                throw new KeyNotFoundException();

            var list = new List<string>();

            for (int i = 0; i < size; ++i)
            {
                Edge edge = edgeSet[i, index];

                if (edge != null)
                    list.Add(nodeSet[i].data);

            }
            
            return list;
        }

        public List<string> getOutputVertexNames(string str)
        {
            int index = FindVertexIndex(str);

            if (index < 0)
                throw new KeyNotFoundException();

            var list = new List<string>();

            for (int i = 0; i < size; ++i)
            {
                Edge edge = edgeSet[index, i];

                if (edge != null)
                    list.Add(nodeSet[i].data);

            }

            return list;
        }

        public int MinLength(string str1, string str2)
        {
            throw new NotImplementedException();
        }
    }
}
