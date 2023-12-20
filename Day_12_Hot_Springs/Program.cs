using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;
using System.Formats.Asn1;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

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
        current.result = true;
        hotSprings.Add(current);
    }
    int maxwidth = 0;
    for (int i=0; i<hotSprings.Count; i++)
    {
        int options = 0;
        HotSprings current = hotSprings[i];
        int width = current.data.Sum() + current.data.Count - 1;
        int testwidth = current.map.Length;
        if (testwidth > maxwidth) maxwidth = testwidth;
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
                //Console.WriteLine(test);
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
        //Console.WriteLine(options);
        result += options;

    }
    Console.WriteLine(maxwidth);
    Console.WriteLine(result);
    Console.ReadLine();
}

void calc_option (HotSprings current, int step, List<int> startpos, int index_updated, int testwidth, UInt128[] tests)
{
    int j = step;
    UInt128 test;

    //if ((j > index_updated)) startpos[j] = startpos[j - 1] + current.data[j - 1] + 1;
    while (startpos[j] < testwidth)
    {
        test = tests[j] << startpos[j];
        if ((test & current.full_mask) == test) break;
        startpos[j]++;
    }
    if (startpos[j] == testwidth)
    {
        current.result = false;
        return;
    }
    else
    {
        if (j==current.data.Count-1)
        {
            UInt128 full_test = 0;
            for (int k = 0; k < current.data.Count; k++)
            {
                full_test += tests[k] << startpos[k];
            }
            if ((full_test & current.hash_mask) == current.hash_mask)
            {
                current.result = true;
            }
        }
        else calc_option(current, j+1, startpos, index_updated, testwidth, tests);
        return;
    }
    
}

void test_func (HotSprings current)
{
    current.data.Add(0);
    current.result = true;
    return;
}
void P2()
{   
    int result = 0;
    int index = 0;
    UInt128[] power2 = new UInt128[32];
    UInt128 one = 1;
    String data = "inputtst.txt";
    List<HotSprings> hotSprings = new List<HotSprings>();


    for (int i = 0; i < 32; i++) power2[i] = (one << i) - 1;
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(' ');
        HotSprings current = new HotSprings();
        current.map = parts[0];
        current.data = new List<int>();
        string[] subparts = parts[1].Split(',');
        for (int j=0; j<5; j++)
        {
            for (int i = 0; i < subparts.Length; i++)
            {
                current.data.Add(int.Parse(subparts[i]));
            }
        }
        current.map = parts[0] + "?" + parts[0] + "?" + parts[0] + "?" + parts[0] + "?" + parts[0];

        UInt128 hmask, fmask;
        hmask = fmask = 0;
        for (int i = 0; i < current.map.Length; i++)
        {
            if (current.map[i] == '#') hmask |= (one << i);
            if ((current.map[i] == '#') || (current.map[i] == '?')) fmask |= (one << i);
        }
        current.hash_mask = hmask;
        current.full_mask = fmask;
        current.result = false;
        hotSprings.Add(current);
        test_func(current);
    }
    int maxwidth = 0;
    for (int i = 0; i < hotSprings.Count; i++)
    {
        int options = 0;
        HotSprings current = hotSprings[i];
        int width = current.data.Sum() + current.data.Count - 1;
        int testwidth = current.map.Length;
        if (testwidth > maxwidth) maxwidth = testwidth;
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
        UInt128[] tests = new UInt128[current.data.Count];
        for (int j=0; j<current.data.Count; j++)
        {
            tests[j] = power2[current.data[j]];
        }
        int lastpos = testwidth - current.data.Last();
        bool done = false;
        bool firstpass = true;
        int index_updated = 0;
        while (!current.result)
        {
            UInt128 test = 0;
            int j,k,l;
            for (j = 0; j < current.data.Count; j++)
            {
                //Console.WriteLine(j);

                if ((j > index_updated)) startpos[j] = startpos[j - 1] + current.data[j - 1] + 1;
            }
            while (!current.result)
            {
                calc_option(current, 0, startpos, 0, testwidth, tests);
                if (current.result) options++;
            }
//                while (startpos[j]<testwidth)
//                {
//                    test = tests[j] << startpos[j];
//                    if ((test & current.full_mask) == test) break;
//                    startpos[j]++;
//                }
//                //for (int m = 0; m < current.data.Count; m++) Console.Write(startpos[m] + ",");
//                //Console.Write("\r");
//                if ((test & current.full_mask) == test)
//                {
//                    if (j == current.data.Count - 1)
//                    {
//                        UInt128 full_test = 0;
//                        for (k = 0; k < current.data.Count; k++)
//                        {
//                            full_test += tests[k] << startpos[k];
//                        }
//                        if ((full_test & current.hash_mask) == current.hash_mask)
//                        {
//                            options++;
//                            for (int m = 0; m < current.data.Count; m++) Console.Write(startpos[m] + ",");
//                            Console.WriteLine();
//                            // this is the first one we've found, so these are the minimum start positions that work.
//                            // Store them.  
//                            if (firstpass)
//                            {
//                                for (l = 0; l < startpos.Count - 1; l++)
//                                {
//                                    origstartpos[l] = startpos[l];
//                                }
//                                firstpass = false;
//                            }
//                            break;
//                        }
//                    }
//                    else continue;
//                    //Console.WriteLine(test);
//                }
//            }
//            // and then iterate over the next positions
//            // but what if one position is now beyond where it can go
//            // we keep going through until something else breaks and that
//            // a) takes too long, b) will break something else or whatever
//            if (startpos.Last() >= testwidth)
//            {
//                index_updated++;
//                if (index_updated == current.data.Count)
//                {
//                    done = true;
//                    break;
//                }
//                for (l=0; l<index_updated; l++)
//                {
//                    startpos[l] = origstartpos[l];
//                }
//                startpos[l]++;
//
//            }
//            else
//            {
//                for (l = 0; l < startpos.Count - 1; l++)
//                {
//                    if (startpos[l] + 1 + current.data[l] < startpos[l + 1])
//                    {
//                        startpos[l]++;
//                        index_updated = l;
//                        break;
//                    }
//                }
//                if (l == startpos.Count - 1)
//                {
//                    startpos[l]++;
//                    index_updated = l;
//                    if (startpos[l] >= lastpos + 1) done = true;
//                }
//            }
//            for (int m = 0; m < l; m++) { startpos[m] = origstartpos[m]; }
//            //Console.WriteLine();
//            Console.WriteLine(options + ":");
//
        }

        Console.WriteLine();
        Console.WriteLine(i + " " + options);
        result += options;

    }
    Console.WriteLine(result);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
//t.Start();
//P1();
//t.Stop();
//Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Start();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");

struct HotSprings
{
    public string map;
    public List<int> data;
    public UInt128 hash_mask;
    public UInt128 full_mask;
    public bool result;
};