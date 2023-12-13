using System.Security.Cryptography.X509Certificates;
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
    int xpos = -1;
    int ypos = -1;
    String data = "input.txt";
    List<int[]> maze = new List<int[]>();
    List<int[]> length = new List<int[]>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        maze.Add(new int[line.Length]);
        length.Add(new int[line.Length]);
        for (int i = 0; i < line.Length; i++)
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
    if ((x < maze[y].Length - 1) && ((maze[y][x + 1] & 1) == 1)) start += 2;
    if ((y < maze.Count - 1) && ((maze[y + 1][x] & 4) == 4)) start += 8;
    maze[y][x] = start;
    int len = 0;
    int prevdir = 0;
    do
    {
        length[y][x] = maze[y][x];  len++;
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
    int edges = 0;
    int newedge = 0;
    bool edge = false;
    bool inside = false;
    int edgedge = 0;
    for (y = 0; y < length.Count; y++)
    {
        for (x = 0; x < length[0].Length; x++)
        {
            //if (length[y][x] == -1) Console.Write('I');
            if (length[y][x] == 3) Console.Write('─');
            if (length[y][x] == 12) Console.Write('│');
            if (length[y][x] == 6) Console.Write('└');
            if (length[y][x] == 5) Console.Write('┘');
            if (length[y][x] == 10) Console.Write('┌');
            if (length[y][x] == 9) Console.Write('┐');
            if (length[y][x] == 0) Console.Write('.');
        }
        Console.WriteLine();
    }
    edges = 0;
    edge = false;
    prevdir = 0;
    edgedge = 0;
    ypos = 45;
    y = ypos;
    x = 0;
    int outside = 0;
    int steps = 0;
    while (length[y][x] == 0) x++; // find first edge from outside
    xpos = x;
    do
    {
        steps++;
        if ((x == 10) && (y == 3))
            Console.WriteLine("debug here");
        if (prevdir == 0)
        {
            switch (length[y][x])
            {
                case 12:
                    outside = 1;
                    if ((x < length[y].Length - 1) && (length[y][x + 1] == 0)) length[y][x + 1] = -1;
                    y++;    // going down
                    prevdir = 8;
                    break;
                case 10:
                    outside = 5;
                    if ((x < length[y].Length - 1) && (y < length.Count - 1) && (length[y + 1][x + 1] == 0)) length[y + 1][x + 1] = -1;
                    x++;
                    prevdir = 2;
                    break;
                case 6:
                    outside = 9;
                    if ((x < length[y].Length - 1) && (y > 0) && (length[y - 1][x + 1] == 0)) length[y - 1][x + 1] = -1;
                    x++;
                    prevdir = 2;
                    break;
                default:
                    Console.WriteLine("Shouldn't get here"); break;
            }
        }
        else
        {
            switch (prevdir)
            {
                case 2:
                    outside &= 12;
                    switch (length[y][x])
                    {
                        case 3:
                            if ((outside == 4) && (y < length.Count - 1) && (length[y + 1][x] == 0)) length[y + 1][x] = -1;
                            if ((outside == 8) && (y > 0) && (length[y - 1][x] == 0)) length[y - 1][x] = -1;
                            x++;
                            prevdir = 2;
                            break;
                        case 5:
                            if (outside == 8) outside += 2;
                            else outside += 1;
                            if (outside == 5)
                            {
                                if ((x < length[y].Length - 1) && (y < length.Count - 1) && (length[y + 1][x + 1] == 0)) length[y + 1][x + 1] = -1;
                                if ((y < length.Count - 1) && (length[y + 1][x] == 0)) length[y + 1][x] = -1;
                                if ((x < length[y].Length -1) && (length[y][x+1]==0)) length[y][x+1] = -1;
                            } 
                            if ((outside == 10) && (x > 0) && (y>0) && (length[y - 1][x-1] == 0)) length[y - 1][x - 1] = -1;
                            y--;
                            prevdir = 4;
                            break;
                        case 9:
                            if (outside == 8) outside += 1;
                            else outside += 2;
                            if ((outside == 6) && (x > 0) && (y < length.Count - 1) && (length[y + 1][x - 1] == 0)) length[y + 1][x - 1] = -1;
                            if (outside == 9)
                            {
                                if ((x < length[y].Length - 1) && (y > 0) && (length[y - 1][x + 1] == 0)) length[y - 1][x + 1] = -1;
                                if ((x < length[y].Length - 1) && (length[y][x + 1] == 0)) length[y][x + 1] = -1;
                                if ((y > 0) && (length[y - 1][x]==0)) length[y - 1][x] = -1;
                            }
                            y++;
                            prevdir = 8;
                            break;
                    }
                    break;
                case 4: // we went up
                    outside &= 3;
                    switch (length[y][x])
                    {
                        case 12:
                            if ((outside == 1) && (x < length[y].Length - 1) && (length[y][x + 1] == 0)) length[y][x + 1] = -1;
                            if ((outside == 2) && (x > 0) && (length[y][x - 1] == 0)) length[y][x - 1] = -1;
                            y--;
                            prevdir = 4;
                            break;
                        case 9:
                            if (outside == 1) outside += 8;
                            else outside += 4;
                            if (outside == 9)
                            {
                                if ((x < length[y].Length - 1) && (y > 0) && (length[y - 1][x + 1] == 0)) length[y - 1][x + 1] = -1;
                                if ((x < length[y].Length - 1) && (length[y][x + 1] == 0)) length[y][x + 1] = -1;
                                if ((y > 0) && (length[y - 1][x]==0)) length[y - 1][x] = -1;
                            }
                            if ((outside==6) && (x > 0) && (y < length.Count - 1) && (length[y + 1][x - 1] == 0)) length[y + 1][x - 1] = -1;
                            x--;
                            prevdir = 1;
                            break;
                        case 10:
                            if (outside == 1) outside += 4;
                            else outside += 8;
                            if ((outside == 5) && (x < length[y].Length - 1) && (y < length.Count - 1) && (length[y + 1][x + 1] == 0)) length[y + 1][x + 1] = -1;
                            if (outside == 10) {
                                if ((x > 0) && (y > 0) && (length[y - 1][x - 1] == 0)) length[y - 1][x - 1] = -1;
                                if ((x > 0) && (length[y][x - 1] == 0)) length[y][x - 1] = -1;
                                if ((y > 0) && (length[y - 1][x]==0)) length[y - 1][x] = -1;
                            }
                            x++;
                            prevdir = 2;
                            break;
                    }
                    break;
                case 8: // we went down
                    outside &= 3;
                    switch (length[y][x])
                    {
                        case 12:
                            if ((outside == 1) && (x < length[y].Length - 1) && (length[y][x + 1] == 0)) length[y][x + 1] = -1;
                            if ((outside == 2) && (x > 0) && (length[y][x - 1] == 0)) length[y][x - 1] = -1;
                            y++;
                            prevdir = 8;
                            break;
                        case 5:
                            if (outside == 1) outside += 4;
                            else outside += 8;
                            if (outside == 5)
                            {
                                if ((x < length[y].Length - 1) && (y < length.Count - 1) && (length[y + 1][x + 1] == 0)) length[y + 1][x + 1] = -1;
                                if ((y < length.Count - 1) && (length[y + 1][x] == 0)) length[y + 1][x] = -1;
                                if ((x < length[y].Length - 1) && (length[y][x + 1] == 0)) length[y][x + 1] = -1;
                            }
                            if ((outside == 10) && (x > 0) && (y > 0) && (length[y - 1][x - 1] == 0)) length[y - 1][x - 1] = -1;
                            x--;
                            prevdir = 1;
                            break;
                        case 6:
                            if (outside == 1) outside += 8;
                            else outside += 4;
                            if ((outside == 9) && (x < length[y].Length - 1) && (y > 0) && (length[y - 1][x + 1] == 0)) length[y - 1][x + 1] = -1;
                            if (outside == 6) {
                                if ((x > 0) && (y < length.Count - 1) && (length[y + 1][x - 1] == 0)) length[y + 1][x - 1] = -1;
                                if ((x > 0) && (length[y][x - 1] == 0)) length[y][x - 1] = -1;
                                if ((y < length.Count - 1) && (length[y + 1][x]==0)) length[y + 1][x] = -1;
                            }
                            x++;
                            prevdir = 2;
                            break;
                    }
                    break;
                case 1: // we went left
                    outside &= 12;
                    switch (length[y][x])
                    {
                        case 3:
                            if ((outside == 4) && (y < length.Count - 1) && (length[y + 1][x] == 0)) length[y + 1][x] = -1;
                            if ((outside == 8) && (y > 0) && (length[y - 1][x] == 0)) length[y - 1][x] = -1;
                            x--;
                            prevdir = 1;
                            break;
                        case 6:
                            if (outside == 4) outside += 2;
                            else outside += 1;
                            if ((outside == 9) && (x < length[y].Length - 1) && (y > 0) && (length[y - 1][x + 1] == 0)) length[y - 1][x + 1] = -1;
                            if (outside == 6)
                            {
                                if ((x > 0) && (y < length.Count - 1) && (length[y + 1][x - 1] == 0)) length[y + 1][x - 1] = -1;
                                if ((x > 0) && (length[y][x - 1] == 0)) length[y][x - 1] = -1;
                                if ((y < length.Count - 1) && (length[y + 1][x] == 0)) length[y + 1][x] = -1;
                            }
                            y--;
                            prevdir = 4;
                            break;
                        case 10:
                            if (outside == 4) outside += 1;
                            else outside += 2;
                            if ((outside == 5) && (x < length[y].Length - 1) && (y < length.Count - 1) && (length[y + 1][x + 1] == 0)) length[y + 1][x + 1] = -1;
                            if (outside == 10)
                            {
                                if ((x > 0) && (y > 0) && (length[y - 1][x - 1] == 0)) length[y - 1][x - 1] = -1;
                                if ((x > 0) && (length[y][x - 1] == 0)) length[y][x - 1] = -1;
                                if ((y > 0) && (length[y - 1][x] == 0)) length[y - 1][x] = -1;
                            }
                            y++;
                            prevdir = 8;
                            break;

                    }
                    break;
            }
        }
    } while ((x != xpos) || (y != ypos));
    bool found = true;
    while (found) {
        found = false;
        for (y = 1; y < length.Count; y++)
        {
            for (x = 1; x < length[y].Length; x++)
            {
                if (length[y][x] == -1)
                {
                    if ((x > 0) && (length[y][x - 1] == 0)) { length[y][x - 1] = -1; found = true; }
                    if ((x < length[y].Length - 1) && (length[y][x + 1] == 0)) { length[y][x + 1] = -1; found = true; }
                    if ((y > 0) && (length[y - 1][x] == 0)) { length[y - 1][x] = -1; found = true; }
                    if ((y < length.Count - 1) && (length[y + 1][x] == 0)) { length[y + 1][x] = -1; found = true; }
                }
            }
        }
    }
    int count = 0;
    for ( y = 0; y<length.Count; y++)
    {
        for (x = 0; x < length[0].Length; x++)
        {
            if (length[y][x] == -1)
            {
                Console.Write('I');
                count++;
            }
            if (length[y][x] == 0) Console.Write('.');
            if (length[y][x] == 3) Console.Write('─');
            if (length[y][x] == 12) Console.Write('│');
            if (length[y][x] == 6) Console.Write('└');
            if (length[y][x] == 5) Console.Write('┘');
            if (length[y][x] == 10) Console.Write('┌');
            if (length[y][x] == 9) Console.Write('┐');
        }
        Console.WriteLine();
    }
    Console.WriteLine(steps);
    Console.WriteLine(count);
    Console.ReadLine();
}

P1();
P2();