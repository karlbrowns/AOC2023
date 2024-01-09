using System.Text.RegularExpressions;
using System.Diagnostics;
using System.ComponentModel.DataAnnotations;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "inputtst.txt";
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
    //Console.ReadLine();
}

void P2()
{
    long result = 0;
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
    List<Part2> results = new List<Part2>();
    List<bool> done = new List<bool>();
    //while (!reject && !accept)
    {
        partials.Enqueue(new Part2("in"));
        while (partials.Count>0)
        {
            Part2 current = partials.Dequeue();
            List<Conditions> conds = dworkflows[current.dest];
            int cond_count = current.nextcond;
            Console.WriteLine("Rule: " + current.dest + " condition " + cond_count);
            Conditions c = conds[cond_count];
            {
                cond_count++;
                if (c.itype > 0)
                {
                    int lt = c.comp;
                    int val = c.value;
                    //partials.Enqueue(new Part2(c.destination));
                    Part2 nextpart = new Part2("", current);
                    switch (c.itype)
                    {
                        case 1:
                            if (lt == 0)
                            {
                                current.maxx = val - 1;
                                nextpart.minx = val;
                            }
                            else
                            {
                                current.minx = val + 1;
                                nextpart.maxx = val;
                            }
                            break;
                        case 2:
                            if (lt == 0)
                            {
                                current.maxm = val - 1;
                                nextpart.minm = val;
                            }
                            else
                            {
                                current.minm = val + 1;
                                nextpart.maxm = val;
                            }
                            break;
                        case 3:
                            if (lt == 0)
                            {
                                current.maxa = val - 1;
                                nextpart.mina = val;
                            }
                            else
                            {
                                current.mina = val + 1;
                                nextpart.maxa = val;
                            }
                            break;
                        case 4:
                            if (lt == 0)
                            {
                                current.maxs = val - 1;
                                nextpart.mins = val;
                            }
                            else
                            {
                                current.mins = val + 1;
                                nextpart.maxs = val;
                            }
                            break;
                    }
                    if (c.idest == 2)
                    {
                        results.Add(current);
                        Console.WriteLine("Add: " + current.dest + " " + current.nextcond + ":" + (current.maxx - current.minx + 1) + "," + (current.maxm - current.minm + 1) + "," + (current.maxa - current.mina + 1) + "," + (current.maxs - current.mins + 1));
                        if (c.type > 0)
                        {
                            nextpart.dest = current.dest;
                            nextpart.nextcond = cond_count;
                            partials.Enqueue(nextpart);
                        }
                    }
                    else if (c.idest == 0)
                    {
                        nextpart.dest = current.dest;
                        current.dest = c.destination;
                        current.nextcond = 0;
                        partials.Enqueue(current);
                        nextpart.nextcond = cond_count;
                        partials.Enqueue(nextpart);
                        cond_count++;
                    }
                    else if (c.idest == 1)
                    {
                        nextpart.nextcond = cond_count;
                        nextpart.dest = current.dest;
                        partials.Enqueue(nextpart);
                    }
                }
                else if (c.itype==0) 
                {
                    current.dest = c.destination;
                    current.nextcond = 0;
                    partials.Enqueue(current);
                }
                else if (c.itype==-1)
                {
                    results.Add(current);
                }
            }

        }
    }
    foreach (Part2 p in results)
    {
        result += p.mult();
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
    public int nextcond;
    public string dest;

    public long mult()
    {
        return (long)(maxx-minx+1) * (long)(maxm - minm+1) * (long)(maxa-mina+1) * (long)(maxs-mins+1);
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
        nextcond = 0;
    }
    public Part2(string dest, Part2 part)
    {
        this.dest = dest;
        minx = part.minx;
        minm = part.minm;
        mina = part.mina;
        mins = part.mins;
        maxx = part.maxx;
        maxm = part.maxm;
        maxa = part.maxa;
        maxs = part.maxs;
        nextcond = 0;
    }
}
