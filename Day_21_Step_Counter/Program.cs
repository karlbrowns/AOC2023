using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing;

List<Point> find_neighbours(List<Point> occupied, List<List<int>> map)
{
    List<Point> new_points = new List<Point>();
    foreach (Point p in occupied)
    {
        int x = p.X;
        int y = p.Y;
        if ((x>0) && map[y][x-1]==0) { new_points.Add(new Point(x-1,y)); }
        if ((x < map[0].Count-1) && map[y][x+1]==0) { new_points.Add(new Point(x + 1, y)); }
        if ((y>0) && (map[y - 1][x]==0)) { new_points.Add(new Point(x, y - 1)); }
        if ((y < map.Count - 1) && (map[y + 1][x] == 0)) { new_points.Add(new Point(x, y + 1)); }

    }
    new_points = new_points.Distinct().ToList();
    return new_points;
}
void P1()
{
    int result = 0;
    int index = 0;
    int x=0, y=0;
    List<List<int>> map = new List<List<int>>();
    String data = "input.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        foreach (char c in line)
        {
            if (c == '.') map.Last().Add(0);
            if (c=='#') map.Last().Add(-1);
            if (c == 'S')
            {
                map.Last().Add(0);
                y = index;
                x = map.Last().Count - 1;
            }

        }
        index++;
    }
    int startx = x;
    int starty = y;
    int steps = 0;
    List<Point> points = new List<Point>();
    points.Add(new Point(startx, starty));
    while (steps<64)
    {
        points = find_neighbours (points, map);
        steps++;
    }
    Console.WriteLine(points.Count);
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

Stopwatch t = new Stopwatch();
t.Start();
P1();
t.Stop();
Console.WriteLine("P1 took " + t.ElapsedMilliseconds/1000.0 + " seconds");
t.Restart();
P2();
t.Stop();
Console.WriteLine("P2 took " + t.ElapsedMilliseconds / 1000.0 + " seconds");
