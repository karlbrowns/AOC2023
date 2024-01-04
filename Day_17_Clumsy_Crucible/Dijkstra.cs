using System;
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

        int minDistance(Dictionary<int, int> dist, bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;

            foreach (var kvp in dist) {
                int v = kvp.Key;
                int v_dist = kvp.Value;
                if (!sptSet[v] && v_dist <= min)
                {
                    min = v_dist;
                    min_index = v;
                }
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
            dist = new Dictionary<int, int>();
            prev = new int[V];

            bool[] sptSet = new bool[V];

            dist.Add(src, 0);

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                int u = minDistance(dist, sptSet);

                sptSet[u] = true;
                if ((count % 1000) == 0) Console.Write('.');
                foreach (int v in graph[u].neighbours) 
                {

                    if (!sptSet[v])
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


        }
        public void dijkstra(List<Nodes2> graph, int src)
        {
            dist = new Dictionary<int, int>();
            prev = new int[V];

            bool[] sptSet = new bool[V];

            dist.Add(src, 0);

            // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++)
            {
                int u = minDistance(dist, sptSet);

                sptSet[u] = true;
                if ((count % 1000) == 0) Console.Write('.');
                foreach (int v in graph[u].neighbours)
                {

                    if (!sptSet[v])
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


        }
    }
}
