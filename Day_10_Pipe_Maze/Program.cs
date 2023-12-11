using System.Text.RegularExpressions;
using System.Xml.Schema;

// Maze
// Left = 1, Right = 2, Up = 4, Down = 8
// S = start       = -1
// | = up/down     = 12
// L = up/right    = 6
// J = up/left     = 5
// F = down/right  = 10
// 7 = down/left   = 9
// - = left/right  = 3
// . = nothing     = 0

void P1()
{
    int result = 0;
    int index = 0;
    int xpos = -1;
    int ypos = -1;
    String data = "input.txt";
    List<int[]> maze = new List<int[]>();
    List<int[]> length = new List<int[]>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        maze.Add(new int[line.Length]);
        length.Add(new int[line.Length]);
        for (int i=0; i<line.Length; i++)
        {
            length[index][i] = 0;
            switch (line[i])
            {
                case 'S':
                    maze[index][i] = -1;
                    xpos = i;
                    ypos = index;
                    break;

                case '|':
                    maze[index][i] = 12; break;
                case 'L':
                    maze[index][i] = 6; break;
                case 'J':
                    maze[index][i] = 5; break;
                case 'F':
                    maze[index][i] = 10; break;
                case '7':
                    maze[index][i] = 9; break;
                case '-':
                    maze[index][i] = 3; break;
                case '.':
                    maze[index][i] = 0; break;
                default:
                    maze[index][i] = 0; break;
            }
        }
        index++;
    }
    int x, y;
    x = xpos; y = ypos;
    int start = 0;
    if ((x > 0) && ((maze[y][x - 1] & 2) == 2)) start += 1;
    if ((y > 0) && ((maze[y - 1][x] & 8) == 8)) start += 4;
    if ((x < maze[y].Length - 1) && ((maze[y][x + 1] & 1)== 1)) start += 2;
    if ((y < maze.Count - 1) && ((maze[y + 1][x] & 4)==4 )) start += 8;
    maze[y][x] = start;
    int len = 0;
    int prevdir = 0;
    do
    {
        length[x][y] = len++;
        if (prevdir == 0)
        {
            if ((maze[y][x] & 1) == 1) { x--; prevdir = 1; }
            else if ((maze[y][x] & 2) == 2) { x++; prevdir = 2; }
            else if ((maze[y][x] & 4) == 4) { y--; prevdir = 4; }
            else if ((maze[y][x] & 8) == 8) { y++; prevdir = 8; }
        }
        else
        {
            if ((prevdir != 2) && ((maze[y][x] & 1) == 1)) { x--; prevdir = 1; }
            else if ((prevdir != 1) && ((maze[y][x] & 2) == 2)) { x++; prevdir = 2; }
            else if ((prevdir != 8) && ((maze[y][x] & 4) == 4)) { y--; prevdir = 4; }
            else if ((prevdir != 4) && ((maze[y][x] & 8) == 8)) { y++; prevdir = 8; }

        }
    }
    while ((x != xpos) || (y != ypos));
    Console.WriteLine(len);
    result = len / 2;
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