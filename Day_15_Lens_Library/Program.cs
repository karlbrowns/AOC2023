using System.Text.RegularExpressions;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

int Hash(string part)
{
    int temp = 0;
    for (int j = 0; j < part.Length; j++)
    {
        temp += (int)(part[j]);
        temp *= 17;
        temp &= 255;
    }
    return temp;
}

void P1()
{
    long result = 0;
    int index = 0;
    string data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(',');
        for (int i=0; i<parts.Length; i++)
        {
            int temp = Hash(parts[i]);
            result += temp;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    long result = 0;
    int index = 0;
    List<box>[] boxes = new List<box>[256];
    for (int i=0; i<256; i++) { boxes[i] = new List<box>(); }
    string data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(',');
        for (int i = 0; i < parts.Length; i++)
        {
            List<string> subparts = new List<string>();
            int op = 0;
            if (parts[i].Contains("="))
            {
                subparts.Add(parts[i].Substring(0,parts[i].IndexOf("=")));
                subparts.Add(parts[i].Substring(parts[i].IndexOf("=") + 1));
                op = 1;
            }
            else
            {
                subparts.Add(parts[i].Substring(0, parts[i].IndexOf("-")));
                op = 2;
            }
            int boxno = Hash(subparts[0]);
            int value = 0;
            if (op == 1)
            {
                value = int.Parse(subparts[1]);
                int j;
                for ( j=0; j< boxes[boxno].Count; j++)
                {
                    List<box> current = boxes[boxno];
                    int t = current.FindIndex(x => x.label == subparts[0]);
                    if (t!=-1) 
                    {
                        box t1 = current[t];
                        t1.lens = value;
                        current[t] = t1;
                        break;
                    }
                }
                if (j == boxes[boxno].Count)
                {
                    box temp2;
                    temp2.lens = value;
                    temp2.label = subparts[0].ToString();
                    boxes[boxno].Add(temp2); 
                }
            }
            else
            {
                List<box> current = boxes[boxno];
                int t = current.FindIndex(x => x.label == subparts[0]);
                if (t!=-1)
                {
                    current.RemoveAt(t);
                }
            }
        }
    }
    for (int i=0; i < boxes.Length; i++)
    {
        for (int j = 0; j < boxes[i].Count; j++)
        {
            result += (i + 1) * (j + 1) * boxes[i][j].lens;
        }
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

struct box
{
    public string label;
    public int lens;
}

