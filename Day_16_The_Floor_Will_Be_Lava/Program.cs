using System.Text.RegularExpressions;
using System.Diagnostics;


void P1()
{
    long result = 0;
    int index = 0;
    int cell = 0;
    String data = "input.txt";
    List<List<char>> map = new List<List<char>>();
    List<List<int>> visited = new List<List<int>>();
    List<List<List<int>>> all_visited = new List<List<List<int>>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<char>());
        visited.Add(new List<int>());
        foreach (char c in line)
        {
            map[index].Add(c);
            visited[index].Add(0);
        }
        index++;
    }
    int x, y;
    x = 0;
    y = 0;
    int dirn = 1;   // 1 = R, 2 = L, 4 = D, 8 = U, 0=dead
    List<Beam> beams = new List<Beam>();
    Beam temp = new Beam();
    temp.x = x;
    temp.y = y;
    temp.dirn = dirn;
    beams.Add(temp);
    //add_visited(all_visited, map.Count, map[0].Count);
    int beams_stopped = 0;
    while (beams_stopped < beams.Count)
    {
        for (int i=0; i<beams.Count ; i++)
        {
            Beam beam = beams[i];
            if (beam.dirn > 0)
            {
                if ((visited[beam.y][beam.x] & beam.dirn) != 0) beam.dirn = 0;
                visited[beam.y][beam.x] += beam.dirn;
                dirn = beam.dirn;
                switch (dirn)
                {
                    case 1:
                        switch (map[beam.y][beam.x])
                        {
                            case '.':
                                beam.x++;
                                if (beam.x >= map[0].Count) beam.dirn = 0;
                                break;
                            case '\\':
                                beam.dirn = 4;
                                beam.y++;
                                if (beam.y >= map.Count) beam.dirn = 0;
                                break;
                            case '/':
                                beam.dirn = 8;
                                beam.y--;
                                if (beam.y < 0) beam.dirn = 0;
                                break;
                            case '|':
                                temp = new Beam();
                                temp.x = beam.x;
                                temp.y = beam.y;
                                temp.dirn = 4;
                                beam.dirn = 8;
                                beam.y--;
                                if (beam.y < 0)
                                {
                                    beam.dirn = 4;
                                    beam.y += 2;
                                }
                                else
                                {
                                    beams.Add(temp);
                                }
                                break;
                            case '-':
                                beam.x++;
                                if (beam.x >= map[0].Count) beam.dirn = 0;
                                break;
                        }
                        break;
                    case 2:
                        switch (map[beam.y][beam.x])
                        {
                            case '.':
                                beam.x--;
                                if (beam.x < 0) beam.dirn = 0;
                                break;
                            case '\\':
                                beam.dirn = 8;
                                beam.y--;
                                if (beam.y < 0) beam.dirn = 0;
                                break;
                            case '/':
                                beam.dirn = 4;
                                beam.y++;
                                if (beam.y >= map.Count) beam.dirn = 0;
                                break;
                            case '|':
                                temp = new Beam();
                                temp.x = beam.x;
                                temp.y = beam.y;
                                temp.dirn = 4;
                                beam.dirn = 8;
                                beam.y--;
                                if (beam.y < 0)
                                {
                                    beam.dirn = 4;
                                    beam.y += 2;
                                }
                                else
                                {
                                    beams.Add(temp);
                                }
                                break;
                            case '-':
                                beam.x--;
                                if (beam.x < 0) beam.dirn = 0;
                                break;
                        }
                        break;
                    case 4:
                        switch (map[beam.y][beam.x])
                        {
                            case '.':
                                beam.y++;
                                if (beam.y >= map.Count) beam.dirn = 0;
                                break;
                            case '\\':
                                beam.dirn = 1;
                                beam.x++;
                                if (beam.x >= map[0].Count) beam.dirn = 0;
                                break;
                            case '/':
                                beam.dirn = 2;
                                beam.x--;
                                if (beam.x < 0) beam.dirn = 0;
                                break;
                            case '|':
                                beam.y++;
                                if (beam.y >= map.Count) beam.dirn = 0;
                                break;
                            case '-':
                                temp = new Beam();
                                temp.x = beam.x;
                                temp.y = beam.y;
                                temp.dirn = 1;
                                beam.dirn = 2;
                                beam.x--;
                                if (beam.x < 0)
                                {
                                    beam.dirn = 1;
                                    beam.x += 2;
                                }
                                else
                                {
                                    beams.Add(temp);
                                }
                                break;
                        }
                        break;
                    case 8:
                        switch (map[beam.y][beam.x])
                        {
                            case '.':
                                beam.y--;
                                if (beam.y < 0) beam.dirn = 0;
                                break;
                            case '\\':
                                beam.dirn = 2;
                                beam.x--;
                                if (beam.x < 0) beam.dirn = 0;
                                break;
                            case '/':
                                beam.dirn = 1;
                                beam.x++;
                                if (beam.x >= map[0].Count) beam.dirn = 0;
                                break;
                            case '|':
                                beam.y--;
                                if (beam.y < 0) beam.dirn = 0;
                                break;
                            case '-':
                                temp = new Beam();
                                temp.x = beam.x;
                                temp.y = beam.y;
                                temp.dirn = 1;
                                beam.dirn = 2;
                                beam.x--;
                                if (beam.x < 0)
                                {
                                    beam.dirn = 1;
                                    beam.x += 2;
                                }
                                else
                                {
                                    beams.Add(temp);
                                }
                                break;
                        }
                        break;
                }
                beams[i] = beam;
                if (beam.dirn == 0) beams_stopped++;

            }
        }
    }
    for (int i=0; i<visited.Count; i++)
    {
        for (int j= 0; j < visited[0].Count; j++)
        {
            if (visited[i][j] > 0) result++;
        }
    }
    Console.WriteLine(result);
    Console.ReadLine();
}

void P2()
{
    long result = 0;
    long maxresult = 0;
    int index = 0;
    int cell = 0;
    String data = "input.txt";
    List<List<char>> map = new List<List<char>>();
    List<List<int>> visited = new List<List<int>>();
    List<List<List<int>>> all_visited = new List<List<List<int>>>();
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<char>());
        visited.Add(new List<int>());
        foreach (char c in line)
        {
            map[index].Add(c);
            visited[index].Add(0);
        }
        index++;
    }
    int x, y;
    x = 0;
    y = 0;
    int dirn = 1;   // 1 = R, 2 = L, 4 = D, 8 = U, 0=dead
    for (int ii = 0; ii<map.Count; ii++)
    {
        for (int i=0; i<map.Count; i++)
        {
            for (int j=0; j < map[0].Count; j++)
            {
                visited[i][j] = 0;
            }
        }
        int beams_stopped = 0;
        result = 0;
        List<Beam> beams = new List<Beam>();
        Beam temp = new Beam();
        temp.x = 0;
        temp.y = ii;
        temp.dirn = 1;
        beams.Add(temp);
        while (beams_stopped < beams.Count)
        {
            for (int i = 0; i < beams.Count; i++)
            {
                Beam beam = beams[i];
                if (beam.dirn > 0)
                {
                    if ((visited[beam.y][beam.x] & beam.dirn) != 0) beam.dirn = 0;
                    visited[beam.y][beam.x] += beam.dirn;
                    dirn = beam.dirn;
                    switch (dirn)
                    {
                        case 1:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 2:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 4:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                        case 8:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                    }
                    beams[i] = beam;
                    if (beam.dirn == 0) beams_stopped++;

                }
            }
        }
        for (int i = 0; i < visited.Count; i++)
        {
            for (int j = 0; j < visited[0].Count; j++)
            {
                if (visited[i][j] > 0) result++;
            }
        }
        if (result > maxresult) maxresult = result;
    }
    for (int ii = 0; ii < map.Count; ii++)
    {
        int beams_stopped = 0;
        result = 0;
        List<Beam> beams = new List<Beam>();
        Beam temp = new Beam();
        temp.x = map[0].Count-1;
        temp.y = ii;
        temp.dirn = 2;
        beams.Add(temp);
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[0].Count; j++)
            {
                visited[i][j] = 0;
            }
        }
        while (beams_stopped < beams.Count)
        {
            for (int i = 0; i < beams.Count; i++)
            {
                Beam beam = beams[i];
                if (beam.dirn > 0)
                {
                    if ((visited[beam.y][beam.x] & beam.dirn) != 0) beam.dirn = 0;
                    visited[beam.y][beam.x] += beam.dirn;
                    dirn = beam.dirn;
                    switch (dirn)
                    {
                        case 1:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 2:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 4:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                        case 8:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                    }
                    beams[i] = beam;
                    if (beam.dirn == 0) beams_stopped++;

                }
            }
        }
        for (int i = 0; i < visited.Count; i++)
        {
            for (int j = 0; j < visited[0].Count; j++)
            {
                if (visited[i][j] > 0) result++;
            }
        }
        if (result > maxresult) maxresult = result;
    }
    for (int ii = 0; ii < map[0].Count; ii++)
    {
        int beams_stopped = 0;
        result = 0;
        List<Beam> beams = new List<Beam>();
        Beam temp = new Beam();
        temp.x = ii;
        temp.y = 0;
        temp.dirn = 4;
        beams.Add(temp);
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[0].Count; j++)
            {
                visited[i][j] = 0;
            }
        }
        while (beams_stopped < beams.Count)
        {
            for (int i = 0; i < beams.Count; i++)
            {
                Beam beam = beams[i];
                if (beam.dirn > 0)
                {
                    if ((visited[beam.y][beam.x] & beam.dirn) != 0) beam.dirn = 0;
                    visited[beam.y][beam.x] += beam.dirn;
                    dirn = beam.dirn;
                    switch (dirn)
                    {
                        case 1:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 2:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 4:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                        case 8:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                    }
                    beams[i] = beam;
                    if (beam.dirn == 0) beams_stopped++;

                }
            }
        }
        for (int i = 0; i < visited.Count; i++)
        {
            for (int j = 0; j < visited[0].Count; j++)
            {
                if (visited[i][j] > 0) result++;
            }
        }
        if (result > maxresult) maxresult = result;
    }
    for (int ii = 0; ii < map[0].Count; ii++)
    {
        int beams_stopped = 0;
        result = 0;
        List<Beam> beams = new List<Beam>();
        Beam temp = new Beam();
        temp.x = ii;
        temp.y = map.Count-1;
        temp.dirn = 8;
        beams.Add(temp);
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[0].Count; j++)
            {
                visited[i][j] = 0;
            }
        }
        while (beams_stopped < beams.Count)
        {
            for (int i = 0; i < beams.Count; i++)
            {
                Beam beam = beams[i];
                if (beam.dirn > 0)
                {
                    if ((visited[beam.y][beam.x] & beam.dirn) != 0) beam.dirn = 0;
                    visited[beam.y][beam.x] += beam.dirn;
                    dirn = beam.dirn;
                    switch (dirn)
                    {
                        case 1:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 2:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 4;
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 4;
                                    beam.dirn = 8;
                                    beam.y--;
                                    if (beam.y < 0)
                                    {
                                        beam.dirn = 4;
                                        beam.y += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                                case '-':
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                            }
                            break;
                        case 4:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y++;
                                    if (beam.y >= map.Count) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                        case 8:
                            switch (map[beam.y][beam.x])
                            {
                                case '.':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '\\':
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0) beam.dirn = 0;
                                    break;
                                case '/':
                                    beam.dirn = 1;
                                    beam.x++;
                                    if (beam.x >= map[0].Count) beam.dirn = 0;
                                    break;
                                case '|':
                                    beam.y--;
                                    if (beam.y < 0) beam.dirn = 0;
                                    break;
                                case '-':
                                    temp = new Beam();
                                    temp.x = beam.x;
                                    temp.y = beam.y;
                                    temp.dirn = 1;
                                    beam.dirn = 2;
                                    beam.x--;
                                    if (beam.x < 0)
                                    {
                                        beam.dirn = 1;
                                        beam.x += 2;
                                    }
                                    else
                                    {
                                        beams.Add(temp);
                                    }
                                    break;
                            }
                            break;
                    }
                    beams[i] = beam;
                    if (beam.dirn == 0) beams_stopped++;

                }
            }
        }
        for (int i = 0; i < visited.Count; i++)
        {
            for (int j = 0; j < visited[0].Count; j++)
            {
                if (visited[i][j] > 0) result++;
            }
        }
        if (result > maxresult) maxresult = result;
    }
    Console.WriteLine(maxresult);
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

struct Beam
{
    public int x;
    public int y;
    public int dirn;
}