using System.Diagnostics;

Console.WriteLine("Hello World!");
var points = new List<(int x, int y)>();
var folds = new List<(char axis, int position)>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    var cordinate = line.Split(",");
    var fold = line.Split("=");

    if (cordinate.Length == 2)
    {
        points.Add(new(int.Parse(cordinate[0]), int.Parse(cordinate[1])));
    }
    else if (fold.Length == 2)
    {
        folds.Add(new(fold[0].Last(), int.Parse(fold[1])));
    }
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
var max_x = points.Max(p => p.x);
var max_y = points.Max(p => p.y);

var map = new char[max_x + 1, max_y + 1];
for (int x = 0; x < map.GetLength(0); x++)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        map[x, y] = points.Any(p => p.x == x && p.y == y) ? '#' : '.';
    }
}
Print(map);

foreach (var fold in folds)
{    
    {
        char[,] newMap;
        if (fold.axis == 'y')
        {
            //Cut y 
            int mid = (map.GetLength(1) / 2);
            if (fold.position <= mid)
            {
                newMap = new char[map.GetLength(0), map.GetLength(1) - (fold.position + 1)];
            }
            else
            {
                newMap = new char[map.GetLength(0), fold.position];
            }
            //Lets assume perfect folds for now
            for (int x = 0; x < newMap.GetLength(0); x++)
            {
                for (int y = 0; y < fold.position; y++)
                {
                    newMap[x, y] = map[x, y];
                }
                for (int y = 0; y < mid; y++)
                {
                    newMap[x, y] = newMap[x, y] == '#' ? '#' : map[x, map.GetLength(1) - 1 - y];
                }
            }
            //Print(newMap);
        }
        else
        {
            //Cut x 
            int mid = (map.GetLength(0) / 2);
            if (fold.position <= mid)
            {
                newMap = new char[map.GetLength(0) - (fold.position + 1), map.GetLength(1)];
            }
            else
            {
                newMap = new char[fold.position, map.GetLength(1)];
            }
            //Lets assume perfect folds for now
            for (int y = 0; y < newMap.GetLength(1); y++)
            {
                for (int x = 0; x < fold.position; x++)
                {
                    newMap[x, y] = map[x, y];
                }
                for (int x = 0; x < mid; x++)
                {
                    newMap[x, y] = newMap[x, y] == '#' ? '#' : map[map.GetLength(0) - 1 - x, y];
                }
            }
            //Print(newMap);
        }
        map = newMap;
    }
}
stopWatch.Stop();
Print(map);
Console.WriteLine($"Result:  - Elapsed {stopWatch.Elapsed} ");

void Print(char[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        string line = string.Empty;
        for (int x = 0; x < map.GetLength(0); x++)
        {
            line += map[x, y];
        }
        Console.WriteLine(line);
    }
    Console.WriteLine($"Width {map.GetLength(0)} Height {map.GetLength(1)}");
}