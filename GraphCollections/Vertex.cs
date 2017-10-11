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
        private List<string> neighbors = null;
        public List<Edge> dist;

        public Vertex() { }
        public Vertex(string data) : this(data, null) { }
        public Vertex(string data, List<string> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
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
                if (Neighbors == null)
                    Neighbors = new List<string>();

                return Neighbors;
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
