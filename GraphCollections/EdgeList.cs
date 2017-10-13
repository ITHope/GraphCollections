using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    class EdgeList
    {
        public int dist = 0;
        public Vertex from;
        public Vertex to;


        public EdgeList()
        {
            dist = 0;
            from = null;
            to = null;
          
        }
        public EdgeList(int num)
        {
            dist = num;
            from = null;
            to = null;
        }

        public EdgeList(int num, Vertex from, Vertex to)
        {
            dist = num;
            this.from = from;
            this.to = to;
        }
    }
}
