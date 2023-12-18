using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "inputtst.txt";
    List<HotSprings> hotSprings = new List<HotSprings>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(' ');
        HotSprings current = new HotSprings();
        current.map = parts[0];
        current.data = new List<int>();
        current.done = new List<bool>();
        string[] subparts = parts[1].Split(',');
        for (int i=0; i < subparts.Length; i++)
        {
            current.data.Add(int.Parse(subparts[i]));
            current.done.Add(false);
        }
        hotSprings.Add(current);
    }
    for (int i=0; i<hotSprings.Count; i++)
    {
        List<int> options = new List<int>();
        HotSprings current = hotSprings[i];
        string[] valid_bits = current.map.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (valid_bits.Length == current.data.Count)
        {
            // we have enough sections to data - this should be easy (hahaha)
            for (int j=0; j<valid_bits.Length; j++)
            {
                if (valid_bits[j].Length != current.data[j])
                {
                    if (current.data[j] > valid_bits[j].Length)
                    {
                        Console.WriteLine("We shouldn't get here, I think");
                    }
                    else
                    {
                        options.Add(valid_bits[j].Length * (valid_bits[j].Length - current.data[j]));
                    }
                }
                if (valid_bits[j].Length == current.data[j])
                {
                    current.done[j] = true;
                    options.Add(1);
                }

            }
        }
        else
        {
            int v = 0;
            for (int j=0; j < current.data.Count; j++)
            {
                if (current.data[j] > valid_bits[v].Length)
                {
                    Console.WriteLine("We shouldn't get here");
                }
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

struct HotSprings
{
    public string map;
    public List<int> data;
    public List<bool> done;
};