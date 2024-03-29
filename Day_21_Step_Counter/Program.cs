﻿using System.Text.RegularExpressions;
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
List<Point> find_neighbours2(List<Point> occupied, List<List<int>> map)
{
    int width = map[0].Count;
    int height = map.Count;
    List<Point> new_points = new List<Point>();
    foreach (Point p in occupied)
    {
        int x = p.X;
        int y = p.Y;
        int testx = ((x - 1) % width + width) % width;
        if (map[y][testx] == 0) { new_points.Add(new Point(testx, y)); }
        testx = (x+1) % width;
        if (map[y][testx] == 0) { new_points.Add(new Point(testx, y)); }
        int testy = ((y - 1) % height + height) % height;
        if ((map[testy][x] == 0)) { new_points.Add(new Point(x, y - 1)); }
        testy = (y + 1) % height;
        if ((map[testy][x] == 0)) { new_points.Add(new Point(x, y + 1)); }

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
    while (steps<150)
    {
        points = find_neighbours (points, map);
        steps++;
    }
    for (y = 0; y<map.Count ; y++)
    {
        for (x = 0; x < map[0].Count ; x++)
        {
            if (points.Contains(new Point(x, y))) Console.Write('O');
            else if (map[y][x] == -1) Console.Write('#');
            else Console.Write('.');
        }
        Console.WriteLine();
    }
    Console.WriteLine(points.Count);
    Console.ReadLine();
}

//7495 for the complete grid.
void P2()
{
    int result = 0;
    int index = 0;
    int x = 0, y = 0;
    List<List<int>> map = new List<List<int>>();
    String data = "inputtst.txt";
    foreach (string line in System.IO.File.ReadLines(data))
    {
        map.Add(new List<int>());
        foreach (char c in line)
        {
            if (c == '.') map.Last().Add(0);
            if (c == '#') map.Last().Add(-1);
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
    List<List<List<Point>>> fullpoints = new List<List<List<Point>>> ();
    points.Add(new Point(startx, starty));
    for (y = 0; y < map.Count; y++)
    {
        fullpoints.Add(new List<List<Point>>());
        for (x = 0; x < map[0].Count; x++)
        {
            List<Point> p = new List<Point>();
            p.Add(new Point(x, y));
            p = find_neighbours2(p, map);
            fullpoints[y].Add(p);
        }
    }
    points.Clear();
    points.Add(new Point(startx, starty));
    steps = 26501365;
 //   for (steps = 0; steps < 26501365; steps++)
    {
        HashSet<Point> newpoints = new HashSet<Point>();
        foreach (Point p in points)
        {
            List<Point> p1 = fullpoints[p.Y][p.X];
            newpoints.UnionWith(p1);
        }
        points = newpoints.ToList();
    }
    Console.WriteLine(points.Count);
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
