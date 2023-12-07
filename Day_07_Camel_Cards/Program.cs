using System.Text.RegularExpressions;

void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<part1> cards = new List<part1>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        part1 temp;
        temp.hand = parts[0];
        temp.sorted_hand = String.Concat(parts[0].OrderBy(c => c));
        temp.score = int.Parse(parts[1]);
        temp.rank = 0;
        cards.Add(temp);
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

P1();
P2();

public struct part1
{
    public string hand;
    public string sorted_hand;
    public int score;
    public int rank;
};

public class tComp : IComparer<part1>
{
    public int Compare(part1 x, part1 y)
    {
        if (x.sorted_hand)





        return 0;

    }

};

