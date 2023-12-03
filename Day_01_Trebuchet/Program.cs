
using System.Text.RegularExpressions;

void P1()
{
    int result = 0;
    String data = "input.txt";
    int row = 0;
    int index = 0;
    foreach (string line in System.IO.File.ReadLines(data))
    {
        int d1, d2;
        Match m1 = Regex.Match(line, "\\d");
        Match m2 = Regex.Match(line, "(\\d)(?!.*\\d)");
        d1 = int.Parse(m1.Value);
        d2 = int.Parse(m2.Value);
        result += 10 * d1 + d2;
        row++;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    String data = "input.txt";
    int row = 0;
    int index = 0;
    List<string> tests = new List<string>();
    tests.Add("\\d");
    tests.Add("one");
    tests.Add("two");
    tests.Add("three");
    tests.Add("four");
    tests.Add("five");
    tests.Add("six");
    tests.Add("seven");
    tests.Add("eight");
    tests.Add("nine");
    tests.Add("zero");
    
    foreach (string line in System.IO.File.ReadLines(data))
    {
        int d1, d2;
        List<Match> m1 = new List<Match>();
        List<Match> m2 = new List<Match>();
        foreach (string test in tests)
        {
            m1.Add( Regex.Match(line, test));
            m2.Add(Regex.Match(line, "(" + test + ")(?!.*" + test + ")"));
        }
        d1 = line.Length;
        Match md1=null,md2=null;
        d2 = 0;
        foreach (Match m in m1)
        {
            if ((m.Success) && (m.Index <= d1))
            {
                d1 = m.Index;
                md1 = m;
            }
        }
        foreach (Match m in m2)
        {
            if ((m.Success) && (m.Index >= d2))
            {
                d2 = m.Index;
                md2 = m;
            }
            
        }
        if (int.TryParse(md1.Value, out d1)) { 
        }
        else
        {
            int i=1;
            while (md1.Value != tests[i]) i++;
            d1 = i;
            if (d1 == 10) d1 = 0;
        }
        if (int.TryParse(md2.Value, out d2))
        {
        }
        else
        {
            int i = 1;
            while (md2.Value != tests[i]) i++;
            d2 = i;
            if (d2 == 10) d2 = 0;
        }
        result += d1 * 10 + d2;
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();