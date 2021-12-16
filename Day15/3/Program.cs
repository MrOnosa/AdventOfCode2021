using System.Diagnostics;
using System.Windows;

Console.WriteLine("Hello World!");
List<List<int>> chitons = new List<List<int>>();
int rows = 0, cols = 0;
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    List<int> inputLine = new List<int>(line.Length);
    foreach (var c in line)
    {
        inputLine.Add(int.Parse(c.ToString()));
    }
    cols = inputLine.Count();
    rows++;
    chitons.Add(inputLine);
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
int best = 793; //Found from another solution...
List<(int row, int col)> bestPath = new List<(int row, int col)>();

Console.WriteLine($"Result: {best} - Path {string.Join("-", bestPath)} ");

int current = 0;
List<(int row, int col)> path = new List<(int row, int col)>() { (0, 0) };
var exit = (row: rows - 1, col: cols - 1);
long checksSoFar = 0;
long outputsSoFar = 0;
Traverse(path, current);

stopWatch.Stop();

Console.WriteLine($"{outputsSoFar} * 100000000 + {checksSoFar} Best so far: {best}, Path; {string.Join("-", bestPath)}");
Console.WriteLine($"Result: {best} - Elapsed {stopWatch.Elapsed} ");

void Traverse(List<(int row, int col)> pathTaken, int risk)
{
    checksSoFar++;
    if (checksSoFar == 100000000)
    {
        checksSoFar = 0;
        outputsSoFar++;
        Console.WriteLine($"{outputsSoFar}: Best so far: {best} - Elapsed {stopWatch.Elapsed}");
    }
    var step = pathTaken.Last();

    if (step == exit)
    {
        //Made it to the end!
        if (risk < best)
        {
            best = risk;
            bestPath = pathTaken;
        }
        Console.WriteLine($"{outputsSoFar} + {checksSoFar}: NEW Best: {best}, Path; {string.Join("-", bestPath)} - Elapsed {stopWatch.Elapsed}");
        return;
    }

    var down = (row: step.row + 1, col: step.col);
    var right = (row: step.row, col: step.col + 1);
    var left = (row: step.row, col: step.col - 1);
    var up = (row: step.row - 1, col: step.col);

    List<(int risk, int minDistanceRisk, (int row, int col) nextStep)> order = new List<(int risk, int minDistanceRisk, (int row, int col) nextStep)>();

    if (down.row < rows)
    {
        int newRiskDown = risk + chitons[down.row][down.col];
        int minDistanceDown = newRiskDown + (cols - 1 - down.col) + (rows - 1 - down.row);
        order.Add((newRiskDown, minDistanceDown, down));
    }

    if (right.col < cols)
    {
        int newRiskRight = risk + chitons[right.row][right.col];
        int minDistanceRight = newRiskRight + (cols - 1 - right.col) + (rows - 1 - right.row);
        order.Add((newRiskRight, minDistanceRight, right));
    }

    if (left.col >= 0)
    {
        int newRiskLeft = risk + chitons[left.row][left.col];
        int minDistanceLeft = newRiskLeft + (cols - 1 - left.col) + (rows - 1 - left.row);
        order.Add((newRiskLeft, minDistanceLeft, left));
    }

    if (up.row >= 0)
    {
        int newRiskUp = risk + chitons[up.row][up.col];
        int minDistanceUp = newRiskUp + (cols - 1 - up.col) + (rows - 1 - up.row);
        order.Add((newRiskUp, minDistanceUp, up));
    }

    order = order.OrderBy(o => o.minDistanceRisk).ToList();

    foreach (var next in order)
    {
        if (next.minDistanceRisk < best && !pathTaken.Contains(next.nextStep))
        {
            var nextPath = pathTaken.ToList();
            nextPath.Add(next.nextStep);
            Traverse(nextPath, next.risk);
        }
    }

    return;
}