using System;
using GraphCollections;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestFixture(typeof(GraphVertexList))]
    [TestFixture(typeof(GraphVertexArray))]
    [TestFixture(typeof(GraphVertexMatrix))]
    public class ListNUnitTests<TGraph> where TGraph : IGraph, new()
    {
        IGraph _graph = new TGraph();

        [SetUp]
        public void SetUp()
        {
            _graph = new TGraph();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void TestAddVertices(int n)
        {
            for (int i = 0; i < n; i++)
            {
                _graph.addVertex(i.ToString());
            }

            Assert.AreEqual(n, _graph.getVerticesCount());
        }

        [Test]
        public void TestDelVertex()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "3", 4);
            _graph.addEdge("3", "4", 5);
            _graph.delVertex("3");
            Assert.AreEqual(4, _graph.getVerticesCount());
        }

        [Test]
        public void TestDelVertexEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.delVertex("6"));
        }

        [Test]
        public void TestAddEdge()
        {
            _graph.addVertex("-1");
            _graph.addEdge("-1", "-2", 50);

            _graph.addVertex("1");
            _graph.addEdge("-1", "1", 5);

            Assert.AreEqual(3, _graph.getVerticesCount());
        }

        [Test]
        public void TestDelEdge()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            int w = _graph.delEdge("0", "1");
            Assert.AreEqual(5, _graph.getVerticesCount());
            Assert.AreEqual(5, w);
        }

        [Test]
        public void TestDelEdgeEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.delEdge("6", "7"));
        }

        [Test]
        public void TestGetEdge()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            int w = _graph.getEdge("0", "1");
            Assert.AreEqual(5, _graph.getVerticesCount());
            Assert.AreEqual(5, w);
        }

        [Test]
        public void TestGetEdgeEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.getEdge("6", "7"));
        }

        [Test]
        public void TestSetEdge()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            _graph.setEdge("0", "1", 6);
            Assert.AreEqual(5, _graph.getVerticesCount());
            Assert.AreEqual(6, _graph.getEdge("0", "1"));
        }

        [Test]
        public void TestGetInputEdgeCount()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            _graph.setEdge("0", "1", 6);
            Assert.AreEqual(1, _graph.getInputEdgeCount("1"));
        }

        [Test]
        public void TestGetInputEdgeCountEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.getInputEdgeCount("6"));
        }

        [Test]
        public void TestGetOutputEdgeCount()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            _graph.setEdge("0", "1", 6);
            Assert.AreEqual(2, _graph.getOutputEdgeCount("0"));
        }

        [Test]
        public void TestGetOutputEdgeCountEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.getOutputEdgeCount("6"));
        }

        [Test]
        public void TestGetInputVertexNames()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            _graph.setEdge("0", "1", 6);
            List<string> list = new List<string>();
            list.Add("0");
            Assert.AreEqual(list, _graph.getInputVertexNames("2"));
        }

        [Test]
        public void TestGetInputVertexNamesEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.getInputVertexNames("6"));
        }

        [Test]
        public void TestGetOutputVertexNames()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            _graph.setEdge("0", "1", 6);
            List<string> list = new List<string>();
            list.Add("1");
            list.Add("2");
            Assert.AreEqual(list, _graph.getOutputVertexNames("0"));
        }

        [Test]
        public void TestGetOutputVertexNamesEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.getOutputVertexNames("6"));
        }


        [Test]
        public void TestMinLength()
        {
            for (int i = 0; i < 5; i++)
            {
                _graph.addVertex(i.ToString());
            }
            _graph.addEdge("0", "1", 5);
            _graph.addEdge("0", "2", 4);
            _graph.setEdge("0", "1", 6);
            
            Assert.AreEqual(6, _graph.MinLength("0", "1"));
        }

        [TestCase("Vertex1", "Vertex5", 20)]
        [TestCase("Vertex1", "Vertex6", 11)]
        [TestCase("Vertex1", "Vertex4", 20)]
        [TestCase("Vertex1", "Vertex3", 9)]
        [TestCase("Vertex1", "Vertex1", 0)]
        [TestCase("Vertex1", "Vertex2", 7)]
        public void TestMinLengthFull(string str1, string str2, int length)
        {
            for (int i = 1; i < 7; ++i)
                _graph.addVertex("Vertex" + i);

            _graph.addEdge("Vertex1", "Vertex2", 7);
            _graph.addEdge("Vertex1", "Vertex3", 9);
            _graph.addEdge("Vertex1", "Vertex6", 14);

            _graph.addEdge("Vertex2", "Vertex1", 7);
            _graph.addEdge("Vertex2", "Vertex3", 10);
            _graph.addEdge("Vertex2", "Vertex4", 15);

            _graph.addEdge("Vertex3", "Vertex1", 9);
            _graph.addEdge("Vertex3", "Vertex2", 10);
            _graph.addEdge("Vertex3", "Vertex4", 11);
            _graph.addEdge("Vertex3", "Vertex6", 2);

            _graph.addEdge("Vertex4", "Vertex2", 15);
            _graph.addEdge("Vertex4", "Vertex3", 11);
            _graph.addEdge("Vertex4", "Vertex5", 6);

            _graph.addEdge("Vertex5", "Vertex4", 6);
            _graph.addEdge("Vertex5", "Vertex6", 9);

            _graph.addEdge("Vertex6", "Vertex1", 14);
            _graph.addEdge("Vertex6", "Vertex3", 2);
            _graph.addEdge("Vertex6", "Vertex5", 9);

            Assert.AreEqual(length, _graph.MinLength(str1, str2));
        }

        [Test]
        public void TestMinLengthEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.MinLength("7", "1"));
        }
    }
}
