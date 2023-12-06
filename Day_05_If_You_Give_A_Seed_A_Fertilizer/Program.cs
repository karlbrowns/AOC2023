using System.Text.RegularExpressions;


void P1()
{
    Modes mode=Modes.seedlist;
    int result = 0;
    int index = 0;
    long i;
    String data = "input.txt";
    List<long> seeds = new List<long>();

    List<long[]> seedtosoil = new List<long[]>();
    List<long[]> soiltofert = new List<long[]>();
    List<long[]> ferttowate = new List<long[]>();
    List<long[]> watetoligh = new List<long[]>();
    List<long[]> lightotemp = new List<long[]>();
    List<long[]> temptohumi = new List<long[]>();
    List<long[]> humitoloca = new List<long[]>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (line.Length == 0) continue;
        if (line.Substring(0,5)=="seeds")
        {
            string[] seedstr = line.Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in seedstr) seeds.Add(long.Parse(s));
        }
        if (line.Substring(0,5)=="seed-")
        {
            mode = Modes.seedtosoil; continue;
        }
        if (line.Substring(0, 5) == "soil-")
        {
            mode = Modes.soiltofert; continue;
        }
        if (line.Substring(0, 5) == "ferti")
        {
            mode = Modes.ferttowater; continue;
        }
        if (line.Substring(0,5)=="water")
        {
            mode = Modes.watertolight; continue;
        }
        if (line.Substring(0,5)=="light")
        {
            mode = Modes.lighttotemp; continue;
        }
        if (line.Substring(0,5)=="tempe")
        {
            mode = Modes.temptohum; continue;
        }
        if (line.Substring(0,5)=="humid")
        {
            mode = Modes.humtoloc; continue;
        }
        string[] nums;
        switch (mode)
        {
            case Modes.seedtosoil:
                nums = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length==3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    seedtosoil.Add(temp);
                }
                break;
            case Modes.soiltofert:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    soiltofert.Add(temp);
                }
                break;
            case Modes.ferttowater:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    ferttowate.Add(temp);
                }
                break;
            case Modes.watertolight:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    watetoligh.Add(temp);
                }
                break;
            case Modes.lighttotemp:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    lightotemp.Add(temp);
                }
                break;
            case Modes.temptohum:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    temptohumi.Add(temp);
                }
                break;
            case Modes.humtoloc:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    humitoloca.Add(temp);
                }
                break;
        }
    }
    Console.WriteLine(seeds.Count);
    Console.WriteLine(seedtosoil.Count);
    Console.WriteLine(soiltofert.Count);
    Console.WriteLine(ferttowate.Count);
    Console.WriteLine(watetoligh.Count);
    Console.WriteLine(lightotemp.Count);
    Console.WriteLine(temptohumi.Count);
    Console.WriteLine(humitoloca.Count);
    List<long> soils = new List<long>();
    List<long> ferts = new List<long>();
    List<long> waters = new List<long>();
    List<long> lights = new List<long>();
    List<long> temps = new List<long>();
    List<long> humis = new List<long>();
    List<long> locas = new List<long>();
    bool found = false;
    foreach (long s in seeds)
    {
        found = false;
        foreach (long[] t in seedtosoil)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                soils.Add(i);
                found = true;
            }
        }
        if (!found) soils.Add(s);
    }
    foreach (long s in soils)
    {
        found = false;
        foreach (long[] t in soiltofert)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                ferts.Add(i);
                found = true;
            }
        }
        if (!found) ferts.Add(s);
    }
    foreach (long s in ferts)
    {
        found = false;
        foreach (long[] t in ferttowate)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                waters.Add(i);
                found = true;
            }
        }
        if (!found) waters.Add(s);
    }
    foreach (long s in waters)
    {
        found = false;
        foreach (long[] t in watetoligh)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                lights.Add(i);
                found = true;
            }
        }
        if (!found) lights.Add(s);
    }
    foreach (long s in lights)
    {
        found = false;
        foreach (long[] t in lightotemp)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                temps.Add(i);
                found = true;
            }
        }
        if (!found) temps.Add(s);
    }
    foreach (long s in temps)
    {
        found = false;
        foreach (long[] t in temptohumi)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                humis.Add(i);
                found = true;
            }
        }
        if (!found) humis.Add(s);
    }
    foreach (long s in humis)
    {
        found = false;
        foreach (long[] t in humitoloca)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                locas.Add(i);
                found = true;
            }
        }
        if (!found) locas.Add(s);
    }
    foreach (long s in locas)
    {
        Console.WriteLine(s);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}
void P2()
{
    Modes mode = Modes.seedlist;
    int result = 0;
    int index = 0;
    long i;
    String data = "input.txt";
    List<long> seedsinfo = new List<long>();
    List<long> seeds= new List<long>();

    List<long[]> seedtosoil = new List<long[]>();
    List<long[]> soiltofert = new List<long[]>();
    List<long[]> ferttowate = new List<long[]>();
    List<long[]> watetoligh = new List<long[]>();
    List<long[]> lightotemp = new List<long[]>();
    List<long[]> temptohumi = new List<long[]>();
    List<long[]> humitoloca = new List<long[]>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (line.Length == 0) continue;
        if (line.Substring(0, 5) == "seeds")
        {
            string[] seedstr = line.Substring(6).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in seedstr) seedsinfo.Add(long.Parse(s));
        }
        if (line.Substring(0, 5) == "seed-")
        {
            mode = Modes.seedtosoil; continue;
        }
        if (line.Substring(0, 5) == "soil-")
        {
            mode = Modes.soiltofert; continue;
        }
        if (line.Substring(0, 5) == "ferti")
        {
            mode = Modes.ferttowater; continue;
        }
        if (line.Substring(0, 5) == "water")
        {
            mode = Modes.watertolight; continue;
        }
        if (line.Substring(0, 5) == "light")
        {
            mode = Modes.lighttotemp; continue;
        }
        if (line.Substring(0, 5) == "tempe")
        {
            mode = Modes.temptohum; continue;
        }
        if (line.Substring(0, 5) == "humid")
        {
            mode = Modes.humtoloc; continue;
        }
        string[] nums;
        switch (mode)
        {
            case Modes.seedtosoil:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    seedtosoil.Add(temp);
                }
                break;
            case Modes.soiltofert:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    soiltofert.Add(temp);
                }
                break;
            case Modes.ferttowater:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    ferttowate.Add(temp);
                }
                break;
            case Modes.watertolight:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    watetoligh.Add(temp);
                }
                break;
            case Modes.lighttotemp:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    lightotemp.Add(temp);
                }
                break;
            case Modes.temptohum:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    temptohumi.Add(temp);
                }
                break;
            case Modes.humtoloc:
                nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (nums.Length == 3)
                {
                    long[] temp = new long[3];
                    i = 0;
                    foreach (string n in nums) { temp[i++] = long.Parse(n); }
                    humitoloca.Add(temp);
                }
                break;
        }
    }
    for (int j=0; j<seedsinfo.Count; j+=2)
    {
        long start = seedsinfo[j];
        long end = seedsinfo[j + 1];
        for (long k = 0; k < end; k++)
        {
            seeds.Add(k+start);
        }

    }
    Console.WriteLine(seeds.Count);
    Console.WriteLine(seedtosoil.Count);
    Console.WriteLine(soiltofert.Count);
    Console.WriteLine(ferttowate.Count);
    Console.WriteLine(watetoligh.Count);
    Console.WriteLine(lightotemp.Count);
    Console.WriteLine(temptohumi.Count);
    Console.WriteLine(humitoloca.Count);
    List<long> soils = new List<long>();
    List<long> ferts = new List<long>();
    List<long> waters = new List<long>();
    List<long> lights = new List<long>();
    List<long> temps = new List<long>();
    List<long> humis = new List<long>();
    List<long> locas = new List<long>();
    bool found = false;
    foreach (long s in seeds)
    {
        found = false;
        foreach (long[] t in seedtosoil)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                soils.Add(i);
                found = true;
            }
        }
        if (!found) soils.Add(s);
    }
    foreach (long s in soils)
    {
        found = false;
        foreach (long[] t in soiltofert)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                ferts.Add(i);
                found = true;
            }
        }
        if (!found) ferts.Add(s);
    }
    foreach (long s in ferts)
    {
        found = false;
        foreach (long[] t in ferttowate)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                waters.Add(i);
                found = true;
            }
        }
        if (!found) waters.Add(s);
    }
    foreach (long s in waters)
    {
        found = false;
        foreach (long[] t in watetoligh)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                lights.Add(i);
                found = true;
            }
        }
        if (!found) lights.Add(s);
    }
    foreach (long s in lights)
    {
        found = false;
        foreach (long[] t in lightotemp)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                temps.Add(i);
                found = true;
            }
        }
        if (!found) temps.Add(s);
    }
    foreach (long s in temps)
    {
        found = false;
        foreach (long[] t in temptohumi)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                humis.Add(i);
                found = true;
            }
        }
        if (!found) humis.Add(s);
    }
    foreach (long s in humis)
    {
        found = false;
        foreach (long[] t in humitoloca)
        {
            if ((s >= t[1]) && (s < t[1] + t[2]))
            {
                i = s - t[1] + t[0];
                locas.Add(i);
                found = true;
            }
        }
        if (!found) locas.Add(s);
    }
    foreach (long s in locas)
    {
        Console.WriteLine(s);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();

public enum Modes
{
    seedlist,
    seedtosoil,
    soiltofert,
    ferttowater,
    watertolight,
    lighttotemp,
    temptohum,
    humtoloc
};

