using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    long result = 0;
    int index = 0;
    List<List<int>> map = new List<List<int>>();
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        foreach (char c in line)
        {
            switch (c) {
                case 'O':
                    map[index].Add(1);
                    break;
                case '#':
                    map[index].Add(-1);
                    break;
                case '.':
                    map[index].Add(0);
                    break;
            }
        }
        index++;
    }
    bool moved = true;
    while (moved)
    {
        moved = false;
        for (int i = 1; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Count; j++)
            {
                if (map[i][j] == 1)
                {
                    if (map[i - 1][j] == 0)
                    {
                        map[i][j] = 0;
                        map[i - 1][j] = 1;
                        moved = true;
                    }
                }
            }
        }
    }
    for (int i=0; i<map.Count; i++)
    {
        for (int j=0; j < map[i].Count; j++)
        {
            if (map[i][j] == 1) result += map.Count - i;
        }
    }
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
