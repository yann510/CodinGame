using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int nodeNumber = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int linksNumber = int.Parse(inputs[1]); // the number of links
        int gatewayNumbers = int.Parse(inputs[2]); // the number of exit gateways

        var graph = new Graph(nodeNumber);
        for (int i = 0; i < linksNumber; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int nodeNumber1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int nodeNumber2 = int.Parse(inputs[1]);

            graph.AddLink(nodeNumber1, nodeNumber2);
            Console.Error.Write(nodeNumber1 + "  " + nodeNumber2 + " | ");
        }
        
        for (int i = 0; i < gatewayNumbers; i++)
        {
            int eI = int.Parse(Console.ReadLine()); // the index of a gateway node
            graph.gatewayNumbers.Add(eI);
            Console.Error.Write(eI + " | ");
        }
        Console.Error.WriteLine("node numbers :{0} | Gateway number :{1}", graph.nodes.Count, graph.gatewayNumbers.Count);
        var path = new int[] {0, 1};
        // game loop
        while (true)
        {
            int sI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn

            var minPathLengthArray = graph.GetNodePathLengthArray(sI);
            var nearestGatewayNumber = -1;

            foreach (var gatewayNumber in graph.gatewayNumbers)
            {
                var exitNodeNumber = graph.GetNodeIndex(gatewayNumber);
                
                Console.Error.WriteLine("miPathLEngthArray : {0}", minPathLengthArray[exitNodeNumber]);
                if (minPathLengthArray[exitNodeNumber] == -1)
                    continue;

                if (nearestGatewayNumber == -1)
                {
                    nearestGatewayNumber = gatewayNumber;
                }
                else
                {
                    var minExitNodeIndex = graph.GetNodeIndex(nearestGatewayNumber);
                    if (minPathLengthArray[exitNodeNumber] < minPathLengthArray[minExitNodeIndex])
                        nearestGatewayNumber = gatewayNumber;
                }
            }
            Console.Error.WriteLine("agent Node: {0} | nearestGateway number: {1} | minPathLength : {2}", sI, nearestGatewayNumber, minPathLengthArray[graph.GetNodeIndex(nearestGatewayNumber)] + 1);
            path = graph.GetShortestPath(sI, nearestGatewayNumber, minPathLengthArray[graph.GetNodeIndex(nearestGatewayNumber)] + 1);
            Console.Error.WriteLine("Number of nodes in path:{0} | agentIndex: {1} | path[0]: {2}", path.Count(), sI, path[0]);
            graph.RemoveLink(path[0], path[1]);

            Console.WriteLine("{0} {1}", path[0], path[1]); // Example: 0 1 are the indices of the nodes you wish to sever the link between
        }
    }
}

public class Graph
{
    public Dictionary<int, Node> nodes;
    public HashSet<int> gatewayNumbers;
    public Dictionary<int, int> nodeNumberToIndex;
    public int nextNodeIndex = 0;

    public Graph(int totalNodesNumber)
    {
        nodes = new Dictionary<int, Node>(totalNodesNumber);
        gatewayNumbers = new HashSet<int>();
        nodeNumberToIndex = new Dictionary<int, int>(totalNodesNumber);
    }

    public void AddLink(int nodeNumber1, int nodeNumber2)
    {
        AddNode(nodeNumber1);
        AddNode(nodeNumber2);

        nodes[nodeNumber1].neighbors.Add(nodes[nodeNumber2]);
        nodes[nodeNumber2].neighbors.Add(nodes[nodeNumber1]);
    }

    private void AddNode(int nodeNumber)
    {
        if (!nodes.ContainsKey(nodeNumber))
        {
            nodes.Add(nodeNumber, new Node(nodeNumber));
            nodeNumberToIndex.Add(nodeNumber, nextNodeIndex++);
        }
    }

    public void RemoveLink(int nodeNumber1, int nodeNumber2)
    {
        var nodeA = nodes[nodeNumber1];
        var nodeB = nodes[nodeNumber2];

        nodeA.neighbors.Remove(nodeB);
        nodeB.neighbors.Remove(nodeA);
    }

    public int GetNodeIndex(int nodeNumber)
    {
        return nodeNumberToIndex[nodeNumber];
    }

    public int[] GetNodePathLengthArray(int startingNodeNumber)
    {
        var nodesNumber = nodes.Count;
        var result = new int[nodesNumber];

        for (int i = 0; i < nodesNumber; i++)
        {
            //-1 means the nodes hasn't been visited
            result[i] = -1;
        }
        
        var startIndex = nodeNumberToIndex[startingNodeNumber];
        result[startIndex] = 0;

        FillMinPathLengthArray(startingNodeNumber, ref result, 1);
        return result;
    }

    private void FillMinPathLengthArray(int startNodeNumber, ref int[] pathLengthArray, int currentPathLength)
    {
        var startNode = nodes[startNodeNumber];
        foreach (var node in startNode.neighbors)
        {
            var nodeIndex = nodeNumberToIndex[node.number];
            //if the node hasn't been visited
            if (pathLengthArray[nodeIndex] == -1)
            {
                pathLengthArray[nodeIndex] = currentPathLength;
                FillMinPathLengthArray(node.number, ref pathLengthArray, currentPathLength + 1);
            }
            //if new path is shorter then current one established
            else if (pathLengthArray[nodeIndex] > currentPathLength)
            {
                pathLengthArray[nodeIndex] = currentPathLength;
                FillMinPathLengthArray(node.number, ref pathLengthArray, currentPathLength + 1);
            }
        }
    }

    public int[] GetShortestPath(int startingNodeNumber, int endingNodeNumber, int maxPathLength)
    {
        var nodesInPath = new HashSet<int> {startingNodeNumber};
        var path = new List<int>(maxPathLength) {startingNodeNumber};
        var startNode = nodes[startingNodeNumber];
        Console.Error.WriteLine("Number of neighbors: {0}", startNode.neighbors.Count);
        foreach (var node in startNode.neighbors)
        {
            path.Add(node.number);
            nodesInPath.Add(node.number);

            if (node.number == endingNodeNumber)
                break;
            if (IsShortestPath(node.number, endingNodeNumber, nodesInPath, path, maxPathLength))
            {
                Console.Error.WriteLine("We found shortest path");
                break;
            }

            path.RemoveAt(path.Count - 1);
            nodesInPath.Remove(node.number);
        }
        Console.Error.WriteLine("Number of nodes in path: {0}", nodesInPath.Count);
        foreach (var node in nodesInPath)
        {
            Console.Error.Write("node number: {0} | ",node);
        }
        return nodesInPath.ToArray();
    }

    private bool IsShortestPath(int startingNodeNumber, int endingNodeNumber, HashSet<int> excludeNodes, List<int> path,
        int maxPathLength)
    {
        //we already know the max path length, we can check if this is the fastest route to this node
        if (path.Count > maxPathLength)
            return false;
        if (path.Count == maxPathLength)
            return path.Last() == endingNodeNumber;

        var startingNode = nodes[startingNodeNumber];
        foreach (var node in startingNode.neighbors)
        {
            if (excludeNodes.Contains(node.number))
                continue;

            path.Add(node.number);
            excludeNodes.Add(node.number);

            if (IsShortestPath(node.number, endingNodeNumber, excludeNodes, path, maxPathLength))
                return true;

            path.RemoveAt(path.Count - 1);
            excludeNodes.Remove(node.number);
        }

        return false;
    }

    public class Node
    {
        public int number;
        public HashSet<Node> neighbors;

        public Node(int number)
        {
            this.number = number;
            this.neighbors = new HashSet<Node>();
        }

        public int Degree()
        {
            return neighbors.Count;
        }
    }
}