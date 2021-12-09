using System.Diagnostics;

Console.WriteLine("Hello World!");
List<List<int>> input = new List<List<int>>();
int cols = 0;
int rows = 0;
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    List<int> inputLine = new List<int>(line.Length);
    foreach (var c in line)
    {
        inputLine.Add(int.Parse(c.ToString()));
    }
    input.Add(inputLine);
    cols = inputLine.Count();
    rows = input.Count();
}
List<(int row, int col)> lowPoints = new List<(int row, int col)>();
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
for (int col = 0; col < cols; col++)
    for (int row = 0; row < rows; row++)
    {
        if ((row == 0 || input[row][col] < input[row - 1][col])
        && (row + 1 == rows || input[row][col] < input[row + 1][col])
        && (col == 0 || input[row][col] < input[row][col - 1])
        && (col + 1 == cols || input[row][col] < input[row][col + 1]))
        {
            lowPoints.Add((row,col));
        }
    }

Console.WriteLine($"Low points are: {string.Join(",", lowPoints)}");

List<List<(int col, int row)>> basins = new List<List<(int col, int row)>>();
foreach (var lowPoint in lowPoints)
{
    var basin = TestBasin(lowPoint.row, lowPoint.col, new List<(int row, int col)>());
    Console.WriteLine($"Basin Length: {basin.Count()}. Basin points are: {string.Join(",", basin)}");
    basins.Add(basin);
}


stopWatch.Stop();

Console.WriteLine($"Result: {basins.OrderByDescending(b => b.Count()).Take(3).Aggregate(1, (acc, x) => acc * x.Count())} - Elapsed {stopWatch.Elapsed} ");

List<(int row, int col)> TestBasin(int row, int col, List<(int row, int col)> basin)
{
    basin.Add((row, col));
    if (!(row == 0 || input[row - 1][col] == 9 || basin.Contains((row - 1, col))))
        TestBasin(row - 1, col, basin);
    if (!(row + 1 == rows || input[row + 1][col] == 9 || basin.Contains((row + 1, col))))
        TestBasin(row + 1, col, basin);
    if (!(col == 0 || input[row][col - 1] == 9 || basin.Contains((row, col - 1))))
        TestBasin(row, col - 1, basin);
    if (!(col + 1 == cols || input[row][col + 1] == 9 || basin.Contains((row, col + 1))))
        TestBasin(row, col + 1, basin);
    return basin;
}