﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public class Edge
    {
        public int dist;
        public Vertex from;
        public Vertex to;
        public bool isVisited;

        public Edge()
        {
            dist = 0;
            from = null;
            to = null;
            init();
        }
        public Edge(int num)
        {
            dist = num;
            from = null;
            to = null;
            init();
        }

        public Edge(int num, Vertex from, Vertex to)
        {
            dist = num;
            this.from = from;
            this.to = to;
            init();
        }

        public void init()
        {
            isVisited = false;
        }
    }
}
