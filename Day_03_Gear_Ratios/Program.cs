using System.Runtime.InteropServices.JavaScript;
using System.Text.RegularExpressions;


void P1()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<List<int>> grid = new List<List<int>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        grid.Add(new List<int>());
        for (int i = 0; i<line.Length; i++)
        {
            grid[index].Add(line[i]);

        }
        index++;
    }
    for (int i=0; i<grid.Count; i++)
    {
        for (int j=0; j < grid[i].Count; j++)
        {
            if (grid[i][j] == '.') grid[i][j] = 0;

        }
    }
    for (int i = 0; i < grid.Count; i++)
    {
        for (int j = 0; j < grid[i].Count; j++)
        {
            if ((grid[i][j] > 0) && ((grid[i][j] < 48) || ((grid[i][j]>57) && (grid[i][j]<128))))
            {
                // we have a symbol - so mark diagonal neighbours
                if ((i>0) && (j>0))
                {
                    if ((grid[i - 1][j - 1] >= 48) && (grid[i - 1][j - 1] <= 57)) grid[i - 1][j - 1] += 128; 
                }
                if (j>0)
                {
                    if ((grid[i][j - 1] >= 48) && (grid[i][j - 1] <= 57)) grid[i][j - 1] += 128;

                }
                if ((i+1<grid.Count) && (j>0))
                {
                    if ((grid[i + 1][j - 1] >= 48) && (grid[i + 1][j - 1] <= 57)) grid[i + 1][j - 1] += 128;

                }
                if ((i > 0) )
                {
                    if ((grid[i - 1][j] >= 48) && (grid[i - 1][j] <= 57)) grid[i - 1][j] += 128;
                }
                if ((i + 1 < grid.Count))
                {
                    if ((grid[i + 1][j] >= 48) && (grid[i + 1][j] <= 57)) grid[i + 1][j] += 128;

                }
                if ((i > 0) && (j < grid[i].Count))
                {
                    if ((grid[i - 1][j + 1] >= 48) && (grid[i - 1][j + 1] <= 57)) grid[i - 1][j + 1] += 128;
                }
                if (j < grid[i].Count)
                {
                    if ((grid[i][j + 1] >= 48) && (grid[i][j + 1] <= 57)) grid[i][j + 1] += 128;

                }
                if ((i + 1 < grid.Count) && (j < grid[i].Count))
                {
                    if ((grid[i + 1][j + 1] >= 48) && (grid[i + 1][j + 1] <= 57)) grid[i + 1][j + 1] += 128;

                }

            }

        }
    }
    Char[] temp = new Char[10];
    for (int i = 0; i < grid.Count; i++)
    {
        int digit = 0;
        bool isValid = false;
        if (i == 19)
            Console.Write("Line 20");
        for (int j = 0; j < grid[i].Count; j++)
        {
            if (((grid[i][j] & 0x7f)>=48) && ((grid[i][j] & 0x7f) <= 57))
            {
                // is digit
                temp[digit++] = (Char) (grid[i][j] & 0x7f);
                if (grid[i][j] > 128) isValid = true;
            }
            else
            {
                if (isValid)
                {
                    string temp2 = new string(temp);
                    result += int.Parse(temp2);
                    Console.Write(int.Parse(temp2));
                    Console.Write(',');
                }
                isValid = false;
                digit = 0;
                temp = new char[10];
            }
        }
        if (isValid)
        {
            string temp2 = new string(temp);
            result += int.Parse(temp2);
            Console.Write(int.Parse(temp2));
            Console.Write(',');
        }
        Console.WriteLine();
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    int result = 0;
    int index = 0;
    String data = "input.txt";
    List<List<int>> grid = new List<List<int>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        grid.Add(new List<int>());
        for (int i = 0; i < line.Length; i++)
        {
            grid[index].Add(line[i]);

        }
        index++;
    }
    for (int i = 0; i < grid.Count; i++)
    {
        for (int j = 0; j < grid[i].Count; j++)
        {
            if ((grid[i][j]!='*') && ((grid[i][j]<48) || (grid[i][j]>57))) grid[i][j] = 0;

        }
    }
    for (int i = 0; i < grid.Count; i++)
    {
        for (int j = 0; j < grid[i].Count; j++)
        {
            if ((grid[i][j] == '*'))
            {
                int neighbours = 0;
                int neighbour = 0;
                if ((i == 55) && (j == 120)) 
                    Console.WriteLine("test");
                // we have a * - count neighbours
                if ((i > 0) && (j > 0))
                {
                    if ((grid[i - 1][j - 1] >= 48) && (grid[i - 1][j - 1] <= 57))
                    {
                        neighbours = 1;
                        neighbour++;
                    }
                }
                if ((i > 0))
                {
                    if ((grid[i - 1][j] >= 48) && (grid[i - 1][j] <= 57))
                    {
                        neighbours += 2;
                        if ((neighbours & 1) == 0) neighbour++;
                    }

                }
                if ((j + 1 < grid[i].Count) && (i > 0))
                {
                    if ((grid[i - 1][j + 1] >= 48) && (grid[i - 1][j + 1] <= 57))
                    {
                        neighbours += 4;
                        if ((neighbours & 2) == 0) neighbour++;
                    }

                }
                if ((j > 0))
                {
                    if ((grid[i][j - 1] >= 48) && (grid[i - 1][j] <= 57))
                    {
                        neighbours += 8;
                        neighbour++;
                    }
                }
                if ((j + 1 < grid[i].Count))
                {
                    if ((grid[i][j + 1] >= 48) && (grid[i][j + 1] <= 57))
                    {
                        neighbours += 16;
                        neighbour++;
                    }

                }
                if ((j > 0) && (i + 1 < grid.Count))
                {
                    if ((grid[i + 1][j - 1] >= 48) && (grid[i + 1][j - 1] <= 57))
                    {
                        neighbours += 32;
                        neighbour++;
                    }
                }
                if ((i + 1 < grid.Count))
                {
                    if ((grid[i + 1][j] >= 48) && (grid[i + 1][j] <= 57))
                    {
                        neighbours += 64;
                        if ((neighbours & 32) == 0) neighbour++;
                    }
                }
                if ((i + 1 < grid.Count) && (j + 1 < grid[i].Count))
                {
                    if ((grid[i + 1][j + 1] >= 48) && (grid[i + 1][j + 1] <= 57))
                    {
                        neighbours += 128;
                        if ((neighbours & 64) == 0) neighbour++;
                    }
                }
                if (neighbour > 2)
                    Console.WriteLine(neighbour.ToString() + " " + neighbours.ToString());
                if (neighbour == 2)
                {
                    int temp1 = 0;
                    List<int> temp = new List<int>();
                    if (((neighbours & 1) == 1) && (neighbours & 2) == 0)
                    {
                        // number top left
                        temp1 = grid[i - 1][j - 1] - 48;
                        if ((j > 1) && (grid[i - 1][j - 2] >= 48)) temp1 += (grid[i - 1][j - 2] - 48) * 10;
                        if ((j > 2) && (grid[i - 1][j - 3] >= 48) && (grid[i - 1][j - 2] >= 48)) temp1 += (grid[i - 1][j - 3] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if (((neighbours & 3) == 3) & (neighbours & 4) == 0)
                    {
                        // number ends top middle 
                        temp1 = grid[i - 1][j] - 48;
                        if ((j > 0) && (grid[i - 1][j - 1] >= 48)) temp1 += (grid[i - 1][j - 1] - 48) * 10;
                        if ((j > 1) && (grid[i - 1][j - 2] >= 48) && (grid[i - 1][j - 1] >= 48)) temp1 += (grid[i - 1][j - 2] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if (((neighbours & 7) == 7))
                    {
                        // number ends right middle
                        temp1 = grid[i - 1][j + 1] - 48;
                        if ((j > -1) && (grid[i - 1][j] >= 48)) temp1 += (grid[i - 1][j - 0] - 48) * 10;
                        if ((j > 0) && (grid[i - 1][j - 1] >= 48) && (grid[i - 1][j] >= 48)) temp1 += (grid[i - 1][j - 1] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if (((neighbours & 7) == 6))
                    {
                        // number starts top middle
                        temp1 = grid[i - 1][j] - 48;
                        if ((j + 1 < grid[i - 1].Count) && (grid[i - 1][j + 1] >= 48)) temp1 = (temp1 * 10) + (grid[i - 1][j + 1] - 48);
                        if ((j + 2 < grid[i - 1].Count) && (grid[i - 1][j + 2] >= 48) && (grid[i - 1][j + 1] >= 48)) temp1 = (temp1 * 10) + (grid[i - 1][j + 2] - 48);
                        temp.Add(temp1);
                    }
                    if (((neighbours & 4) == 4) && ((neighbours & 2) == 0))
                    {
                        // number starts top right
                        temp1 = grid[i - 1][j + 1] - 48;
                        if ((j + 2 < grid[i - 1].Count) && (grid[i - 1][j + 2] >= 48)) temp1 = (temp1 * 10) + (grid[i - 1][j + 2] - 48);
                        if ((j + 3 < grid[i - 1].Count) && (grid[i - 1][j + 3] >= 48) && (grid[i - 1][j + 2] >= 48)) temp1 = (temp1 * 10) + (grid[i - 1][j + 3] - 48);
                        temp.Add(temp1);
                    }
                    if ((neighbours & 8) == 8)
                    {
                        // number ends middle left
                        temp1 = grid[i][j - 1] - 48;
                        if ((j > 1) && (grid[i][j - 2] >= 48)) temp1 += (grid[i][j - 2] - 48) * 10;
                        if ((j > 2) && (grid[i][j - 3] >= 48) && (grid[i][j - 2] >= 48)) temp1 += (grid[i][j - 3] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if ((neighbours & 16) == 16)
                    {
                        // number starts middle right
                        temp1 = grid[i][j + 1] - 48;
                        if ((j + 2 < grid[i].Count) && (grid[i][j + 2] >= 48)) temp1 = (temp1 * 10) + (grid[i][j + 2] - 48);
                        if ((j + 3 < grid[i].Count) && (grid[i][j + 3] >= 48) && (grid[i][j + 2] >= 48)) temp1 = (temp1 * 10) + (grid[i][j + 3] - 48);
                        temp.Add(temp1);
                    }
                    if ((neighbours & 96) == 32)
                    {
                        // number ends bottom left
                        temp1 = grid[i + 1][j - 1] - 48;
                        if ((j > 1) && (grid[i + 1][j - 2] >= 48)) temp1 += (grid[i + 1][j - 2] - 48) * 10;
                        if ((j > 2) && (grid[i + 1][j - 3] >= 48) && (grid[i + 1][j - 2] >= 48)) temp1 += (grid[i + 1][j - 3] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if (((neighbours & (32 + 64 + 128)) == 96) || ((neighbours & (32 + 64+ 128))==64))
                    {
                        // number ends bottom middle
                        temp1 = grid[i + 1][j] - 48;
                        if ((j > 0) && (grid[i + 1][j - 1] >= 48)) temp1 += (grid[i + 1][j - 1] - 48) * 10;
                        if ((j > 1) && (grid[i + 1][j - 2] >= 48) && (grid[i + 1][j - 1] >= 48)) temp1 += (grid[i + 1][j - 2] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if ((neighbours & (32 + 64 + 128)) == (32 + 64 + 128))
                    {
                        // number ends bottom right
                        temp1 = grid[i + 1][j + 1] - 48;
                        if ((j > -1) && (grid[i + 1][j - 0] >= 48)) temp1 += (grid[i + 1][j - 0] - 48) * 10;
                        if ((j > 0) && (grid[i + 1][j - 1] >= 48) && (grid[i + 1][j - 0] >= 48)) temp1 += (grid[i + 1][j - 1] - 48) * 100;
                        temp.Add(temp1);
                    }
                    if ((neighbours & (32 + 64 + 128)) == (64 + 128))
                    {
                        // number starts bottom middle
                        temp1 = grid[i + 1][j] - 48;
                        if ((j + 1 < grid[i + 1].Count) && (grid[i + 1][j + 1] >= 48)) temp1 = (temp1 * 10) + (grid[i + 1][j + 1] - 48);
                        if ((j + 2 < grid[i + 1].Count) && (grid[i + 1][j + 2] >= 48) && (grid[i + 1][j + 1] >= 48)) temp1 = (temp1 * 10) + (grid[i + 1][j + 2] - 48);
                        temp.Add(temp1);
                    }
                    if ((neighbours & (64 + 128)) == (128))
                    {
                        // number starts bottom right
                        temp1 = grid[i + 1][j + 1] - 48;
                        if ((j + 2 < grid[i + 1].Count) && (grid[i + 1][j + 2] >= 48)) temp1 = (temp1 * 10) + (grid[i + 1][j + 2] - 48);
                        if ((j + 3 < grid[i + 1].Count) && (grid[i + 1][j + 3] >= 48) && (grid[i + 1][j + 2] >= 48)) temp1 = (temp1 * 10) + (grid[i + 1][j + 3] - 48);
                        temp.Add(temp1);
                    }
                    if (temp.Count != 2) Console.WriteLine("error! " + temp.Count.ToString() + " " + neighbours.ToString() + " " + i.ToString() + " " + j.ToString());
                    else
                    {
                        result += temp[0] * temp[1];
                        Console.WriteLine(temp[0].ToString() + " " + temp[1].ToString());
                    }
                }

            }
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

P1();
P2();