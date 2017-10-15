using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollections
{
    public class Vertex
    {
        public string data;
        public List<Edge> dist;

        public Vertex() : this(null, null) { }
        public Vertex(string data) : this(data, null) { }
        public Vertex(string data, List<Edge> edges)
        {
            this.data = data;
            if (edges == null)
            {
                this.dist = new List<Edge>();
            }
            else
            {
                this.dist = edges;
            }

        }

        public string Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public List<Edge> Dist
        {
            get
            {
                if (dist == null)
                    dist = new List<Edge>();

                return dist;
            }
        }
    }
}
