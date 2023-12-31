using System.Text.RegularExpressions;
using System.Diagnostics;

List<Nodes> nodes = new List<Nodes>();
List<List<int>> map = new List<List<int>>();
List<List<int>> visited_map = new List<List<int>>();

int[,] table = new int[9,4];
void build_graph(int current_node)
{
    table[1,0] = 1;
    table[1,1] = 16;
    table[1,2] = 256;
    table[2,0] = 2;
    table[2,1] = 32;
    table[2,2] = 512;
    table[4,0] = 4;
    table[4,1] = 64;
    table[4,2] = 1024;
    table[8,0] = 8;
    table[8,1] = 128;
    table[8,2] = 2048;
    current_node = 0;
    // initial node
    Nodes temp;
    temp.x = nodes[current_node].x + 1;
    temp.y = nodes[current_node].y;
    temp.dirn = 1;
    temp.dirsteps = 1;
    temp.cost = map[temp.y][temp.x];
    temp.neighbours = new List<int>();
    nodes.Add(temp);
    nodes[current_node].neighbours.Add(nodes.Count - 1);
    temp = new Nodes();
    temp.x = nodes[current_node].x;
    temp.y = nodes[current_node].y + 1;
    temp.dirn = 4;
    temp.dirsteps = 1;
    temp.cost = map[temp.y][temp.x];
    temp.neighbours = new List<int>();
    nodes.Add(temp);
    nodes[current_node].neighbours.Add(nodes.Count - 1);
    bool active_nodes = true;
    while (active_nodes) 
    {
        active_nodes = false;
        for (int thisnode; thisnode < nodes[current_node].neighbours.Count; thisnode++)
        {
            int node = nodes[current_node].neighbours[thisnode];
            if (visited_map[nodes[node].y][nodes[node].x] == nodes[node].dirn * table[nodes[node].dirn,nodes[node].dirsteps - 1])
            {
                nodes[node].dirn = 0;
                continue;
            }
            active_nodes = true;
            visited_map[nodes[node].y][nodes[node].x] = nodes[node].dirn * table[nodes[node].dirn,nodes[node].dirsteps - 1];
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
            if (nodes[node].dirsteps == 3) dirns.Remove(nodes[node].dirn);

            foreach (int dirn in dirns)
            {
                temp = new Nodes();
                bool added = false;
                switch (dirn)
                {
                    case 1: // going right
                        temp.x = nodes[node].x + 1;
                        temp.y = nodes[node].y;
                        if (temp.x >= map[0].Count) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        temp.cost = map[temp.y][temp.x];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                    case 2: // going left
                        temp.x = nodes[node].x - 1;
                        temp.y = nodes[node].y;
                        if (temp.x < 0) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        temp.cost = map[temp.y][temp.x];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                    case 4: // going down
                        temp.x = nodes[node].x;
                        temp.y = nodes[node].y + 1;
                        if (temp.y >= map.Count) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        temp.cost = map[temp.y][temp.x];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                    case 8: // going up
                        temp.x = nodes[node].x;
                        temp.y = nodes[node].y - 1;
                        if (temp.y < 0) break;
                        temp.dirn = dirn;
                        if (dirn != nodes[node].dirn) temp.dirsteps = 1;
                        else temp.dirsteps = nodes[node].dirsteps + 1;
                        temp.cost = map[temp.y][temp.x];
                        temp.neighbours = new List<int>();
                        nodes.Add(temp);
                        nodes[node].neighbours.Add(nodes.Count - 1);
                        added = true;
                        break;
                }
            }
        }
        current_node++;
    }
}
void P1()
{
    int result = 0;
    int index = 0;
    String data = "inputtst.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        visited_map.Add(new List<int>());
        foreach (char c in line)
        {
            int ch = c - 0x30;
            map[index].Add(ch);
            visited_map[index].Add(0);
        }
        index++;
    }
    int x = 0;
    int y = 0;
    int dirn = 1;   // 1 = Right, 2 = Left, 4 = Down, 8 = Up
    int dirncount = 0;
    Nodes temp;
    temp.x = x; temp.y = y;
    temp.cost = 0;
    temp.dirn = 0;
    temp.dirsteps = 0;
    temp.neighbours = new List<int>();
    nodes.Add(temp);
    build_graph(0);

    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");

struct Nodes
{
    public int x;
    public int y;
    public int cost;
    public int dirn;
    public int dirsteps;
    public List<int> neighbours;
}

