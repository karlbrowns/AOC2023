using System.Text.RegularExpressions;


void P1()
{
    long result = 0;
    int index = 0;
    String data = "input.txt";
    List<List<List<int>>> input = new List<List<List<int>>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        input.Add(new List<List<int>>());
        input[index].Add(new List<int>());
        String[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in parts)
        {
            input[index][0].Add(int.Parse(s));
        }
        index++;
    }
    for (int i=0; i<input.Count; i++)
    {
        bool done = false;
        int j=0;
        int diffcount = 1;
        while (!done)
        {
            done = true;
            input[i].Add(new List<int>());
            for (j = 0; j< input[i][diffcount - 1].Count -1 ; j++)
            {
                int diff = input[i][diffcount - 1][j + 1] - input[i][diffcount - 1][j];
                input[i][diffcount].Add(diff);
                //Console.Write(diff + " ");
                if (diff != 0) done = false;
            }
            //Console.WriteLine();
            if (input[i][diffcount].Count < 2)
            {
                Console.WriteLine("Error - diffs don't converge");
                break;
            }
            diffcount++;
        }
        diffcount--;
        int temp=0;
        for (int k=diffcount-1; k>=0; k--)
        {
            temp += input[i][k][input[i][k].Count - 1];
        }
        result += temp;
        Console.WriteLine(temp);

    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    long result = 0;
    int index = 0;
    String data = "input.txt";
    List<List<List<int>>> input = new List<List<List<int>>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        input.Add(new List<List<int>>());
        input[index].Add(new List<int>());
        String[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string s in parts)
        {
            input[index][0].Add(int.Parse(s));
        }
        index++;
    }
    for (int i = 0; i < input.Count; i++)
    {
        bool done = false;
        int j = 0;
        int diffcount = 1;
        while (!done)
        {
            done = true;
            input[i].Add(new List<int>());
            for (j = 0; j < input[i][diffcount - 1].Count - 1; j++)
            {
                int diff = input[i][diffcount - 1][j + 1] - input[i][diffcount - 1][j];
                input[i][diffcount].Add(diff);
                //Console.Write(diff + " ");
                if (diff != 0) done = false;
            }
            //Console.WriteLine();
            if (input[i][diffcount].Count < 2)
            {
                Console.WriteLine("Error - diffs don't converge");
                break;
            }
            diffcount++;
        }
        diffcount--;
        int temp = 0;
        for (int k = diffcount - 1; k >= 0; k--)
        {
            temp = input[i][k][0] - temp;
        }
        result += temp;
        Console.WriteLine(temp);

    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();