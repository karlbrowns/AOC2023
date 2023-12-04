using System.Text.RegularExpressions;
using System.Threading.Tasks.Sources;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(':', '|');
        if (parts.Length>2)
        {
            string[] wins = parts[1].Split(' ',StringSplitOptions.RemoveEmptyEntries);
            string[] nums = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int score = 0;
            List<int> win= new List<int>();
            List<int> numbers = new List<int>();
            foreach (string w in wins) win.Add(int.Parse(w));
            foreach (string n in nums) numbers.Add(int.Parse(n));
            foreach (int n in numbers)
            {
                if (win.IndexOf(n) != -1) score++;
            }
            result += (int)Math.Pow((double)2 ,(double)(score - 1));

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
    List<int> cards = new List<int>();
    List<int> scores = new List<int>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(':', '|');
        if (parts.Length > 2)
        {
            string[] wins = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] nums = parts[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int score = 0;
            List<int> win = new List<int>();
            List<int> numbers = new List<int>();
            foreach (string w in wins) win.Add(int.Parse(w));
            foreach (string n in nums) numbers.Add(int.Parse(n));
            foreach (int n in numbers)
            {
                if (win.IndexOf(n) != -1) score++;
            }
            scores.Add(score);
            cards.Add(1);
            //result += (int)Math.Pow((double)2, (double)(score - 1));

        }
    }
    for (int i=0; i<cards.Count; i++)
    {
        for (int j=i+1; j < i+scores[i]+1; j++)
        {
            cards[j]+=cards[i];
        }
    }
    for (int i=0; i<cards.Count; i++)
    {
        //Console.WriteLine(cards[i]);
        result += cards[i];
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();