using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepthFirstSearch
{
    class Program
    {
        public static bool[,] graph;
        private static int AmountOfPoints;
        public static Dictionary<int, List<int>> pointAndItsAdjasent;

        static void readInputGraph()
        {
            string line;
            int tempValue;

            using (System.IO.StreamReader file = new System.IO.StreamReader(@"D:\DepthFirstSearch\graph.txt"))
            {
                line = file.ReadLine();
                if (line != null)
                {
                    if (int.TryParse(line, out AmountOfPoints))
                    {
                        graph = new bool[AmountOfPoints, AmountOfPoints];

                        for (int i = 0; i < AmountOfPoints; ++i)
                        {
                            line = file.ReadLine();
                            if (line != null)
                            {
                                string[] numbers = line.Split(' ');

                                for (int j = 0; j < AmountOfPoints; ++j)
                                {
                                    if (int.TryParse(numbers[j], out tempValue))
                                    {
                                        graph[i, j] = (tempValue == 1) ? true: false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("The symbol should have a value between 1 and 0!");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("The file is not complete!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The first line should include the amount of points!");
                    }
                }
                else
                {
                    Console.WriteLine("The input file is empty!");
                }

                file.Close();
            }
            printInputGraph();
        }

        public static void printInputGraph()
        {
            Console.Write("Amount of points - {0}\n\n", AmountOfPoints);
            for (int i = 0; i < AmountOfPoints; ++i)
            {
                for (int j = 0; j < AmountOfPoints; ++j)
                {
                    int value = (graph[i, j]) ? 1 : 0;
                    Console.Write(value + "\t");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }

        static bool DFSSetup(int startPoint, int targetPoint)
        {
            // setup dictionary and bool array
            pointAndItsAdjasent = new Dictionary<int, List<int>>();

            bool[] visited = new bool[AmountOfPoints];

            for (int i = 0; i < AmountOfPoints; ++i)
            {
                List<int> pointsFromThisPoint = new List<int>();

                visited[i] = false;

                for(int j = 0; j < AmountOfPoints; ++j)
                {
                    if(graph[i, j])
                    {
                        pointsFromThisPoint.Add(j);
                    }
                }
                pointAndItsAdjasent.Add(i, pointsFromThisPoint);
            }

            return DFSRecursive(visited, startPoint, targetPoint);
        }
        static bool DFSRecursive(bool[] visited, int currentPoint, int targetPoint)
        {
            if (currentPoint == targetPoint)
                return true;

            if (visited[currentPoint])
                return false;

            visited[currentPoint] = true;
            Console.Write(currentPoint + " - ");

            foreach (var point in pointAndItsAdjasent[currentPoint])
            {
                if (!visited[point])
                {
                    var reached = DFSRecursive(visited, point, targetPoint);
                    if (reached)
                        return true;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            readInputGraph();

            while (true)
            {
                Console.WriteLine("Enter the point, from which the search should start:");

                string startPoint = Console.ReadLine();

                Console.WriteLine();
                int startPointInt = int.Parse(startPoint);

                Console.WriteLine("Enter the point, that we are searching:");
                var endPoint = Console.ReadLine();
                int endPointInt = int.Parse(endPoint);

                if (startPointInt < AmountOfPoints && startPointInt >= 0)
                {
                    var result = DFSSetup(startPointInt, endPointInt);

                    if (result)
                    {
                        Console.Write(endPointInt + " - ");
                        Console.WriteLine("The end poit was found!");
                    }
                    else
                    {
                        Console.WriteLine("Could not find the end point from chosen start point!");
                    }
                }
                else
                {
                    Console.WriteLine("The point is out of range!");
                }

                Console.WriteLine("\nTo stop working with program enter 0:");

                if (int.Parse(Console.ReadLine()) == 0)
                {
                    break;
                }

                Console.WriteLine();
            }
        }
    }
}
