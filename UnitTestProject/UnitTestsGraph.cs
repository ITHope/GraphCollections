using System;
using GraphCollections;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestFixture(typeof(GraphL))]
    //[TestFixture(typeof(GraphMatrix))]
    //[TestFixture(typeof(GraphEdgeList))]
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
                _graph.addVertex(i.ToString());
            }

            Assert.AreEqual(n, _graph.nodeSet.Count);
            //Assert.AreEqual(0, _graph.EdgeCount);
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
            Assert.AreEqual(4, _graph.nodeSet.Count);
            //Assert.AreEqual(0, _graph.EdgeCount);
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

            Assert.AreEqual(3, _graph.nodeSet.Count);
            //Assert.AreEqual(2, _graph.EdgeCount);
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
            Assert.AreEqual(5, _graph.nodeSet.Count);
            //Assert.AreEqual(1, _graph.EdgeCount);
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
            Assert.AreEqual(5, _graph.nodeSet.Count);
            //Assert.AreEqual(2, _graph.EdgeCount);
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
            Assert.AreEqual(5, _graph.nodeSet.Count);
            //Assert.AreEqual(2, _graph.EdgeCount);
            Assert.AreEqual(6, _graph.getEdge("0", "1"));
        }

        [Test]
        public void TestSetEdgeEx()
        {
            Assert.Throws<KeyNotFoundException>(() => _graph.setEdge("6", "7", 1));
        }
    }
}
