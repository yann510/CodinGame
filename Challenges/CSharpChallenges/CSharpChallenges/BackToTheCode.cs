using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    public enum steps { GetToStartingPosition, GetToBiggestFreeZone, GetToFreeZone, ThirdClose, CloseSquare, CloseSquareAfterObstacle };

    public static char firstEnemyWall = '0';
    public static char firstOurWall = '0';
    public static char secondOurWall = '0';
    public static char thirdOurWall = '0';
    public static char fourthOurWall = '0';
    public static List<Node> squareCorner = new List<Node>();
    public static Node firstNode;
    public static steps currentStep = steps.GetToBiggestFreeZone;
    public static int numberOfNodesChangedByUs = 0;
    public static Node[,] previousNodes = new Node[35, 20];
    public static int currentLineLength = 0;

    static void Main(string[] args)
    {
        string[] inputs;
        int opponentCount = int.Parse(Console.ReadLine()); // Opponent count
        Stack<Node> movementStack = new Stack<Node>();
        char ourWall = '0';
        char enemyWall = '0';
        bool isTryingToCloseSquare = false;

        // game loop
        while (true)
        {
            int gameRound = int.Parse(Console.ReadLine());
            inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]); // Your x position
            int y = int.Parse(inputs[1]); // Your y position
            int backInTimeLeft = int.Parse(inputs[2]); // Remaining back in time
            Node us = new Node('0', new Point(x, y));
            List<Node> enemies = new List<Node>();
            for (int i = 1; i <= opponentCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int opponentX = int.Parse(inputs[0]); // X position of the opponent
                int opponentY = int.Parse(inputs[1]); // Y position of the opponent
                int opponentBackInTimeLeft = int.Parse(inputs[2]); // Remaining back in time of the opponent
                enemies.Add(new Node((char)i, new Point(opponentX, opponentY)));
            }
            Node[,] nodes = new Node[35, 20];
            for (int i = 0; i < 20; i++)
            {
                string line = Console.ReadLine();
                int count = line.Count();
                for (int j = 0; j < count; j++)
                {
                    nodes[j, i] = new Node(line[j], new Point(j, i));
                }
            }

            string answer = WriteBestPath(nodes, enemies, us, movementStack);
            previousNodes = nodes;
            movementStack.Push(new Node('0', new Point(x, y)));
            Console.WriteLine(answer);
        }
    }

    private static string WriteBestPath(Node[,] nodes, List<Node> enemies, Node us, Stack<Node> movementStack)
    {
        Node enemy1 = enemies.ElementAt(0);
        Node previousUs = movementStack.Count > 0 ? movementStack.Peek() : us;
        if (firstNode == null)
        {
            firstNode = us;
            squareCorner.Add(us);
        }
        string answer = "";


        if (currentStep == steps.GetToBiggestFreeZone)
        {
            if (firstOurWall == '0')
            {
                firstOurWall = GetFreeDirection(us, nodes);
            }
            if (firstOurWall != 'V')
            {
                Console.Error.WriteLine("1-{0} Starting ourwall: {1} | position : {2} {3}", currentStep, firstOurWall,
                    us.position.X, us.position.Y);

                answer = StraightLineToObstacle(us, nodes, firstOurWall);
                if (answer == "Found obstacle")
                {
                    squareCorner.Add(us);
                    currentStep = steps.GetToFreeZone;
                }
            }
            else
            {
                currentStep = steps.GetToStartingPosition;
            }
        }
        if (currentStep == steps.GetToFreeZone)
        {
            if (secondOurWall == '0')
            {
                secondOurWall = GetFreeDirection(us, nodes);
            }
            if (secondOurWall != 'V')
            {
                Console.Error.WriteLine("2-{0} secondWall: {1}", currentStep, secondOurWall);

                answer = StraightLineToObstacle(us, nodes, secondOurWall);

                if (answer == "Found obstacle")
                {
                    squareCorner.Add(us);
                    currentStep = steps.ThirdClose;
                }
            }
            else
            {
                currentStep = steps.GetToStartingPosition;
            }
        }
        if (currentStep == steps.ThirdClose)
        {
            Console.Error.WriteLine("3-{0}", currentStep);


            if (thirdOurWall == '0')
            {
                Console.Error.WriteLine("First wall: {0} | Second wall: {1}", firstOurWall, secondOurWall);
                thirdOurWall = GetThirdWall(firstOurWall, secondOurWall);
            }
            Point thirdSquarePosition = GetThirdSquarePosition(us, firstNode);

            answer = StraightLineToObstacle(us, nodes, thirdOurWall);
            Console.Error.WriteLine("Thirdclose answer: {0} thirdOurWall: {1} | thirdSquarePosition: {2} {3}", answer, thirdOurWall, thirdSquarePosition.X, thirdSquarePosition.Y);


            if (us.position == thirdSquarePosition)
            {
                squareCorner.Add(us);
                currentStep = steps.CloseSquare;
            }
            if (answer == "Found obstacle")
            {
                squareCorner.Add(us);
                currentStep = steps.CloseSquare;
            }
        }

        if (currentStep == steps.CloseSquare)
        {
            Console.Error.WriteLine("4-{0}", currentStep);

            if (fourthOurWall == '0')
            {
                fourthOurWall = GetFourthWall(firstOurWall, secondOurWall, thirdOurWall);
            }

            answer = StraightLineToObstacle(us, nodes, fourthOurWall);

            if (answer == "Found obstacle")
            {
                currentStep = steps.CloseSquareAfterObstacle;
            }


            if (us.position.X == firstNode.position.X && firstNode.position.Y == us.position.Y)
            {
                currentStep = steps.GetToBiggestFreeZone;
                resetWalls();
            }
        }
        if (currentStep == steps.CloseSquareAfterObstacle)
        {
            var wasSquareClosedPreviously = WasSquareClosedPreviousTurn(nodes);
            answer = StraightLineToObstacle(us, nodes, firstOurWall);
            if (answer == "Found obstacle" || wasSquareClosedPreviously)
            {
                currentStep = steps.GetToBiggestFreeZone;
                resetWalls();
                answer = WriteBestPath(nodes, enemies, us, movementStack);
            }
        }
        if (currentStep == steps.GetToStartingPosition)
        {
            answer = firstNode.position.X + " " + firstNode.position.Y;
            if ((Math.Abs(us.position.X - firstNode.position.X) == 1 || Math.Abs(us.position.Y - firstNode.position.Y) == 1) && !(Math.Abs(us.position.X - firstNode.position.X) == 1 && Math.Abs(us.position.Y - firstNode.position.Y) == 1))
            {
                currentStep = steps.GetToBiggestFreeZone;
                resetWalls();
            }
        }
        Console.Error.WriteLine(currentStep.ToString());
        return answer;
    }

    private static bool WasSquareClosedPreviousTurn(Node[,] nodes)
    {
        var actualNumber = 0;
        foreach (var node in nodes.Cast<Node>().Where(node => node.type == '0'))
        {
            actualNumber++;
        }
        var previousNumber = 0;
        foreach (var node in previousNodes.Cast<Node>().Where(node => node.type == '0'))
        {
            previousNumber++;
        }

        return actualNumber - previousNumber > 1;
    }

    private static Node GetClosestEmptyNode(Node us, Node[,] nodes)
    {
        for (int d = 1; d < 54; d++)
        {

            for (int i = 0; i < d + 1; i++)
            {

                int x1 = us.position.X - d + i;
                int y1 = us.position.Y - i;

                try
                {
                    if (nodes[x1, y1].type == '.')
                    {
                        return nodes[x1, y1];
                    }
                }
                catch (Exception)
                {
                    //
                }


                int x2 = us.position.X + d - i;
                int y2 = us.position.Y + i;
                try
                {
                    if (nodes[x2, y2].type == '.')
                    {
                        return nodes[x2, y2];
                    }
                }
                catch (Exception)
                {
                    //
                }
            }


            for (int i = 1; i < d; i++)
            {
                int x1 = us.position.X - i;
                int y1 = us.position.Y + d - i;
                try
                {
                    if (nodes[x1, y1].type == '.')
                    {
                        return nodes[x1, y1];
                    }
                }
                catch (Exception)
                {
                    //
                }

                int x2 = us.position.X + d - i;
                int y2 = us.position.Y - i;
                try
                {
                    if (nodes[x2, y2].type == '.')
                    {
                        return nodes[x2, y2];
                    }
                }
                catch (Exception)
                {
                    //
                }
            }
        }
        throw new Exception("CLOEST POINT NOT FOUND");
    }

    private static void resetWalls()
    {
        firstOurWall = '0';
        secondOurWall = '0';
        thirdOurWall = '0';
        fourthOurWall = '0';
    }

    private static char GetThirdWall(char firstWall, char secondWall)
    {
        if ((firstWall == 'S' && secondWall == 'E') || (firstWall == 'S' && secondWall == 'W'))
        {
            return 'N';
        }
        if ((firstWall == 'E' && secondWall == 'S') || (firstWall == 'E' && secondWall == 'N'))
        {
            return 'W';
        }
        if ((firstWall == 'W' && secondWall == 'S') || (firstWall == 'W' && secondWall == 'N'))
        {
            return 'E';
        }
        if ((firstWall == 'N' && secondWall == 'W') || (firstWall == 'N' && secondWall == 'E'))
        {
            return 'S';
        }
        throw new Exception("WTF THIRD WALL");
    }

    private static char GetFourthWall(char firstWall, char secondWall, char thirdWall)
    {
        if ((firstWall == 'S' && secondWall == 'E' && thirdWall == 'N') ||
            (firstWall == 'N' && secondWall == 'E' && thirdWall == 'S'))
        {
            return 'W';
        }
        if ((firstWall == 'E' && secondWall == 'S' && thirdWall == 'W') ||
            (firstWall == 'W' && secondWall == 'S' && thirdWall == 'E'))
        {
            return 'N';
        }
        if ((firstWall == 'S' && secondWall == 'W' && thirdWall == 'N') ||
            (firstWall == 'N' && secondWall == 'W' && thirdWall == 'S'))
        {
            return 'E';
        }
        if ((firstWall == 'W' && secondWall == 'N' && thirdWall == 'E') ||
            (firstWall == 'E' && secondWall == 'N' && thirdWall == 'W'))
        {
            return 'S';
        }
        throw new Exception("WTF FOURTH WALL");
    }

    private static Point GetThirdSquarePosition(Node us, Node firstNode)
    {
        Point thirdSquarePosition;
        if (((us.position.X >= firstNode.position.X && us.position.Y >= firstNode.position.Y) && thirdOurWall == 'N') || ((us.position.X < firstNode.position.X && us.position.Y < firstNode.position.Y) && thirdOurWall == 'S') ||
            ((us.position.X < firstNode.position.X && us.position.Y > firstNode.position.Y) && thirdOurWall == 'N') || ((us.position.X > firstNode.position.X && us.position.Y < firstNode.position.Y) && thirdOurWall == 'S'))
        {
            thirdSquarePosition = new Point(us.position.X, firstNode.position.Y);
        }
        else
        {
            thirdSquarePosition = new Point(firstNode.position.X, us.position.Y);
        }

        return thirdSquarePosition;
    }

    private static char GetFreeDirection(Node us, Node[,] nodes)
    {
        //north
        var arrayWall = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            arrayWall.Add(0);
        }
        for (int i = us.position.Y - 1; i > 0; i--)
        {
            if (nodes[us.position.X, i].type == '.')
            {
                arrayWall[0]++;
            }
            else
            {
                break;
            }
        }
        //south
        for (int i = us.position.Y + 1; i < 20; i++)
        {
            if (nodes[us.position.X, i].type == '.')
            {
                arrayWall[1]++;
            }
            else
            {
                break;
            }
        }

        //east
        for (int i = us.position.X + 1; i < 35; i++)
        {
            if (nodes[i, us.position.Y].type == '.')
            {
                arrayWall[2]++;
            }
            else
            {
                break;
            }
        }

        //west
        for (int i = us.position.X - 1; i > 0; i--)
        {
            if (nodes[i, us.position.Y].type == '.')
            {
                arrayWall[3]++;
            }
            else
            {
                break;
            }
        }
        var max = arrayWall.Max();
        if (max == 0)
        {
            firstNode = GetClosestEmptyNode(us, nodes);
            Console.Error.WriteLine("Cloesest node: {0} {1}", firstNode.position.X, firstNode.position.Y);
            return 'V';
        }
        Console.Error.WriteLine("Max number" + max);
        switch (arrayWall.IndexOf(max))
        {
            case 0:
                return 'N';
            case 1:
                return 'S';
            case 2:
                return 'E';
            case 3:
                return 'W';
            default:
                throw new Exception("NOPE");
        }
    }

    private static string StraightLineToObstacle(Node us, Node[,] nodes, char targetWall)
    {
        try
        {
            switch (targetWall)
            {
                case 'E':
                    if (nodes[us.position.X + 1, us.position.Y].type == '.' ||
                        nodes[us.position.X + 1, us.position.Y].type == '0')
                    {
                        return GetToWall(targetWall, us);
                    }
                    else
                    {
                        return "Found obstacle";
                    }
                case 'W':
                    if (nodes[us.position.X - 1, us.position.Y].type == '.' ||
                        nodes[us.position.X - 1, us.position.Y].type == '0')
                    {
                        return GetToWall(targetWall, us);
                    }
                    else
                    {
                        return "Found obstacle";
                    }
                case 'N':
                    if (nodes[us.position.X, us.position.Y - 1].type == '.' ||
                        nodes[us.position.X, us.position.Y - 1].type == '0')
                    {
                        return GetToWall(targetWall, us);
                    }
                    else
                    {
                        return "Found obstacle";
                    }
                case 'S':
                    if (nodes[us.position.X, us.position.Y + 1].type == '.' ||
                        nodes[us.position.X, us.position.Y + 1].type == '0')
                    {
                        return GetToWall(targetWall, us);
                    }
                    else
                    {
                        return "Found obstacle";
                    }
                default:
                    throw new Exception("logic error in straightLine");
            }
        }
        catch (IndexOutOfRangeException)
        {
            return "Found obstacle";
        }
    }

    private static string TrackEnemy(Node enemy, Node us)
    {
        bool enemyUp = us.position.Y < enemy.position.Y;
        bool enemyRight = us.position.X < enemy.position.X;
        if (enemyUp && enemyRight)
        {
            return (enemy.position.X - 1) + " " + (enemy.position.Y - 1);
        }
        else if (enemyUp && !enemyRight)
        {
            return (enemy.position.X + 1) + " " + (enemy.position.Y - 1);
        }
        else if (!enemyUp && enemyRight)
        {
            return (enemy.position.X - 1) + " " + (enemy.position.Y + 1);
        }
        else
        {
            return (enemy.position.X + 1) + " " + (enemy.position.Y + 1);
        }
    }



    private static bool IsCloseToWall(Node us, bool rightByWall)
    {
        if (rightByWall)
        {
            return us.position.X == 0 || us.position.X == 34 || us.position.Y == 0 || us.position.Y == 19;
        }
        else
        {
            return us.position.X == 1 || us.position.X == 33 || us.position.Y == 1 || us.position.Y == 18;
        }
    }

    /// <summary>
    /// returns the ourWall when we are right by it
    /// </summary>
    /// <param name="us"></param>
    /// <returns></returns>
    private static char GetWall(Node us, Node previousUs)
    {
        if ((us.position.X == 0 && us.position.Y == 0) || (us.position.X == 34 && us.position.Y == 0) || (us.position.X == 0 && us.position.Y == 19) || (us.position.X == 34 && us.position.Y == 19))
        {
            us = previousUs;
        }

        if (us.position.X == 0)
        {
            return 'W';
        }
        else if (us.position.X == 34)
        {
            return 'E';
        }
        else if (us.position.Y == 0)
        {
            return 'N';
        }
        else if (us.position.Y == 19)
        {
            return 'S';
        }
        else
        {
            throw new Exception("NOPE GET WALL DOESNT WORK");
        }
    }

    private static string GetToWall(char wall, Node us)
    {
        switch (wall)
        {
            case 'N':
                return us.position.X + " " + 0;
            case 'S':
                return us.position.X + " " + 19;
            case 'W':
                return 0 + " " + us.position.Y;
            case 'E':
                return 34 + " " + us.position.Y;
            default:
                return "ERROR";
        }
    }

    private static char GetCloestWall(Node node)
    {
        var distance = 500;
        var wall = '0';

        if (Math.Abs(node.position.X - 0) <= distance)
        {
            distance = Math.Abs(node.position.X - 0);
            wall = 'W';
        }
        if (Math.Abs(node.position.X - 34) <= distance)
        {
            distance = Math.Abs(node.position.X - 34);
            wall = 'E';
        }
        if (Math.Abs(node.position.Y - 0) <= distance)
        {
            distance = Math.Abs(node.position.Y - 0);
            wall = 'N';
        }
        if (Math.Abs(node.position.Y - 19) <= distance)
        {
            distance = Math.Abs(node.position.Y - 19);
            wall = 'S';
        }
        Console.Error.WriteLine("CALCULUS: PositionX:{0} | PositionY:{1} Wall distance:{2}", node.position.X, node.position.Y, distance);
        return wall;
    }

    private static bool IsCloseToEnemy(Node enemy, Node us)
    {
        return (Math.Abs(us.position.X - enemy.position.X) == 1 && Math.Abs(us.position.Y - enemy.position.Y) == 0) || (Math.Abs(us.position.X - enemy.position.X) == 0 && Math.Abs(us.position.Y - enemy.position.Y) == 1);
    }

    private static string GetToClosestWall(Node us)
    {
        if (us.position.X == 1)
        {
            return 0 + " " + us.position.Y;
        }
        else if (us.position.X == 33)
        {
            return 34 + " " + us.position.Y;
        }
        else if (us.position.Y == 1)
        {
            return us.position.X + " " + 0;
        }
        else
        {
            return us.position.X + " " + 19;
        }
    }
}

public class Node
{
    public char type { get; set; }
    public Point position { get; set; }

    public Node(char type, Point position)
    {
        this.type = type;
        this.position = position;
    }
}