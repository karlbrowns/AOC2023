using System.Text.RegularExpressions;


void P1()
{
    String data = "input.txt";
    int result = 0;

    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(':', ';');
        if (parts.Length>0)
        {
            string gamebit = parts[0].Substring(4);
            int game = int.Parse(gamebit);
            int red, green, blue;
            int subparts = 1;
            red = green = blue = 0;
            while (subparts < parts.Length)
            {
                string[] substrings = parts[subparts].Split(',');
                foreach (string substring in substrings)
                {
                    string[] numbers = substring.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    int num = int.Parse(numbers[0]);
                    switch(numbers[1])
                    {
                        case "red": red = num;
                            break;
                        case "green": green = num;
                            break;
                        case "blue": blue = num;
                            break;
                    }
                }
                if ((red > 12) || (green > 13) || (blue > 14))
                {
                    break;
                }
                subparts++;
            }
            if ((red <= 12) && (green <= 13) && (blue <= 14)) result += game;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    String data = "input.txt";
    int result = 0;

    foreach (string line in System.IO.File.ReadLines(data))
    {
        string[] parts = line.Split(':', ';');
        if (parts.Length > 0)
        {
            string gamebit = parts[0].Substring(4);
            int game = int.Parse(gamebit);
            int red, green, blue;
            int maxred, maxgreen, maxblue;
            int subparts = 1;
            red = green = blue = 0;
            maxred = maxgreen = maxblue = 0;
            while (subparts < parts.Length)
            {
                string[] substrings = parts[subparts].Split(',');
                foreach (string substring in substrings)
                {
                    string[] numbers = substring.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    int num = int.Parse(numbers[0]);
                    switch (numbers[1])
                    {
                        case "red":
                            red = num;
                            break;
                        case "green":
                            green = num;
                            break;
                        case "blue":
                            blue = num;
                            break;
                    }
                }
                if (red > maxred) maxred = red;
                if (green > maxgreen) maxgreen = green;
                if (blue > maxblue) maxblue = blue;
                
                subparts++;
            }
            result += maxred * maxgreen * maxblue;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();