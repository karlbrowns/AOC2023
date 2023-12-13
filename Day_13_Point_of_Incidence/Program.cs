using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Reflection;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    bool newmap = true;
    List<List<List<int>>> maph = new List<List<List<int>>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (newmap)
        {
            maph.Add(new List<List<int>>());
            newmap = false;
        }
        if (line.Length > 0)
        {
            maph.Last().Add(new List<int>());
            foreach (char c in line)
            {
                if (c == '.') maph.Last().Last().Add(0);
                else maph.Last().Last().Add(1);
            }
        }
        else newmap = true;
    }
    List<List<List<int>>> mapv = new List<List<List<int>>>();
    for (int i=0; i<maph.Count; i++)
    {
        mapv.Add(new List<List<int>>());
        for (int j=0; j < maph[i][0].Count; j++)
        {
            mapv[i].Add(new List<int>());
            for (int k=0; k < maph[i].Count; k++)
            {
                mapv[i][j].Add(maph[i][k][j]);
            }
        }
    }
    for (int i=0; i<maph.Count; i++)
    {
        int mapheight = maph[i].Count;
        bool reflect = true;
        for (int j=0; j < maph[i].Count - 1; j++)
        {
            reflect = true;
            for (int k=0; k<=j; k++)
            {
                if (k + j + 1 < mapheight)
                {
                    if (maph[i][j-k].SequenceEqual(maph[i][k + j + 1])) ;
                    else reflect = false;
                }
            }
            if (reflect) { 
                result += (j + 1) * 100;
                Console.WriteLine("maph: " + (j + 1).ToString());
            }
        }
    }
    for (int i = 0; i < mapv.Count; i++)
    {
        int mapheight = mapv[i].Count;
        bool reflect = true;
        for (int j = 0; j < mapv[i].Count - 1; j++)
        {
            reflect = true;
            for (int k = 0; k < j+1 ; k++)
            {
                if (k + j + 1 < mapheight)
                {
                    if (mapv[i][j-k].SequenceEqual(mapv[i][k + j + 1])) ;
                    else reflect = false;
                }
            }
            if (reflect) { 
                result += (j + 1) * 1;
                Console.WriteLine("mapv: " + (j + 1).ToString());

            }
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

int check_reflect(List<List<List<int>>> map, List<int> reflectv, int i, bool add, string text)
{
    int mapheight = map[i].Count;
    bool reflect = true;
    int result = -1;
    for (int j = 0; j < map[i].Count - 1; j++)
    {
        reflect = true;
        for (int k = 0; k < j + 1; k++)
        {
            if (k + j + 1 < mapheight)
            {
                if (map[i][j - k].SequenceEqual(map[i][k + j + 1])) ;
                else reflect = false;
            }
        }
        if (reflect)
        {
            result = (j + 1) * 1;
            if (add) reflectv.Add(j);
            else
            {
                if (j == reflectv[i]) result = -1;
            }
            if (result >= 0)
            {
                Console.WriteLine("map " + text + ": " + (j + 1).ToString());
                return result;
            }
        }
        
    }
    if (result==-1) if (add) reflectv.Add(-1);
    return result;

}
void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    int i, j, k;
    bool newmap = true;
    List<List<List<int>>> maph = new List<List<List<int>>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (newmap)
        {
            maph.Add(new List<List<int>>());
            newmap = false;
        }
        if (line.Length > 0)
        {
            maph.Last().Add(new List<int>());
            foreach (char c in line)
            {
                if (c == '.') maph.Last().Last().Add(0);
                else maph.Last().Last().Add(1);
            }
        }
        else newmap = true;
    }
    List<List<List<int>>> mapv = new List<List<List<int>>>();
    for (i = 0; i < maph.Count; i++)
    {
        mapv.Add(new List<List<int>>());
        for (j = 0; j < maph[i][0].Count; j++)
        {
            mapv[i].Add(new List<int>());
            for (k = 0; k < maph[i].Count; k++)
            {
                mapv[i][j].Add(maph[i][k][j]);
            }
        }
    }
    List<int> reflecth = new List<int>();
    List<int> reflectv = new List<int>();
    for (i = 0; i < maph.Count; i++)
    {
        result += (check_reflect(maph, reflecth, i, true, "h") * 100);
    }
    for (i = 0; i < mapv.Count; i++)
    {
        result += (check_reflect(mapv, reflectv, i, true, "v"));
    }
    int test, test2;
    result = 0;
    Console.WriteLine();
    for (i=0; i<maph.Count; i++)
    {
        Console.WriteLine("Map number: " + i);
        test = test2 = -1;
        for (j=0; j < maph[i].Count; j++)
        {
            for (k=0; k < maph[i][j].Count; k++)
            {
                maph[i][j][k] = 1 - maph[i][j][k];
                mapv[i][k][j] = 1 - mapv[i][k][j];
                test = check_reflect(maph, reflecth, i, false,"h");
                test2 = check_reflect(mapv, reflectv, i, false,"v");
                maph[i][j][k] = 1 - maph[i][j][k];
                mapv[i][k][j] = 1 - mapv[i][k][j];
                if ((test != -1) || (test2 != -1)) break;
            }
            if ((test != -1) || (test2 != -1))
            {
                Console.WriteLine("x: " + k + " y: " + j);
                break;
            }
        }
        if (test >= 0) result += (test * 100);
        if (test2 >= 0) result += test2;
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
