using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;

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
    //Console.ReadLine();
}

Stopwatch t = new Stopwatch();

void P2()
{
    long result = 0;
    int index = 0;
    List<List<int>> map = new List<List<int>>();
    List<List<List<int>>> all_maps = new List<List<List<int>>>();
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        foreach (char c in line)
        {
            switch (c)
            {
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
    long count = 0;
    while (count < 1000000000)
    {
        if (count > 1000)
        {
            all_maps.Add(new List<List<int>>());
            for (int i = 0; i < map.Count; i++)
            {
                all_maps.Last().Add(new List<int>());
                for (int j = 0; j < map[0].Count; j++)
                {
                    all_maps.Last()[i].Add(map[i][j]);
                }
            }
        }
        if (count==1100)
        {
            for (int k=1; k<all_maps.Count; k++)
            {
                bool same = true;
                for (int i=0; i<map.Count; i++)
                {
                    for (int j=0; j < map[i].Count; j++)
                    {
                        if (all_maps[0][i][j] != all_maps[k][i][j]) same = false;
                    }
                }
                if (same) { Console.WriteLine("Match at " + k); }
            }
            Console.ReadLine();
            long temp = 1034;
            do
            {
                temp += 34;
            }
            while (temp < 1000000000);
            temp -= 34;
            temp = 1000000000 - temp;
            Console.WriteLine("Final iteration = " + temp);
            Console.ReadLine();
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (all_maps[9][i][j] == 1) result += map.Count - i;
                }
            }
            Console.WriteLine(result);
            Console.ReadLine();
        }
        // tilt north
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
        // tilt west
        moved = true;
        while (moved)
        {
            moved = false;
            for (int i = 1; i < map[0].Count; i++)
            {
                for (int j = 0; j < map.Count; j++)
                {
                    if (map[j][i] == 1)
                    {
                        if (map[j][i-1] == 0)
                        {
                            map[j][i] = 0;
                            map[j][i-1] = 1;
                            moved = true;
                        }
                    }
                }
            }
        }
        moved = true;
        // tilt south
        while (moved)
        {
            moved = false;
            for (int i = map.Count-2; i >=0 ; i--)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    if (map[i][j] == 1)
                    {
                        if (map[i + 1][j] == 0)
                        {
                            map[i][j] = 0;
                            map[i + 1][j] = 1;
                            moved = true;
                        }
                    }
                }
            }
        }
        // tilt west
        moved = true;
        while (moved)
        {
            moved = false;
            for (int i = map[0].Count-2; i >= 0; i--)
            {
                for (int j = 0; j < map.Count; j++)
                {
                    if (map[j][i] == 1)
                    {
                        if (map[j][i + 1] == 0)
                        {
                            map[j][i] = 0;
                            map[j][i + 1] = 1;
                            moved = true;
                        }
                    }
                }
            }
        }
        //if (count < 3)
//        {
//            for (int i = 0; i < map.Count; i++)
//            {
//                for (int j = 0; j < map[0].Count; j++)
//                {
//                    switch (map[i][j])
//                    {
//                        case -1: Console.Write('#'); break;
//                        case 0: Console.Write('.'); break;
//                        case 1: Console.Write('O'); break;
//                    }
//                }
//                Console.WriteLine();
//            }
//            Console.WriteLine();
//        }
        count++;
        if ((count % 1000000) == 0) Console.WriteLine(t.ElapsedMilliseconds);
    }
    for (int i = 0; i < map.Count; i++)
    {
        for (int j = 0; j < map[i].Count; j++)
        {
            if (map[i][j] == 1) result += map.Count - i;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
