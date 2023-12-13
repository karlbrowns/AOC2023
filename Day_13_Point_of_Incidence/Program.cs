using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;

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
