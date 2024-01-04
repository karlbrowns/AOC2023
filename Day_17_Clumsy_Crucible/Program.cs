using System.Text.RegularExpressions;
using System.Diagnostics;
using Day_17_Clumsy_Crucible;

List<Nodes2> nodes = new List<Nodes2>();
List<List<int>> map = new List<List<int>>();
List<List<Visited>> visited_map = new List<List<Visited>>();
List<List<Visited2>> visited_map_p2 = new List<List<Visited2>>();

int[,] table = new int[9,4];
long[] table_p2 = new long[11];

//void build_graph(int current_node)
//{
//    table[1,0] = 1;
//    table[1,1] = 16;
//    table[1,2] = 256;
//    table[2,0] = 2;
//    table[2,1] = 32;
//    table[2,2] = 512;
//    table[4,0] = 4;
//    table[4,1] = 64;
//    table[4,2] = 1024;
//    table[8,0] = 8;
//    table[8,1] = 128;
//    table[8,2] = 2048;
//    current_node = 0;
//    // initial node
//    Nodes temp = new Nodes();
//    temp.x = nodes[current_node].x + 1;
//    temp.y = nodes[current_node].y;
//    temp.dirn = 1;
//    temp.dirsteps = 1;
//    temp.cost = map[temp.y][temp.x];
//    temp.neighbours = new List<int>();
//    nodes.Add(temp);
//    nodes[current_node].neighbours.Add(nodes.Count - 1);
//    temp = new Nodes();
//    temp.x = nodes[current_node].x;
//    temp.y = nodes[current_node].y + 1;
//    temp.dirn = 4;
//    temp.dirsteps = 1;
//    temp.cost = map[temp.y][temp.x];
//    temp.neighbours = new List<int>();
//    nodes.Add(temp);
//    nodes[current_node].neighbours.Add(nodes.Count - 1);
//    bool active_nodes = true;
//    while (active_nodes) 
//    {
//        active_nodes = false;
//        int limit = nodes[current_node].neighbours.Count;
//        for (int thisnode = 0; thisnode < limit ; thisnode++)
//        //foreach(int node in nodes[current_node].neighbours) 
//        {
//            int node = nodes[current_node].neighbours[thisnode];
//            if ((visited_map[nodes[node].y][nodes[node].x].dir_and_steps>0) && 
//                ((visited_map[nodes[node].y][nodes[node].x].dir_and_steps & (/*nodes[node].dirn */ table[nodes[node].dirn, nodes[node].dirsteps-1]))== 
//                    /*nodes[node].dirn */ table[nodes[node].dirn,nodes[node].dirsteps - 1]))
//            {
//                foreach (Neighbours ntemp in visited_map[nodes[node].y][nodes[node].x].neighbours)
//                {
//                    if ((ntemp.dir_and_steps & table[nodes[node].dirn, nodes[node].dirsteps-1]) == table[nodes[node].dirn, nodes[node].dirsteps - 1]) 
//                        nodes[current_node].neighbours.Add(ntemp.node);
//                }
//                //nodes[node].dirn = 0;
//                //nodes[current_node].neighbours.RemoveAt(thisnode);
//                continue;
//            }
            
//            active_nodes = true;
//            visited_map[nodes[node].y][nodes[node].x].dir_and_steps += /*nodes[node].dirn */ table[nodes[node].dirn,nodes[node].dirsteps - 1];
//            Neighbours tempn = new Neighbours();
//            tempn.dir_and_steps = table[nodes[node].dirn, nodes[node].dirsteps - 1];
//            tempn.node = node;
//            visited_map[nodes[node].y][nodes[node].x].neighbours.Add(tempn);
//            List<int> dirns = new List<int>();
//            dirns.Add(1); dirns.Add(2); dirns.Add(4); dirns.Add(8);
//            switch (nodes[node].dirn)
//            {
//                case 1: dirns.Remove(2); break;
//                case 2: dirns.Remove(1); break;
//                case 4: dirns.Remove(8); break;
//                case 8: dirns.Remove(4); break;
//                case 0: dirns = new List<int>(); break;
//            }
//            if (nodes[node].dirsteps == 3) dirns.Remove(nodes[node].dirn);
//            foreach (int dirn in dirns)
//            {
//                temp = new Nodes();
//                bool added = false;
//                switch (dirn)
//                {
//                    case 1: // going right
//                        temp.x = nodes[node].x + 1;
//                        temp.y = nodes[node].y;
//                        if (temp.x >= map[0].Count) break;
//                        temp.dirn = dirn;
//                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
//                        else temp.dirsteps = nodes[node].dirsteps + 1;
//                        if (((visited_map[temp.y][temp.x].dir_and_steps & (table[temp.dirn, temp.dirsteps - 1])) == table[temp.dirn, temp.dirsteps - 1]))
//                        {
//                            foreach (Neighbours ntemp in visited_map[temp.y][temp.x].neighbours)
//                            {
//                                if ((ntemp.dir_and_steps & table[temp.dirn, temp.dirsteps-1]) == table[temp.dirn, temp.dirsteps - 1]) 
//                                    nodes[node].neighbours.Add(ntemp.node);
//                            }
                            
//                            break;
//                        }
//                        temp.cost = map[temp.y][temp.x];
//                        temp.neighbours = new List<int>();
//                        nodes.Add(temp);
//                        //Console.WriteLine((nodes.Count - 1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
//                        nodes[node].neighbours.Add(nodes.Count - 1);
//                        added = true;
//                        break;
//                    case 2: // going left
//                        temp.x = nodes[node].x - 1;
//                        temp.y = nodes[node].y;
//                        if (temp.x < 0) break;
//                        temp.dirn = dirn;
//                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
//                        else temp.dirsteps = nodes[node].dirsteps + 1;
//                        if (((visited_map[temp.y][temp.x].dir_and_steps & (table[temp.dirn, temp.dirsteps - 1])) == table[temp.dirn, temp.dirsteps - 1]))
//                        {
//                            foreach (Neighbours ntemp in visited_map[temp.y][temp.x].neighbours)
//                            {
//                                if ((ntemp.dir_and_steps & table[temp.dirn, temp.dirsteps - 1]) == table[temp.dirn, temp.dirsteps - 1])
//                                    nodes[node].neighbours.Add(ntemp.node);
//                            }

//                            break;
//                        }
//                        temp.cost = map[temp.y][temp.x];
//                        temp.neighbours = new List<int>();
//                        nodes.Add(temp);
//                        //Console.WriteLine((nodes.Count - 1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
//                        nodes[node].neighbours.Add(nodes.Count - 1);
//                        added = true;
//                        break;
//                    case 4: // going down
//                        temp.x = nodes[node].x;
//                        temp.y = nodes[node].y + 1;
//                        if (temp.y >= map.Count) break;
//                        temp.dirn = dirn;
//                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
//                        else temp.dirsteps = nodes[node].dirsteps + 1;
//                        if (((visited_map[temp.y][temp.x].dir_and_steps & (table[temp.dirn, temp.dirsteps - 1])) == table[temp.dirn, temp.dirsteps - 1]))
//                        {
//                            foreach (Neighbours ntemp in visited_map[temp.y][temp.x].neighbours)
//                            {
//                                if ((ntemp.dir_and_steps & table[temp.dirn, temp.dirsteps - 1]) == table[temp.dirn, temp.dirsteps - 1])
//                                    nodes[node].neighbours.Add(ntemp.node);
//                            }

//                            break;
//                        }
//                        temp.cost = map[temp.y][temp.x];
//                        temp.neighbours = new List<int>();
//                        nodes.Add(temp);
//                        //Console.WriteLine((nodes.Count - 1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
//                        nodes[node].neighbours.Add(nodes.Count - 1);
//                        added = true;
//                        break;
//                    case 8: // going up
//                        temp.x = nodes[node].x;
//                        temp.y = nodes[node].y - 1;
//                        if (temp.y < 0) break;
//                        temp.dirn = dirn;
//                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
//                        else temp.dirsteps = nodes[node].dirsteps + 1;
//                        if (((visited_map[temp.y][temp.x].dir_and_steps & (table[temp.dirn, temp.dirsteps - 1])) == table[temp.dirn, temp.dirsteps - 1]))
//                        {
//                            foreach (Neighbours ntemp in visited_map[temp.y][temp.x].neighbours)
//                            {
//                                if ((ntemp.dir_and_steps & table[temp.dirn, temp.dirsteps - 1]) == table[temp.dirn, temp.dirsteps - 1])
//                                    nodes[node].neighbours.Add(ntemp.node);
//                            }

//                            break;
//                        }
//                        temp.cost = map[temp.y][temp.x];
//                        temp.neighbours = new List<int>();
//                        nodes.Add(temp);
//                        //Console.WriteLine((nodes.Count-1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
//                        nodes[node].neighbours.Add(nodes.Count - 1);
//                        added = true;
//                        break;
//                }
//            }
//            //Console.ReadLine();
//        }
//        current_node++;
//        if (current_node < nodes.Count) active_nodes = true;
//        else active_nodes = false;
//    }
//}
void build_graph_p2(int current_node)
{
    table_p2[1] = 1;
    for (int i=1; i<10; i++)
    {
        table_p2[i + 1] = (long)16 * table_p2[i];
    }
    current_node = 0;
    // initial node
    Nodes2 temp = new Nodes2();
    temp.x = nodes[current_node].x + 4;
    temp.y = nodes[current_node].y;
    temp.dirn = 1;
    temp.dirsteps = 1;
    temp.cost = map[temp.y][temp.x] + map[temp.y][temp.x-1] + map[temp.y][temp.x-2] + map[temp.y][temp.x-3];
    temp.neighbours = new List<int>();
    nodes.Add(temp);
    nodes[current_node].neighbours.Add(nodes.Count - 1);
    temp = new Nodes2();
    temp.x = nodes[current_node].x;
    temp.y = nodes[current_node].y + 4;
    temp.dirn = 4;
    temp.dirsteps = 1;
    temp.cost = map[temp.y][temp.x] + map[temp.y-1][temp.x] + map[temp.y-2][temp.x] + map[temp.y-3][temp.x];
    temp.neighbours = new List<int>();
    nodes.Add(temp);
    nodes[current_node].neighbours.Add(nodes.Count - 1);
    bool active_nodes = true;
    while (active_nodes)
    {
        active_nodes = false;
        int limit = nodes[current_node].neighbours.Count;
        for (int thisnode = 0; thisnode < limit; thisnode++)
        //foreach(int node in nodes[current_node].neighbours) 
        {
            int node = nodes[current_node].neighbours[thisnode];
            if ((visited_map_p2[nodes[node].y][nodes[node].x].dir_and_steps > 0) &&
                ((visited_map_p2[nodes[node].y][nodes[node].x].dir_and_steps & (nodes[node].dirn * table_p2[nodes[node].dirsteps])) ==
                    nodes[node].dirn * table_p2[nodes[node].dirsteps]))
            {
                foreach (Neighbours2 ntemp in visited_map_p2[nodes[node].y][nodes[node].x].neighbours)
                {
                    if ((ntemp.dir_and_steps & nodes[node].dirn * table_p2[nodes[node].dirsteps]) == nodes[node].dirn * table_p2[nodes[node].dirsteps])
                        nodes[current_node].neighbours.Add(ntemp.node);
                }
                //nodes[node].dirn = 0;
                //nodes[current_node].neighbours.RemoveAt(thisnode);
                continue;
            }

            active_nodes = true;
            visited_map_p2[nodes[node].y][nodes[node].x].dir_and_steps += nodes[node].dirn * table_p2[nodes[node].dirsteps];
            Neighbours2 tempn = new Neighbours2();
            tempn.dir_and_steps = nodes[node].dirn * table_p2[nodes[node].dirsteps];
            tempn.node = node;
            visited_map_p2[nodes[node].y][nodes[node].x].neighbours.Add(tempn);
            List<int> dirns = new List<int>();
            dirns.Add(1); dirns.Add(2); dirns.Add(4); dirns.Add(8);
            switch (nodes[node].dirn)
            {
                case 1: dirns.Remove(2); break;
                case 2: dirns.Remove(1); break;
                case 4: dirns.Remove(8); break;
                case 8: dirns.Remove(4); break;
                case 0: dirns = new List<int>(); break;
            }
            if (nodes[node].dirsteps == 10) dirns.Remove(nodes[node].dirn);
            foreach (int dirn in dirns)
            {
                temp = new Nodes2();
                bool added = false;
                int inc = 1;
                if (nodes[node].dirn != dirn) inc = 4;
                switch (dirn)
                {
                    case 1: // going right
                        temp.x = nodes[node].x + inc;
                        temp.y = nodes[node].y;
                        if (temp.x >= map[0].Count) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = inc;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        if (((visited_map_p2[temp.y][temp.x].dir_and_steps & (temp.dirn * table_p2[temp.dirsteps])) == temp.dirn * table_p2[temp.dirsteps]))
                        {
                            foreach (Neighbours2 ntemp in visited_map_p2[temp.y][temp.x].neighbours)
                            {
                                if ((ntemp.dir_and_steps & temp.dirn * table_p2[temp.dirsteps]) == temp.dirn * table_p2[temp.dirsteps])
                                    nodes[node].neighbours.Add(ntemp.node);
                            }

                            break;
                        }
                        temp.cost = 0;
                        for (int c = 0; c < inc; c++) temp.cost += map[temp.y][temp.x - c];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        //Console.WriteLine((nodes.Count - 1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                    case 2: // going left
                        temp.x = nodes[node].x - inc;
                        temp.y = nodes[node].y;
                        if (temp.x < 0) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = inc;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        if (((visited_map_p2[temp.y][temp.x].dir_and_steps & (temp.dirn * table_p2[temp.dirsteps])) == temp.dirn * table_p2[temp.dirsteps]))
                        {
                            foreach (Neighbours2 ntemp in visited_map_p2[temp.y][temp.x].neighbours)
                            {
                                if ((ntemp.dir_and_steps & temp.dirn * table_p2[temp.dirsteps]) == temp.dirn * table_p2[temp.dirsteps])
                                    nodes[node].neighbours.Add(ntemp.node);
                            }

                            break;
                        }
                        temp.cost = 0;
                        for (int c = 0; c < inc; c++) temp.cost += map[temp.y][temp.x + c];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        //Console.WriteLine((nodes.Count - 1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                    case 4: // going down
                        temp.x = nodes[node].x;
                        temp.y = nodes[node].y + inc;
                        if (temp.y >= map.Count) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = inc;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        if (((visited_map_p2[temp.y][temp.x].dir_and_steps & (temp.dirn * table_p2[temp.dirsteps])) == temp.dirn * table_p2[temp.dirsteps]))
                        {
                            foreach (Neighbours2 ntemp in visited_map_p2[temp.y][temp.x].neighbours)
                            {
                                if ((ntemp.dir_and_steps & temp.dirn * table_p2[temp.dirsteps]) == temp.dirn * table_p2[temp.dirsteps])
                                    nodes[node].neighbours.Add(ntemp.node);
                            }

                            break;
                        }
                        temp.cost = 0;
                        for (int c = 0; c < inc; c++) temp.cost += map[temp.y - c][temp.x];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        //Console.WriteLine((nodes.Count - 1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                    case 8: // going up
                        temp.x = nodes[node].x;
                        temp.y = nodes[node].y - inc;
                        if (temp.y < 0) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        if (((visited_map_p2[temp.y][temp.x].dir_and_steps & (temp.dirn * table_p2[temp.dirsteps])) == temp.dirn * table_p2[temp.dirsteps]))
                        {
                            foreach (Neighbours2 ntemp in visited_map_p2[temp.y][temp.x].neighbours)
                            {
                                if ((ntemp.dir_and_steps & temp.dirn * table_p2[temp.dirsteps]) == temp.dirn * table_p2[temp.dirsteps])
                                    nodes[node].neighbours.Add(ntemp.node);
                            }

                            break;
                        }
                        temp.cost = 0;
                        for (int c = 0; c < inc; c++) temp.cost += map[temp.y + c][temp.x];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        //Console.WriteLine((nodes.Count-1) + ":" + temp.x + "," + temp.y + " in " + temp.dirn);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                }
            }
            //Console.ReadLine();
        }
        current_node++;
        if (current_node < nodes.Count) active_nodes = true;
        else active_nodes = false;
    }
}
//void P1()
//{
//    int result = 0;
//    int index = 0;
//    String data = "input.txt";
//    foreach (string line in System.IO.File.ReadLines(data))
//    {
//        map.Add(new List<int>());
//        visited_map.Add(new List<Visited>());
//        foreach (char c in line)
//        {
//            int ch = c - 0x30;
//            map[index].Add(ch);
//            Visited tempv = new Visited();
//            tempv.dir_and_steps = 0;
//            tempv.neighbours = new List<Neighbours>();
//            visited_map[index].Add(tempv);
//        }
//        index++;
//    }
//    int x = 0;
//    int y = 0;
//    int dirn = 1;   // 1 = Right, 2 = Left, 4 = Down, 8 = Up
//    int dirncount = 0;
//    Nodes temp = new Nodes();
//    temp.x = x; temp.y = y;
//    temp.cost = 0;
//    temp.dirn = 0;
//    temp.dirsteps = 0;
//    temp.neighbours = new List<int>();
//    nodes.Add(temp);
//    build_graph(0);
//    Dijkstra find_path= new Dijkstra();
//    List<int> destination=new List<int>();
////    graph = new int[nodes.Count, nodes.Count];
////    for (int i = 0; i < nodes.Count; i++)
////    {
////        if (nodes[i].dirn == 0)
////        {
////            nodes.RemoveAt(i);
////            i--;
////        }
////    }
//    for (int i=0; i<nodes.Count; i++)
//    {
//        if ((nodes[i].y == map.Count - 1) && (nodes[i].x == map[0].Count - 1)) destination.Add(i);
////        for (int j=0; j<nodes.Count; j++)
////        {
////            if (nodes[i].neighbours.Contains(j)) graph[i, j] = nodes[j].cost;
////            else graph[i, j] = 0;
////        }
////        Console.WriteLine("Node " + i + " at " + nodes[i].x + "," + nodes[i].y);
//    }
//    find_path.V = nodes.Count;
//    find_path.dijkstra(nodes, 0);
//    List<int> results = new List<int>();
//    foreach (int dest in destination)
//    {
//        results.Add(find_path.dist[dest]);
//        //Console.WriteLine(dest + " : " + find_path.dist[dest]);
//    }
//    int min = results.Min();
//    index = results.IndexOf(min);
//    int desti = destination[index];
//    x = nodes[desti].x;
//    y = nodes[desti].y;
//    while ((x!=0) || (y!=0))
//    {
//        Console.WriteLine(desti + ": " + x + "," + y);
//        desti = find_path.prev[desti];
//        x = nodes[desti].x;
//        y = nodes[desti].y;
//    }
//    Console.WriteLine(min);
//    Console.ReadLine();
//}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        visited_map_p2.Add(new List<Visited2>());
        foreach (char c in line)
        {
            int ch = c - 0x30;
            map[index].Add(ch);
            Visited2 tempv = new Visited2();
            tempv.dir_and_steps = 0;
            tempv.neighbours = new List<Neighbours2>();
            visited_map_p2[index].Add(tempv);
        }
        index++;
    }
    int x = 0;
    int y = 0;
    int dirn = 1;   // 1 = Right, 2 = Left, 4 = Down, 8 = Up
    int dirncount = 0;
    Nodes2 temp = new Nodes2();
    temp.x = x; temp.y = y;
    temp.cost = 0;
    temp.dirn = 0;
    temp.dirsteps = 0;
    temp.neighbours = new List<int>();
    nodes.Add(temp);
    build_graph_p2(0);
    Dijkstra find_path = new Dijkstra();
    List<int> destination = new List<int>();
    for (int i = 0; i < nodes.Count; i++)
    {
        if ((nodes[i].y == map.Count - 1) && (nodes[i].x == map[0].Count - 1)) destination.Add(i);
    }
    find_path.V = nodes.Count;
    find_path.dijkstra(nodes, 0);
    List<int> results = new List<int>();
    foreach (int dest in destination)
    {
        results.Add(find_path.dist[dest]);
        //Console.WriteLine(dest + " : " + find_path.dist[dest]);
    }
    int min = results.Min();
    index = results.IndexOf(min);
    int desti = destination[index];
    x = nodes[desti].x;
    y = nodes[desti].y;
    while ((x != 0) || (y != 0))
    {
        Console.WriteLine(desti + ": " + x + "," + y);
        desti = find_path.prev[desti];
        x = nodes[desti].x;
        y = nodes[desti].y;
    }
    Console.WriteLine(min);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
//t.Start();
//P1();
//t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");

class Nodes
{
    public int x;
    public int y;
    public int cost;
    public int dirn;
    public int dirsteps;
    public List<int> neighbours;
}

class Nodes2
{
    public int x;
    public int y;
    public int cost;
    public int dirn;
    public long dirsteps;
    public List<int> neighbours;
}

class Neighbours
{
    public int node;
    public int dir_and_steps;
}

class Neighbours2
{
    public int node;
    public long dir_and_steps;
}

class Visited
{
    public int dir_and_steps;
    public List<Neighbours> neighbours;
}

class Visited2
{
    public long dir_and_steps;
    public List<Neighbours2> neighbours;
}
