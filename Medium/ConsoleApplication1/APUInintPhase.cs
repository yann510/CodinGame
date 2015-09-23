using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player
{
    static void Main(string[] args)
    {
        int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
        Console.Error.WriteLine("Width:{0} | height:{1}", width,height);
        var nodes = new List<Node>();

        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine(); // width characters, each either 0 or .
            Console.Error.WriteLine(line);
            var charArray = line.ToCharArray();
            for (int j = 0; j < charArray.Length; j++)
            {
                var value = charArray[j] == '0';
                var node = new Node(new Point(j,i), value);
                nodes.Add(node);
                Console.Error.WriteLine("node position: {0} {1} | node value:{2}", node.position.X, node.position.Y, node.value);

            }
        }

        foreach (var node in nodes.Where(x => x.value))
        {
            //Console.Error.WriteLine("node position: {0} {1}", node.position[0], node.position[1]);
            Console.WriteLine(WriteAnswer(node.position, node.GetRightNeighbor(nodes, width), node.GetBottomNeighbor(nodes,height)));
        }
    }

    private static string WriteAnswer(Point currentNode, Point rightNode, Point bottomNode)
    {
        return currentNode.X + " " + currentNode.Y + " " + rightNode.X + " " + rightNode.Y + " " + bottomNode.X + " " + bottomNode.Y;
    }
}

public class Node
{
    public Point position;
    public bool value;

    public Node(Point position, bool value)
    {
        this.position = position;
        this.value = value;
    }

    public Point GetRightNeighbor(List<Node> nodes, int width)
    {
        var nextPosition = new Point(position.X + 1, position.Y);
        var nextNode = nodes.SingleOrDefault(x => x.position.X == nextPosition.X && x.position.Y == nextPosition.Y);

        if (nextNode != null && nextNode.value)
        {
            return nextNode.position;
        }
        else
        {
            for (int i = nextPosition.X; i < width; i++)
            { 
                nextPosition = new Point(i, nextPosition.Y);
                nextNode = nodes.SingleOrDefault(x => x.position.X == nextPosition.X && x.position.Y == nextPosition.Y);
                if (nextNode != null && nextNode.value)
                {
                    return nextPosition;
                }
            }
        }
        return new Point(-1,-1);
    }

    public Point GetBottomNeighbor(List<Node> nodes, int height)
    {
        var nextPosition = new Point(position.X, position.Y+1);
        var nextNode = nodes.SingleOrDefault(x => x.position.X == nextPosition.X && x.position.Y == nextPosition.Y);

        if (nextNode != null && nextNode.value)
        {
            return nextNode.position;
        }
        else
        {
            for (int i = nextPosition.Y; i < height; i++)
            {
                nextPosition = new Point(nextPosition.X, i);
                nextNode = nodes.SingleOrDefault(x => x.position.X == nextPosition.X && x.position.Y == nextPosition.Y);
                if (nextNode != null && nextNode.value)
                {
                    return nextPosition;
                }
            }
        }
        return new Point(-1, -1);
    }
}