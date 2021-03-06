﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public interface IGraph
    {
        void addVertex(string str);
        void addEdge(string str1, string str2, int num);
        int delEdge(string str1, string str2);
        void delVertex(string str);
        void print();
        int getEdge(string str1, string str2);
        void setEdge(string str1, string str2, int num);

        int getInputEdgeCount(string str);
        int getOutputEdgeCount(string str);

        List<string> getInputVertexNames(string str);
        List<string> getOutputVertexNames(string str);

        int getVerticesCount();

        int MinLength(string str1, string str2);
    }
}
