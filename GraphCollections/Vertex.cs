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
        private List<string> neighbors;
        public List<Edge> dist;

        public Vertex() : this(null, null) { }
        public Vertex(string data) : this(data, null, null) { }
        public Vertex(string data, List<string> neighbors) : this(data, neighbors, null) { }
        public Vertex(string data, List<string> neighbors, List<Edge> edges)
        {
            this.data = data;
            if (neighbors == null)
            {
                this.neighbors = new List<string>();
            }
            else
            {
                this.neighbors = neighbors;
            }
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


        public List<string> Neighbors
        {
            get
            {
                if (neighbors == null)
                    neighbors = new List<string>();

                return neighbors;
            }
            set
            {
                neighbors = value;
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
