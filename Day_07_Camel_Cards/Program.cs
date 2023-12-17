using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using AdventOfCodeUtilities;

/*
Every hand is exactly one type. From strongest to weakest, they are:

Five of a kind, where all five cards have the same label: AAAAA
AAAAA
AAAA J
AAA JJ
AA JJJ
A JJJJ
 JJJJJ
Four of a kind, where four cards have the same label and one card has a different label: AA8AA
AAAAB
AAAB J
AAB JJ
AB JJJ

Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
AAABB
AABB J

Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
AABC J
ABC JJ
Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
No jokers here
One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
ABCDJ
AABCD
High card, where all cards' labels are distinct: 23456
*/

void P1()
{
    long result = 0;
    int index = 0;
    String data = "input.txt";
    List<part1> cards = new List<part1>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(' ',StringSplitOptions.RemoveEmptyEntries);
        part1 temp;
        string hand = "";
        for (int i = 0; i < parts[0].Length; i++)
        {
            char c = parts[0][i];
            switch (c)
            {
                case 'A': hand = String.Concat(hand, 'E'); break;
                case 'K': hand = String.Concat(hand, 'D'); break;
                case 'Q': hand = String.Concat(hand, 'C'); break;
                case 'J': hand = String.Concat(hand, 'B'); break;
                case 'T': hand = String.Concat(hand, 'A'); break;
                default: hand = String.Concat(hand, c); break;
            }
        }
        temp.hand = hand;
        Dictionary<char, int> temp_dict = new Dictionary<char, int>();
        for (int i=0; i < parts[0].Length; i++)
        {
            if (temp_dict.ContainsKey(parts[0][i])) { temp_dict[parts[0][i]]++; }
            else temp_dict.Add(parts[0][i], 1);
        }
        //List<card_freq> tlist = new List<card_freq>();
        List<int> counts = new List<int>();
        foreach (var kvp in temp_dict.OrderByDescending(kvp => kvp.Value)) counts.Add(kvp.Value);
        List<int> tPos = new List<int>();
        List<char> uCards = new List<char>();
        temp.counts = counts;
        temp.jPos = tPos;
        temp.unique_cards = uCards;
        temp.cards = temp_dict;
        temp.score = int.Parse(parts[1]);
        temp.rank = 0;
        cards.Add(temp);
    }
    List<part1> sorted_cards = cards.OrderByLambda(widget => widget, (x , y) =>
    {
        char c1, c2;
        int xcount1 = 0, xcount2 = 0;
        int ycount1 = 0, ycount2 = 0;
        xcount1 = x.counts[0];
        ycount1 = y.counts[0];
        //if (xcount1 > ycount1) return 1;
        //if (xcount1 < ycount1) return -1;
        switch (xcount1)
        {
            case 5:
                if (ycount1 != 5) return 1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
                break;
            case 4:
                if (ycount1 < 4) return 1;
                if (ycount1 > 4) return -1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
            case 3:
                if (ycount1 < 3) return 1;
                if (ycount1 > 3) return -1;
                switch (x.counts[1])
                {
                    case 2:
                        if (y.counts[1] < 2) return 1;
                        if (y.counts[1] > 2) return -1;
                        break;
                    case 1:
                        if (y.counts[1] > x.counts[1]) return -1;
                        break;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
            case 2:
                if (ycount1 > 2) return -1;
                if (ycount1 < 2) return 1;
                if (x.counts[1] > y.counts[1]) return 1;
                if (y.counts[1] > x.counts[1]) return -1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
            case 1:
                if (ycount1 > 1) return -1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
        
        }
        return 0;
        //xcount1 = x.sorted_hand.Count(xx => xx == c1);
        //if (xcount1 < 5)
        //{
        //    c2 = x.sorted_hand[xcount1];
        //    xcount2 = x.sorted_hand.Count(xx => xx == c2);
        //}
        //c1 = y.sorted_hand[0];
        //ycount1 = y.sorted_hand.Count(yy => yy == c1);
        //if (ycount1 < 5)
        //{
        //    c2 = y.sorted_hand[ycount1];
        //    ycount2 = y.sorted_hand.Count(yy => yy == c2);
        //}
        //if (xcount1 > ycount2) return 1;
        //if (ycount1 > xcount1) return -1;
        //return 0;
    }).ToList();
    for (int i=0; i<sorted_cards.Count; i++)
    {
        result += (i + 1) * sorted_cards[i].score;
        Console.WriteLine(sorted_cards[i].hand + " : " + sorted_cards[i].score);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<part1> cards = new List<part1>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        index++;
        if (index==230)
        {
            Console.WriteLine("Problem line");
        }
        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<int> tPos = new List<int>();
        List<char> uCards = new List<char>();
        part1 temp;
        string hand = "";
        string hand2 = "";
        for (int i = 0; i < parts[0].Length; i++)
        {
            char c = parts[0][i];
            switch (c)
            {
                case 'A': hand = String.Concat(hand, 'E'); break;
                case 'K': hand = String.Concat(hand, 'D'); break;
                case 'Q': hand = String.Concat(hand, 'C'); break;
                case 'J':
                    {
                        hand = String.Concat(hand, '0'); 
                        tPos.Add(i);
                        break;
                    }
                case 'T': hand = String.Concat(hand, 'A'); break;
                default: hand = String.Concat(hand, c); break;
            }
        }
        hand2 = hand.ToString();
        temp.hand = hand;
        Dictionary<char, int> temp_dict = new Dictionary<char, int>();
        for (int i = 0; i < parts[0].Length; i++)
        {
            if (temp_dict.ContainsKey(hand[i])) { temp_dict[hand[i]]++; }
            else temp_dict.Add(hand[i], 1);
        }
        //List<card_freq> tlist = new List<card_freq>();
        List<int> counts = new List<int>();
        foreach (var kvp in temp_dict.OrderByDescending(kvp => kvp.Value))
        {
            counts.Add(kvp.Value);
            uCards.Add(kvp.Key);
        }
        if (tPos.Count > 0)
        {
            if (uCards[0]!='0')
            {
                hand = hand.Replace('0', uCards[0]);
            }
            else
            {
                if (tPos.Count != 5) hand = hand.Replace('0', uCards[1]);
                
            }
            temp_dict = new Dictionary<char, int>();
            for (int i = 0; i < 5; i++)
            {
                if (temp_dict.ContainsKey(hand[i])) { temp_dict[hand[i]]++; }
                else temp_dict.Add(hand[i], 1);
            }
            counts = new List<int>();
            foreach (var kvp in temp_dict.OrderByDescending(kvp => kvp.Value))
            {
                counts.Add(kvp.Value);
                uCards.Add(kvp.Key);
            }
            uCards = new List<char>();
            temp.hand = hand2;
        }
        temp.counts = counts;
        temp.cards = temp_dict;
        temp.score = int.Parse(parts[1]);
        temp.rank = 0;
        temp.jPos = tPos;
        temp.unique_cards = uCards;
        cards.Add(temp);
    }
    List<part1> sorted_cards = cards.OrderByLambda(widget => widget, (x, y) =>
    {
        char c1, c2;
        int xcount1 = 0, xcount2 = 0;
        int ycount1 = 0, ycount2 = 0;
        xcount1 = x.counts[0];
        ycount1 = y.counts[0];
        //if (xcount1 > ycount1) return 1;
        //if (xcount1 < ycount1) return -1;
        switch (xcount1)
        {
            case 5:
                if (ycount1 != 5) return 1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
                break;
            case 4:
                if (ycount1 < 4) return 1;
                if (ycount1 > 4) return -1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
            case 3:
                if (ycount1 < 3) return 1;
                if (ycount1 > 3) return -1;
                switch (x.counts[1])
                {
                    case 2:
                        if (y.counts[1] < 2) return 1;
                        if (y.counts[1] > 2) return -1;
                        break;
                    case 1:
                        if (y.counts[1] > x.counts[1]) return -1;
                        break;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
            case 2:
                if (ycount1 > 2) return -1;
                if (ycount1 < 2) return 1;
                if (x.counts[1] > y.counts[1]) return 1;
                if (y.counts[1] > x.counts[1]) return -1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;
            case 1:
                if (ycount1 > 1) return -1;
                for (int i = 0; i < 5; i++)
                {
                    if (x.hand[i] > y.hand[i]) return 1;
                    else if (y.hand[i] > x.hand[i]) return -1;
                }
                return 0;

        }
        return 0;
        //xcount1 = x.sorted_hand.Count(xx => xx == c1);
        //if (xcount1 < 5)
        //{
        //    c2 = x.sorted_hand[xcount1];
        //    xcount2 = x.sorted_hand.Count(xx => xx == c2);
        //}
        //c1 = y.sorted_hand[0];
        //ycount1 = y.sorted_hand.Count(yy => yy == c1);
        //if (ycount1 < 5)
        //{
        //    c2 = y.sorted_hand[ycount1];
        //    ycount2 = y.sorted_hand.Count(yy => yy == c2);
        //}
        //if (xcount1 > ycount2) return 1;
        //if (ycount1 > xcount1) return -1;
        //return 0;
    }).ToList();
    for (int i = 0; i < sorted_cards.Count; i++)
    {
        result += (i + 1) * sorted_cards[i].score;
        Console.WriteLine(sorted_cards[i].hand + " : " + sorted_cards[i].score);
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();

public struct part1
{
    public string hand;
    public Dictionary<char , int > cards;
    public List<int> counts;
    public List<int> jPos;
    public List<char> unique_cards;
    public int score;
    public int rank;
};
