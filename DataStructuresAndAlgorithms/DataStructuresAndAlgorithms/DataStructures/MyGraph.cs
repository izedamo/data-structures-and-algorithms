using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresAndAlgorithms.DataStructures
{
    //Graph implementation using Adjacency List representation.
    public class MyGraph
    {
        private readonly Dictionary<int, HashSet<int>> adjacencyList;

        private int totalNodes;

        public MyGraph()
        {
            adjacencyList = new Dictionary<int, HashSet<int>>();
            totalNodes = 0;
        }

        public void AddNode(int node)
        {
            if (adjacencyList.ContainsKey(node))
                throw new ArgumentException($"{node} node already exists in the graph.", nameof(node));

            adjacencyList.Add(node, new HashSet<int>());
        }

        public void AddEdge(int node1, int node2)
        {
            AddNodesIfNotExists(node1, node2);

            adjacencyList[node1].Add(node2);
            adjacencyList[node2].Add(node1);
        }

        public string GetConnections()
        {
            var connectionsStringBuilder = new StringBuilder();
            foreach(var kv in adjacencyList)
            {
                connectionsStringBuilder
                    .Append(kv.Key)
                    .Append(": ")
                    .AppendLine(string.Join(", ", kv.Value));
            }

            return connectionsStringBuilder.ToString();
        }

        public void RemoveNode(int node)
        {
            adjacencyList.Remove(node);

            foreach(var kv in adjacencyList)
            {
                kv.Value.Remove(node);
            }
        }

        private void AddNodesIfNotExists(params int[] nodes)
        {
            foreach(var node in nodes)
            {
                if (adjacencyList.ContainsKey(node))
                    continue;

                adjacencyList.Add(node, new HashSet<int>());
            }
        }
    }
}
