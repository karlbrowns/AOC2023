using System.Diagnostics;
using System.Text.RegularExpressions;


void P1()
{
    int result = 0;
    int index = 0;
    int galaxy = 1;
    String data = "input.txt";
    List<List<int>> map = new List<List<int>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        foreach (char c in line)
        {
            if (c == '.') map[index].Add(0);
            if (c == '#') map[index].Add(galaxy++);
        }
        index++;
    }
    int width = map[0].Count;
    int height = map.Count;
    bool[] empty_column = new bool[width];
    bool[] empty_row = new bool[height];
    for (int i = 0; i < height; i++)
    {
        empty_row[i] = true;
    }
    for (int j=0; j<width; j++)
    {
        empty_column[j] = true;
    }
    for (int i = 0; i<height; i++)
    {
        for (int j=0; j<width; j++)
        {
            if (map[i][j] != 0)
            {
                empty_column[j] = false;
                empty_row[i] = false;
            }
        }
    }
    int rows_added = 0;
    int cols_added = 0;
    for (int i=0; i<height; i++)
    {
        if (empty_row[i])
        {
            map.Insert(i + rows_added, new List<int>());
            for (int j=0; j<width; j++) { map[i + rows_added].Add(0); }
            rows_added++;
        }
    }
    height += rows_added;
    for (int j=0; j<width; j++)
    {
        if (empty_column[j])
        {
            for (int i=0; i<height; i++)
            {
                map[i].Insert(j + cols_added, 0);
            }
            cols_added++;
        }
    }
    width += cols_added;
    List<int[]> galaxies = new List<int[]>();
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (map[i][j] != 0) { 
                galaxies.Add(new int[2]);
                galaxies.Last()[0] = i;
                galaxies.Last()[1] = j;
            }
        }
    }
    for (int i=0; i<galaxies.Count-1; i++)
    {
        for (int j=i+1; j<galaxies.Count; j++)
        {
            int dist = Math.Abs(galaxies[i][0] - galaxies[j][0]) + Math.Abs(galaxies[i][1] - galaxies[j][1]);
            result += dist;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    long result = 0;
    int index = 0;
    int galaxy = 1;
    String data = "input.txt";
    List<List<int>> map = new List<List<int>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        foreach (char c in line)
        {
            if (c == '.') map[index].Add(0);
            if (c == '#') map[index].Add(galaxy++);
        }
        index++;
    }
    int width = map[0].Count;
    int height = map.Count;
    bool[] empty_column = new bool[width];
    bool[] empty_row = new bool[height];
    for (int i = 0; i < height; i++)
    {
        empty_row[i] = true;
    }
    for (int j = 0; j < width; j++)
    {
        empty_column[j] = true;
    }
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (map[i][j] != 0)
            {
                empty_column[j] = false;
                empty_row[i] = false;
            }
        }
    }
    int rows_added = 0;
    int cols_added = 0;
 //   for (int i = 0; i < height; i++)
 //   {
 //       if (empty_row[i])
 //       {
 //           map.Insert(i + rows_added, new List<int>());
 //           for (int j = 0; j < width; j++) { map[i + rows_added].Add(0); }
 //           rows_added++;
 //       }
 //   }
    height += rows_added;
 //   for (int j = 0; j < width; j++)
 //   {
 //       if (empty_column[j])
 //       {
 //           for (int i = 0; i < height; i++)
 //           {
 //               map[i].Insert(j + cols_added, 0);
 //           }
 //           cols_added++;
 //       }
 //   }
    width += cols_added;
    List<int[]> galaxies = new List<int[]>();
    for (int i = 0; i < height; i++)
    {
        for (int j = 0; j < width; j++)
        {
            if (map[i][j] != 0)
            {
                galaxies.Add(new int[2]);
                galaxies.Last()[0] = i;
                galaxies.Last()[1] = j;
            }
        }
    }
    for (int i = 0; i < galaxies.Count - 1; i++)
    {
        for (int j = i + 1; j < galaxies.Count; j++)
        {
            long dist = Math.Abs(galaxies[i][0] - galaxies[j][0]) + Math.Abs(galaxies[i][1] - galaxies[j][1]);
            int x1, x2, y1, y2;
            if (galaxies[i][0] > galaxies[j][0]) { y1 = galaxies[j][0]; y2 = galaxies[i][0]; }
            else { y1 = galaxies[i][0]; y2 = galaxies[j][0]; }
            if (galaxies[i][1]> galaxies[j][1]) { x1 = galaxies[j][1]; x2 = galaxies[i][1]; }
            else { x1 = galaxies[i][1]; x2 = galaxies[j][1]; }
            for (int k=x1; k<x2; k++)
            {
                if (empty_column[k]) dist += 999999;
            }
            for (int k=y1; k<y2; k++)
            {
                if (empty_row[k]) dist += 999999;
            }
            result += dist;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
