using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    int maxx, maxy, maxz;
    List<Bricks> bricks = new List<Bricks>();
    String data = "input.txt";
    char[] delims = { ',', '~' };
    maxx = maxy = maxz = 0;

    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
        Bricks b = new Bricks();
        if (parts.Length > 0)
        {
            b.x1 = int.Parse(parts[0]);
            b.y1 = int.Parse(parts[1]);
            b.z1 = int.Parse(parts[2]);
            b.x2 = int.Parse(parts[3]);
            b.y2 = int.Parse(parts[4]);
            b.z2 = int.Parse(parts[5]);
            bricks.Add(b);
            if ((b.x1 > b.x2) || (b.y1 > b.y2) || (b.z1 > b.z2)) Console.WriteLine("Unordered bricks: " + index);
            if (b.x2 > maxx) maxx = b.x2;
            if (b.y2 > maxy) maxy = b.y2;
            if (b.z2 > maxz) maxz = b.z2;
            index++;
        }
    }
    maxx++; maxy++; maxz++;
    int[,,] map = new int[maxx, maxy, maxz];
    for (int i=0; i<maxx; i++)
    {
        for (int j=0; j<maxy; j++)
        {
            for ( int k=0; k<maxz; k++)
            {
                map[i, j, k] = 0;
            }
        }
    }
    index = 1;
    foreach (Bricks b in bricks)
    {
        for (int i=b.x1; i<=b.x2; i++)
        {
            for (int j=b.y1; j<=b.y2; j++)
            {
                for (int k=b.z1; k<=b.z2; k++)
                {
                    map[i, j, k] = index;
                }
            }
        }
        index++;
    }
    List<Bricks> sorted_bricks = bricks.OrderBy(b => b.z1).ToList();
    foreach (Bricks b in sorted_bricks)
    {
        bool stopped = false;
        while ((b.z1 > 1) && !stopped) {
            stopped = false;
            for (int i=b.x1; i<=b.x2; i++)
            {
                for (int j=b.y1; j<=b.y2; j++)
                {
                    if (map[i,j,b.z1-1] != 0)
                    {
                        stopped = true;
                    }
                    if (stopped) break;
                }
                if (stopped) break;
            }
            if (!stopped)
            {
                for (int i = b.x1; i <= b.x2; i++)
                {
                    for (int j = b.y1; j <= b.y2; j++)
                    {
                        for (int k = b.z1; k<=b.z2; k++)
                        {
                            map[i, j, k - 1] = map[i, j, k];
                            map[i, j, k] = 0;
                        }
                    }
                    if (stopped) break;
                }
                b.z1--;
                b.z2--;

            }
        }
    }
    foreach(Bricks b in sorted_bricks)
    {
        index = map[b.x1, b.y1, b.z1];
        if (b.z1==b.z2)
        {
            List<int> above = new List<int>();
            for (int i = b.x1; i <= b.x2; i++)
            {
                for (int j = b.y1; j <= b.y2; j++)
                {
                    if (map[i, j, b.z1 + 1] != 0) above.Add(map[i, j, b.z1 + 1]);
                }
            }
            for (int check = 0; check < above.Count; check++)
            {
                Bricks c = bricks[above[check] - 1];
                bool removed = false;
                for (int i=c.x1; i<=c.x2; i++)
                {
                    for (int j = c.y1; j<= c.y2; j++)
                    {
                        if ((map[i,j,c.z1 - 1] != index) && (map[i,j,c.z1-1] != 0))
                        {
                            above.RemoveAt(check);
                            removed = true;
                            check--;
                            break;
                        }
                    }
                    if (removed) break;
                }
            }
            if (above.Count == 0) result++;
        }
        else
        {
            List<int> above = new List<int>();
            if ((map[b.x1, b.y1, b.z2 + 1] != 0)) above.Add(map[b.x1, b.y1, b.z2 + 1]);
            for (int check = 0; check < above.Count; check++)
            {
                Bricks c = bricks[above[check] - 1];
                bool removed = false;
                for (int i = c.x1; i <= c.x2; i++)
                {
                    for (int j = c.y1; j <= c.y2; j++)
                    {
                        if ((map[i, j, c.z1 - 1] != index) && (map[i, j, c.z1 - 1] != 0))
                        {
                            above.RemoveAt(check);
                            removed = true;
                            break;
                        }
                    }
                    if (removed) break;
                }
            }
            if (above.Count == 0) result++;

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

class Bricks
{
    public int x1, y1, z1;
    public int x2,y2,z2;

    public Bricks()
    {
        x1 = y1 = z1 = 0;
        x2 = y2 = z2 = 0;
    }
}