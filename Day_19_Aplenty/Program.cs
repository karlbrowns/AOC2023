﻿using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    string delim = "{},=";
    int start=-1;
    char[] delims = delim.ToCharArray();
    List<Workflow> workflows = new List<Workflow>();
    List<Part> Parts = new List<Part>();
    string[] parts;
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (line.Length == 0)
        {
            index++;
            continue;
        }
        switch (index)
        {
            case 0:
                parts = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    workflows.Add(new Workflow(parts[0]));
                    if (parts[0] == "in") start = workflows.Count - 1;
                    foreach (string part in parts.Skip(1)) // we don't want the name - we've already got that.
                    {
                        char t = part[0];
                        int it = 0;
                        int comp = 0;
                        switch (t)
                        {
                            case 'x': { it = 1; break; }
                            case 'm': { it = 2; break; }
                            case 'a': { it = 3; break; }
                            case 's': { it = 4; break; }
                            case 'A': { it = -1; break; }
                            case 'R': { it = -2; break; }
                        }
                        if (it >= 0)
                        {
                            t = part[1];
                            if (t == '>') comp = 1;
                            else if (t == '<') comp = 0;
                            else comp = -1;
                            if (comp < 0)
                            {
                                workflows.Last().conditions.Add(new Conditions(0, 0, 0, 0, part, ' '));
                            }
                            else
                            {
                                string[] subparts = part[2..].Split(':', StringSplitOptions.RemoveEmptyEntries);
                                int value = int.Parse(subparts[0]);
                                int idest = 0;
                                if (subparts[1] == "A") idest = 2;
                                if (subparts[1] == "R") idest = 1;
                                Conditions c = new Conditions(it, comp, value, idest, subparts[1], t);

                                workflows.Last().conditions.Add(c);
                            }
                        }
                        else
                        {
                            workflows.Last().conditions.Add(new Conditions(it, 0, 0, it + 3, part, t));

                        }
                    }
                }
                break;
            case 1:
                parts = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                int x = int.Parse(parts[1]);
                int m = int.Parse(parts[3]);
                int a = int.Parse(parts[5]);
                int s = int.Parse(parts[7]);
                Parts.Add(new Part(x, m, a, s));
                break;
        


        }
    }
    foreach (Part curpart in Parts)
    {
        int w = start;
        string next = "";
        bool reject = false;
        bool accept = false;
        while (!reject && !accept)
        {
            Workflow wk = workflows[w];
            foreach (Conditions c in wk.conditions)
            {
                bool test = false;
                if (c.itype > 0)
                {
                    int comp = (c.itype == 1) ? curpart.x : (c.itype == 2) ? curpart.m : (c.itype == 3) ? curpart.a : (c.itype == 4) ? curpart.s : 0;
                    int lt = c.comp;
                    int val = c.value;
                    if (lt == 0) test = comp < val; else test = comp > val;
                    if (test)
                    {
                        next = c.destination;
                        if (c.idest == 1)
                        {
                            reject = true;
                            break;
                        }
                        else if (c.idest == 2)
                        {
                            accept = true;
                            break;
                        }
                        break;
                    }
                    else continue;
                }
                if (c.idest == 1)
                {
                    reject = true;
                    break;
                }
                if (c.idest == 2)
                {
                    accept = true;
                    break;
                }
                next = c.destination;
                
            }
            if (!accept && !reject)
            {
                if (next != null)
                {
                    w = 0;
                    foreach (Workflow wki in workflows)
                    {
                        if (wki.name == next) break;
                        w++;
                    }

                }
            }
        }
        if (accept) result += curpart.sum();
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    string delim = "{},=";
    int start = -1;
    char[] delims = delim.ToCharArray();
    List<Workflow> workflows = new List<Workflow>();
    Dictionary<string,List<Conditions>> dworkflows = new Dictionary<string,List<Conditions>>();
    List<Part> Parts = new List<Part>();
    string[] parts;
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (line.Length == 0)
        {
            index++;
            continue;
        }
        switch (index)
        {
            case 0:
                parts = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    workflows.Add(new Workflow(parts[0]));
                    //if (parts[0] == "in") start = workflows.Count - 1;
                    foreach (string part in parts.Skip(1)) // we don't want the name - we've already got that.
                    {
                        char t = part[0];
                        int it = 0;
                        int comp = 0;
                        switch (t)
                        {
                            case 'x': { it = 1; break; }
                            case 'm': { it = 2; break; }
                            case 'a': { it = 3; break; }
                            case 's': { it = 4; break; }
                            case 'A': { it = -1; break; }
                            case 'R': { it = -2; break; }
                        }
                        if (it >= 0)
                        {
                            t = part[1];
                            if (t == '>') comp = 1;
                            else if (t == '<') comp = 0;
                            else comp = -1;
                            if (comp < 0)
                            {
                                workflows.Last().conditions.Add(new Conditions(0, 0, 0, 0, part, ' '));
                            }
                            else
                            {
                                string[] subparts = part[2..].Split(':', StringSplitOptions.RemoveEmptyEntries);
                                int value = int.Parse(subparts[0]);
                                int idest = 0;
                                if (subparts[1] == "A") idest = 2;
                                if (subparts[1] == "R") idest = 1;
                                Conditions c = new Conditions(it, comp, value, idest, subparts[1], t);

                                workflows.Last().conditions.Add(c);
                            }
                        }
                        else
                        {
                            workflows.Last().conditions.Add(new Conditions(it, 0, 0, it + 3, part, t));

                        }
                    }
                    dworkflows.Add(workflows.Last().name, workflows.Last().conditions);

                }
                break;
            case 1:
                break;


        }
    }
    int w = start;
    string next = "in";
    bool reject = false;
    bool accept = false;
    Queue<Part2> partials = new Queue<Part2>();
    List<bool> done = new List<bool>();
    while (!reject && !accept)
    {
        partials.Enqueue(new Part2("next"));
        while (partials.Count>0)
        {
            Part2 current = partials.Dequeue();
            List<Conditions> conds = dworkflows[current.dest];
            foreach (Conditions c in conds)
            {
                bool test = false;
                if (c.itype > 0)
                {
                    int lt = c.comp;
                    int val = c.value;
                    //partials.Enqueue(new Part2(c.destination));
                    
                    if (lt == 0) test = comp < val; else test = comp > val;
                    if (test)
                    {
                        next = c.destination;
                        if (c.idest == 1)
                        {
                            reject = true;
                            break;
                        }
                        else if (c.idest == 2)
                        {
                            accept = true;
                            break;
                        }
                        break;
                    }
                    else continue;
                }
                if (c.idest == 1)
                {
                    reject = true;
                    break;
                }
                if (c.idest == 2)
                {
                    accept = true;
                    break;
                }
                next = c.destination;

            }

        }
        if (!accept && !reject)
        {
            if (next != null)
            {
                w = 0;
                foreach (Workflow wki in workflows)
                {
                    if (wki.name == next) break;
                    w++;
                }

            }
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


class Conditions
{
    public char type;  // purely for tracking
    public int itype;  // 1 = x, 2 = m, 3 = a, 4 = s
    public int comp;   // 0 = <, 1 = >
    public int value;  // what to compare with
    public string destination; // the name of the workflow to go to (can be R or A)
    public int idest;  // the R = 1, A = 2

    public Conditions(int itype, int comp, int value, int idest, string destination, char type)
    {
        this.itype = itype;
        this.comp = comp;
        this.value = value;
        this.idest = idest;
        this.destination = destination;
        this.type = type;
    }

}
class Workflow
{
    public string name;
    public List<Conditions> conditions;

    public Workflow(string name)
    {
        this.name = name;
        this.conditions = new List<Conditions>();
    }
}

class Part
{
    public int x;
    public int m;
    public int a;
    public int s;

    public int sum()
    {
        return x + m + a + s;
    }
    public long mult()
    {
        return (long)x * (long)m * (long)a * (long)s;
    }
    public Part(int x, int m, int a, int s)
    {
        this.x = x;
        this.m = m;
        this.a = a;
        this.s = s;
    }
}

class Part2
{
    public int maxx;
    public int maxm;
    public int maxa;
    public int maxs;
    public int minx;
    public int minm;
    public int mina;
    public int mins;
    string dest;

    public int sum()
    {
        return x + m + a + s;
    }
    public long mult()
    {
        return (long)(maxx-minx) * (long)(maxm - minm) * (long)(maxa-mina) * (long)(maxs-mins);
    }
    public Part2(string dest)
    {
        this.dest = dest;
        minx = 1;
        minm = 1;
        mina = 1;
        mins = 1;
        maxx = 4000;
        maxm = 4000;
        maxa = 4000;
        maxs = 4000;
    }
}
