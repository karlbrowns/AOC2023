using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    string instructions="";
    Dictionary<string, string[]> map = new Dictionary<string, string[]>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (index == 0) instructions = line;
        if (index > 1)
        {
            string key = line.Substring(0, 3);
            string[] values = new string[2];
            values[0] = line.Substring(7, 3);
            values[1] = line.Substring(12, 3);
            map.Add(key, values);

        }
        index++;
    }
    string start = "AAA";
    int position = 0;
    while (start != "ZZZ")
    {
        string[] values = map[start];
        if (instructions[position] == 'L')
        {
            start = values[0];
        }
        else start = values[1];
        position = (position + 1) % instructions.Length;
        result++;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    long result = 0;
    int index = 0;
    String data = "input.txt";
    string instructions = "";
    List<string> starts = new List<string>();
    List<string> ends = new List<string>();
    
    
    Dictionary<string, string[]> map = new Dictionary<string, string[]>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        if (index == 0) instructions = line;
        if (index > 1)
        {
            string[] values = new string[2];
            string key = line.Substring(0, 3);
            values[0] = line.Substring(7, 3);
            values[1] = line.Substring(12, 3);
            map.Add(key, values);
            if (key[2] == 'A') starts.Add(key);
            if (key[2] == 'Z') ends.Add(key);
        }
        index++;
    }
    //string start = "AAA";
    int position = 0;
    bool done = false;
    string start;
    List<long> initial_steps = new List<long>();
    List<long>[] next_z_array = new List<long>[instructions.Length];
    List<string>[] next_end_array = new List<string>[instructions.Length];
    List<string> initial_end = new List<string>();
    while (!done)
    {
        bool test = true;
        for (int i=0; i< starts.Count; i++)
        {
            start = starts[i];
            position = 0;
            result = 0;
            while (start[2] != 'Z')
            {
                string[] values = map[start];
                if (instructions[position] == 'L')
                {
                    start = values[0];
                }
                else start = values[1];
                position = (position + 1) % instructions.Length;
                result++;
            }
            initial_steps.Add(result);
            initial_end.Add(start);
            Console.WriteLine("Start: " + starts[i] + ", End: " + start + ", Steps: " + result);
        }
        for (int i=0; i<instructions.Length; i++)
        {
            next_z_array[i] = new List<long>();
            next_end_array[i] = new List<string>();
        }
        for (int j = 0; j< ends.Count; j++)
        {
            
            for (int i = 0; i < instructions.Length; i++)
            {

                start = ends[j]; position = i;
                result = 0;
                do
                {
                    string[] values = map[start];
                    if (instructions[position] == 'L')
                    {
                        start = values[0];
                    }
                    else start = values[1];
                    position = (position + 1) % instructions.Length;
                    result++;
                }
                while (start[2] != 'Z');
                next_z_array[i].Add(result);
                next_end_array[i].Add(start);
                Console.WriteLine(result);
            }
        }
        done = true;
        
    }
    result = 0;
    long[] current_iteration = new long[6];
    long[] current_start = new long[6];
    done = false;
    while (!done)
    {
        for (int i=0; i<starts.Count; i++)
        {
            current_iteration[i] = initial_steps[i];
            current_start[i] = ends.IndexOf(initial_end[i]);
        }
        do
        {
            long min = current_iteration.Min();
            index = Array.IndexOf(current_iteration, min);
            
            current_iteration[index] += next_z_array[current_iteration[index] % instructions.Length][(int)current_start[index]];
            current_start[index] = ends.IndexOf(next_end_array[current_iteration[index] % instructions.Length][(int)current_start[index]]);
        }
        while (!((current_iteration[0] == current_iteration[1]) && (current_iteration[1] == current_iteration[2]) && (current_iteration[2] == current_iteration[3]) && (
            current_iteration[3] == current_iteration[4]) && (current_iteration[4] == current_iteration[5])));
        done = true;
    }
    Console.WriteLine(current_iteration[0]);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine(t.ElapsedMilliseconds);
t.Start();
P2();
t.Stop();
Console.WriteLine(t.ElapsedMilliseconds);