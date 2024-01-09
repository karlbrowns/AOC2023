using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    char[] delims = { '-', '>', ',',' ' };
    Dictionary<string, Module> modules = new Dictionary<string, Module>();
    Module button = new Module("button", 1);
    modules.Add("button", button);
    modules["button"].outputs.Add("broadcaster");
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(delims,StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 0)
        {
            int type = 0;
            parts[0] = parts[0].Trim();
            if (parts[0][0] == '%') type = 3;
            else if (parts[0][0] == '&') type = 4;
            else if (string.Compare(parts[0],"broadcaster")==0) type = 2;
            else type = 5;
            string name;
            if ((type == 3) || (type == 4)) name = parts[0].Substring(1);
            else name = parts[0];
            Module temp = new Module(name, type);
            for (int i = 1; i<parts.Length; i++)
            {
                temp.outputs.Add(parts[i]);
            }
            modules.Add(name, temp);
        }
    }
    List<string> outputs = new List<string>();
    foreach (Module m in modules.Values)
    {
        foreach (string d in m.outputs)
        {
            if (!modules.ContainsKey(d)) outputs.Add(d);
        }
    }
    foreach (string d in outputs)
    {
        modules.Add(d, new Module(d, 0));
    }
    foreach (Module m in modules.Values)
    {
        foreach (string d in m.outputs)
        {
            if (!modules.ContainsKey(d))
            {
                modules.Add(d, new Module(d, 0));
            }
            modules[d].inputs.Add(m.name);  // create the bijection
            modules[d].last_in.Add(0);
            
        }
    }
    int lows = 0;
    int highs = 0;
    for (int i=0; i<1000; i++)
    {
        Queue<Pulse> pulses = new Queue<Pulse>();
        pulses.Enqueue(new Pulse(0,"button", "broadcaster"));
        while (pulses.Count > 0)
        {
            Pulse p = pulses.Dequeue();
            //Console.WriteLine(p.source + " sends " + p.value + " to " + p.destination);
            if (p.value == 0) lows++;
            else highs++;
            Module m = modules[p.destination];
            switch (m.type)
            {
                case 1: pulses.Enqueue(new Pulse(0, "button", "broadcaster")); break;
                case 2:
                    foreach (string d in m.outputs)
                    {
                        pulses.Enqueue(new Pulse(0, m.name, d));
                    }
                    break;
                case 3:
                    if (p.value == 0)
                    {
                        m.state = 1 - m.state;
                        foreach (string d in m.outputs)
                        {
                            pulses.Enqueue(new Pulse(m.state, m.name, d));
                        }
                    }
                    break;
                case 4:
                    int j = m.inputs.IndexOf(p.source);
                    m.last_in[j] = p.value;
                    bool all_high = true;
                    foreach (int v in m.last_in) { if (v == 0) { all_high = false; break; } }
                    foreach (string d in m.outputs)
                    {
                        pulses.Enqueue(new Pulse(all_high ? 0 : 1, m.name, d));
                    }
                    break;
            }
        }
    }
    result = (lows) * highs;
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    long result = 0;
    int index = 0;
    String data = "input.txt";
    char[] delims = { '-', '>', ',', ' ' };
    Dictionary<string, Module> modules = new Dictionary<string, Module>();
    Module button = new Module("button", 1);
    modules.Add("button", button);
    modules["button"].outputs.Add("broadcaster");
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(delims, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 0)
        {
            int type = 0;
            parts[0] = parts[0].Trim();
            if (parts[0][0] == '%') type = 3;
            else if (parts[0][0] == '&') type = 4;
            else if (string.Compare(parts[0], "broadcaster") == 0) type = 2;
            else type = 5;
            string name;
            if ((type == 3) || (type == 4)) name = parts[0].Substring(1);
            else name = parts[0];
            Module temp = new Module(name, type);
            for (int i = 1; i < parts.Length; i++)
            {
                temp.outputs.Add(parts[i]);
            }
            modules.Add(name, temp);
        }
    }
    List<string> outputs = new List<string>();
    foreach (Module m in modules.Values)
    {
        foreach (string d in m.outputs)
        {
            if (!modules.ContainsKey(d)) outputs.Add(d);
        }
    }
    foreach (string d in outputs)
    {
        modules.Add(d, new Module(d, 0));
    }
    foreach (Module m in modules.Values)
    {
        foreach (string d in m.outputs)
        {
            if (!modules.ContainsKey(d))
            {
                modules.Add(d, new Module(d, 0));
            }
            modules[d].inputs.Add(m.name);  // create the bijection
            modules[d].last_in.Add(0);

        }
    }
    long presses = 0;
    bool rx_low = false;
    while (presses<10000) 
    {
        rx_low = false;
        int rx_lows = 0;
        Queue<Pulse> pulses = new Queue<Pulse>();
        pulses.Enqueue(new Pulse(0, "button", "broadcaster"));
        presses++;
        while (pulses.Count > 0)
        {
            Pulse p = pulses.Dequeue();
            //Console.WriteLine(p.source + " sends " + p.value + " to " + p.destination);
            if ((p.value == 1) && ((p.source == "lh") || (p.source =="fk") || (p.source =="ff") || (p.source =="mm")))
            {
                //rx_lows++;
                Console.WriteLine("1 at " + p.source + " in " + presses + " button presses");
            }
            //if (p.destination == "rx") Console.Write('r');
            Module m = modules[p.destination];
            switch (m.type)
            {
                case 1: pulses.Enqueue(new Pulse(0, "button", "broadcaster")); break;
                case 2:
                    foreach (string d in m.outputs)
                    {
                        pulses.Enqueue(new Pulse(0, m.name, d));
                    }
                    break;
                case 3:
                    if (p.value == 0)
                    {
                        m.state = 1 - m.state;
                        foreach (string d in m.outputs)
                        {
                            pulses.Enqueue(new Pulse(m.state, m.name, d));
                        }
                    }
                    break;
                case 4:
                    int j = m.inputs.IndexOf(p.source);
                    m.last_in[j] = p.value;
                    bool all_high = true;
                    foreach (int v in m.last_in) { if (v == 0) { all_high = false; break; } }
                    foreach (string d in m.outputs)
                    {
                        pulses.Enqueue(new Pulse(all_high ? 0 : 1, m.name, d));
                    }
                    break;
            }
        }
        if (rx_lows == 1) rx_low = true;
        //if ((presses % 1000) == 0) Console.Write('.');
        if ((presses % 1000000) == 0) Console.Write('!');
    }
    Console.WriteLine(presses);
    Console.ReadLine();
}

Stopwatch t = new Stopwatch();
t.Start();
//P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");

class Module
{
    public string name;             // redundant, because it will be in the dictionary, but here for clarity
    public int type;
    public int state;
    public List<string> inputs;
    public List<string> outputs;
    public List<int> last_in;

    public Module (string name, int type)
    {
        this.name = name;
        this.type = type;   // 0 = no function, 1 = button, 2 = broadcaster, 3 = flip-flop, 4 = and, 5 = output
        this.inputs = new List<string>();
        this.outputs = new List<string>();
        this.last_in = new List<int>();
        this.state = 0;
    }
}

class Pulse
{
    public int value;
    public string destination;
    public string source;

    public Pulse(int value, string source, string destination)
    {
        this.value = value;
        this.source = source;
        this.destination = destination;
    }
}