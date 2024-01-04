﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace Day_17_Clumsy_Crucible
{
    internal class Dijkstra
    {
        // C# program for Dijkstra's single
        // source shortest path algorithm.
        // The program is for adjacency matrix
        // representation of the graph
        // A utility function to find the
        // vertex with minimum distance
        // value, from the set of vertices
        // not yet included in shortest
        // path tree
        public int V = 9;
        public Dictionary<int, int> dist;
        public int[] prev;
        //        int minDistance(int[] dist, bool[] sptSet)
        //        {
        //            // Initialize min value
        //            int min = int.MaxValue, min_index = -1;
        //
        //            for (int v = 0; v < V; v++)
        //                if (sptSet[v] == false && dist[v] <= min)
        //                {
        //                    min = dist[v];
        //                    min_index = v;
        //                }
        //
        //            return min_index;
        //        }

        int minDistance(Dictionary<int, int> dist, List<int> sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            foreach (int v in dist.Keys)
                if (sptSet.Contains(v) == false && dist[v]<=min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        // A utility function to print
        // the constructed distance array
        void printSolution(int[] dist)
        {
            Console.Write("Vertex \t\t Distance "
                        + "from Source\n");
            for (int i = 0; i < V; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }

        // Function that implements Dijkstra's
        // single source shortest path algorithm
        // for a graph represented using adjacency
        // matrix representation
        public void dijkstra(List<Nodes> graph, int src)
        //public void dijkstra(int[,] graph, int src)
        {
            //dist
            //    = new int[V]; // The output array. dist[i]
            // will hold the shortest
            // distance from src to i
            dist = new Dictionary<int, int>();
            prev = new int[V];

            // sptSet[i] will true if vertex
            // i is included in shortest path
            // tree or shortest distance from
            // src to i is finalized
            List<int> sptSet = new List<int>();
            //bool[] sptSet = new bool[V];

            // Initialize all distances as
            // INFINITE and stpSet[] as false
            //for (int i = 0; i < V; i++)
            //{
            //    dist[i] = int.MaxValue;
            //    //sptSet[i] = false;
            //}
            //sptSet.Add(src) = true;
            // Distance of source vertex
            // from itself is always 0
            dist.Add(src, 0);

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                // Pick the minimum distance vertex
                // from the set of vertices not yet
                // processed. u is always equal to
                // src in first iteration.
                int u = minDistance(dist, sptSet);

                // Mark the picked vertex as processed
                sptSet.Add(u);
                if ((sptSet.Count % 1000) == 0) Console.Write('.');
                // Update dist value of the adjacent
                // vertices of the picked vertex.
                foreach (int v in graph[u].neighbours) 
                {

                    // Update dist[v] only if is not in
                    // sptSet, there is an edge from u
                    // to v, and total weight of path
                    // from src to v through u is smaller
                    // than current value of dist[v]
                    if (!sptSet.Contains(v))
                    {
                        if (dist.ContainsKey(v))
                        {
                            if (dist[u] + graph[v].cost < dist[v])
                            {
                                dist[v] = dist[u] + graph[v].cost;
                                prev[v] = u;
                            }
                        }
                        else
                        {
                            dist.Add(v, dist[u] + graph[v].cost);
                            prev[v] = u;
                        }
                    }
                }
            }
            //    // Update dist value of the adjacent
            //    // vertices of the picked vertex.
            //    for (int v = 0; v < V; v++)
            //
            //        // Update dist[v] only if is not in
            //        // sptSet, there is an edge from u
            //        // to v, and total weight of path
            //        // from src to v through u is smaller
            //        // than current value of dist[v]
            //        if (!sptSet[v] && graph[u, v] != 0
            //            && dist[u] != int.MaxValue
            //            && dist[u] + graph[u, v] < dist[v])
            //        {
            //            dist[v] = dist[u] + graph[u, v];
            //            prev[v] = u;
            //        }


            //}

            // print the constructed distance array
            //printSolution(dist);
            //}

            //        // Driver's Code
            //        public static void Main()
            //        {
            //            /* Let us create the example
            //    graph discussed above */
            //            int[,] graph
            //                = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
            //                            { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
            //                            { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
            //                            { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
            //                            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
            //                            { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
            //                            { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
            //                            { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
            //                            { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
            //            GFG t = new GFG();
            //
            //            // Function call
            //            t.dijkstra(graph, 0);
            //        }
            //    }

            // This code is contributed by ChitraNayal


        }
    }
//    internal class Dijkstra
//    {
//        // C# program for Dijkstra's single
//        // source shortest path algorithm.
//        // The program is for adjacency matrix
//        // representation of the graph
//        // A utility function to find the
//        // vertex with minimum distance
//        // value, from the set of vertices
//        // not yet included in shortest
//        // path tree
//        public int V = 9;
//        public int[] dist;
//        public int[] prev;
//        int minDistance(int[] dist, bool[] sptSet)
//        {
//            // Initialize min value
//            int min = int.MaxValue, min_index = -1;
//
//            for (int v = 0; v < V; v++)
//                if (sptSet[v] == false && dist[v] <= min)
//                {
//                    min = dist[v];
//                    min_index = v;
//                }
//
//            return min_index;
//        }
//
//        // A utility function to print
//        // the constructed distance array
//        void printSolution(int[] dist)
//        {
//            Console.Write("Vertex \t\t Distance "
//                        + "from Source\n");
//            for (int i = 0; i < V; i++)
//                Console.Write(i + " \t\t " + dist[i] + "\n");
//        }
//
//        // Function that implements Dijkstra's
//        // single source shortest path algorithm
//        // for a graph represented using adjacency
//        // matrix representation
//        public void dijkstra(int[,] graph, int src)
//        {
//            dist
//                = new int[V]; // The output array. dist[i]
//                              // will hold the shortest
//                              // distance from src to i
//            prev = new int[V];
//
//            // sptSet[i] will true if vertex
//            // i is included in shortest path
//            // tree or shortest distance from
//            // src to i is finalized
//            bool[] sptSet = new bool[V];
//
//            // Initialize all distances as
//            // INFINITE and stpSet[] as false
//            for (int i = 0; i < V; i++)
//            {
//                dist[i] = int.MaxValue;
//                sptSet[i] = false;
//            }
//
//            // Distance of source vertex
//            // from itself is always 0
//            dist[src] = 0;
//
//            // Find shortest path for all vertices
//            for (int count = 0; count < V - 1; count++)
//            {
//                // Pick the minimum distance vertex
//                // from the set of vertices not yet
//                // processed. u is always equal to
//                // src in first iteration.
//                int u = minDistance(dist, sptSet);
//
//                // Mark the picked vertex as processed
//                sptSet[u] = true;
//
//                // Update dist value of the adjacent
//                // vertices of the picked vertex.
//                for (int v = 0; v < V; v++)
//
//                    // Update dist[v] only if is not in
//                    // sptSet, there is an edge from u
//                    // to v, and total weight of path
//                    // from src to v through u is smaller
//                    // than current value of dist[v]
//                    if (!sptSet[v] && graph[u, v] != 0
//                        && dist[u] != int.MaxValue
//                        && dist[u] + graph[u, v] < dist[v])
//                    {
//                        dist[v] = dist[u] + graph[u, v];
//                        prev[v] = u;
//                    }
//
//            }
//
//            // print the constructed distance array
//            //printSolution(dist);
//        }
//
//        //        // Driver's Code
//        //        public static void Main()
//        //        {
//        //            /* Let us create the example
//        //    graph discussed above */
//        //            int[,] graph
//        //                = new int[,] { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
//        //                            { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
//        //                            { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
//        //                            { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
//        //                            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
//        //                            { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
//        //                            { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
//        //                            { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
//        //                            { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
//        //            GFG t = new GFG();
//        //
//        //            // Function call
//        //            t.dijkstra(graph, 0);
//        //        }
//        //    }
//
//        // This code is contributed by ChitraNayal
//
//
//    }
}
