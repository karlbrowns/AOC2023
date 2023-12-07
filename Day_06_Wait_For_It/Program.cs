using System.Runtime.InteropServices;
using System.Text.RegularExpressions;


void P1()
{
    int result = 0;
    int index = 0;
    List<int> times = new List<int>();
    List<int> dists = new List<int>();
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (index==0)
        {
            string[] snums = line.Substring(10).Split(' ',StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in snums)
            {
                if (int.TryParse(s, out int o)!=false)
                {
                    times.Add(o);
                }
            }
        }
        if (index == 1)
        {
            string[] snums = line.Substring(10).Split( ' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in snums)
            {
                if (int.TryParse(s, out int o) != false)
                {
                    dists.Add(o);
                }
            }
        }
        index++;
    }
    List<int> best = new List<int>();
    result = 1;
    for (int i=0; i<times.Count; i++)
    {
        int dist = dists[i];
        int time = times[i];
        best.Add(0);

        for (int j=0; j<=time; j++)
        {
            int k = j * (time - j);
            if (k > dist) best[i]++;
        }
        result *= best[i]; 
    }
   
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 1;
    long time = 60947882;
    long dist = 475213810151650;
    List<int> best = new List<int>();
    best.Add(0);

    for (long j = 0; j <= time; j++)
    {
        long k = j * (time - j);
        if (k > dist) best[0]++;
    }
    result *= best[0];
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();