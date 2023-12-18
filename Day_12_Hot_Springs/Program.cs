using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Formats.Asn1;

void P1()
{
    int result = 0;
    int index = 0;
    uint[] power2 = new uint[32];
    uint one = 1;
    String data = "input.txt";
    List<HotSprings> hotSprings = new List<HotSprings>();
    
    
    for (int i = 0; i < 32; i++) power2[i] = (one<<i) - 1;
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(' ');
        HotSprings current = new HotSprings();
        current.map = parts[0];
        current.data = new List<int>();
        string[] subparts = parts[1].Split(',');
        for (int i=0; i < subparts.Length; i++)
        {
            current.data.Add(int.Parse(subparts[i]));
        }
        uint hmask, fmask;
        hmask = fmask = 0;
        for (int i=0; i< current.map.Length; i++)
        {
            if (current.map[i] == '#') hmask |= (one<<i);
            if ((current.map[i] == '#') || (current.map[i] == '?')) fmask |= (one<<i);
        }
        current.hash_mask = hmask;
        current.full_mask = fmask;
        hotSprings.Add(current);
    }
    for (int i=0; i<hotSprings.Count; i++)
    {
        int options = 0;
        HotSprings current = hotSprings[i];
        int width = current.data.Sum() + current.data.Count - 1;
        int testwidth = current.map.Length;
        List<int> startpos = new List<int>();
        List<int> origstartpos = new List<int>();
        for (int j = 0; j < current.data.Count; j++)
        {
            if (j == 0)
            {
                startpos.Add(0);
                origstartpos.Add(0);
            }
            else
            {
                startpos.Add(startpos[j - 1] + current.data[j - 1] + 1);
                origstartpos.Add(startpos[j - 1] + current.data[j - 1] + 1);
            }
        }
        int lastpos = testwidth - current.data.Last();
        bool done = false;
        while (!done)
        {
            uint test = 0;
            int j;
            for (j=0; j<current.data.Count; j++)
            {
                test += (power2[current.data[j]] << startpos[j]);
            }
            if (((test & current.hash_mask) == current.hash_mask) && ((test & current.full_mask) == test))
            {
                options++;
                Console.WriteLine(test);
            }
            for (j=0; j<startpos.Count - 1; j++)
            {
                if (startpos[j] + 1 + current.data[j] < startpos[j + 1])
                {
                    startpos[j]++;
                    break;
                }
            }
            if (j==startpos.Count - 1)
            {
                startpos[j]++;
                if (startpos[j] == lastpos+1) done = true;
            }
            for (int k=0; k<j; k++) { startpos[k] = origstartpos[k]; }
        }
        Console.WriteLine(options);
        result += options;

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

struct HotSprings
{
    public string map;
    public List<int> data;
    public uint hash_mask;
    public uint full_mask;
};