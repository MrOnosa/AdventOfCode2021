using System.Diagnostics;

Console.WriteLine("Hello World!");
var caves = new List<Cave>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    var beginning = line.Split("-")[0];
    var end = line.Split("-")[1];
    var startCave = caves.FirstOrDefault(c => c.Name == beginning);
    if (startCave == null)
    {
        startCave = new Cave { Name = beginning, IsBig = beginning.All(c => char.IsUpper(c)) };
        caves.Add(startCave);
    }
    var endCave = caves.FirstOrDefault(c => c.Name == end);
    if (endCave == null)
    {
        endCave = new Cave { Name = end, IsBig = end.All(c => char.IsUpper(c)) };
        caves.Add(endCave);
    }
    startCave.Path.Add(endCave);
    endCave.Path.Add(startCave);
}

foreach (var cave in caves)
{
    Console.WriteLine($"Cave {cave.Name} connects to {string.Join(",", cave.Path.Select(c => c.Name))}. It {(cave.IsBig ? "is" : "is not")} big.");
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

var validPaths = new List<List<string>>();
var paths = new List<List<string>>();

var start = caves.First(c => c.Name == "start");
foreach (var path in start.Path)
{
    var pathTaken = new List<string>();
    pathTaken.Add(start.Name);
    paths.AddRange(Explore(path, pathTaken));
    foreach (var p in paths)
    {
        if (p.Last() == "end")
        {
            var duplicate = false;
            foreach (var v in validPaths)
            {
                if (v.Count() == p.Count())
                {
                    var same = true;
                    for (int i = 0; i < v.Count(); i++)
                    {
                        same &= v[i] == p[i];
                    }
                    if (same)
                    {
                        duplicate = true;
                        break;
                    }
                }
            }
            if (duplicate)
            {
                //Console.WriteLine($"Duplicate path: {string.Join(",", p)}");
            }
            else
            {
                //Console.WriteLine($"    Valid path: {string.Join(",", p)}");
                validPaths.Add(p);
            }
        }
        else
        {
            //Console.WriteLine(    $"  Invalid path: {string.Join(",", p)}");
        }
    }
}


stopWatch.Stop();

Console.WriteLine($"Result: {validPaths.Count()} - Elapsed {stopWatch.Elapsed} ");

List<List<string>> Explore(Cave cave, List<string> pathTaken)
{
    var branches = new List<List<string>>();
    var newPath = new List<string>(pathTaken);
    newPath.Add(cave.Name);
    branches.Add(newPath);
    foreach (var path in cave.Path)
    {
        // if(path.Name == "end")
        // {
        //     pathTaken.Add(path.Name);
        //     return pathTaken;
        // }
        if (path.IsBig || !newPath.Contains(path.Name))
        {
            branches.AddRange(Explore(path, newPath));
        }
    }
    return branches;
}

class Cave
{
    public string Name { get; set; } = string.Empty;
    public bool IsBig { get; set; }

    public List<Cave> Path { get; set; } = new List<Cave>();
}