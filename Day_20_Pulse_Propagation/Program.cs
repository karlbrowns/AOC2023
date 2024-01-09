using System.Text.RegularExpressions;
using System.Diagnostics;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "inputtst.txt";
    char[] delims = { '-', '>', ',' };
    Dictionary<string, Module> modules = new Dictionary<string, Module>();
    Module button = new Module("button", 1);
    modules.Add("button", button);
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
        this.type = type;   // 1 = button, 2 = broadcaste, 3 = flip-flop, 4 = and, 5 = output
        this.inputs = new List<string>();
        this.outputs = new List<string>();
        this.last_in = new List<int>();
        this.state = 0;
    }
}