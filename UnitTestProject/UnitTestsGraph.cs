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
    }
}
