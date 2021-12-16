using System.Diagnostics;

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
//Get a baseline. Go directly across and then down.
int best = 0;
List<(int row, int col)> bestPath = new List<(int row, int col)>() { (0, 0) };
for (int col = 1; col < cols; col++)
{
    bestPath.Add((0, col));
    best += chitons[0][col];
}
for (int row = 1; row < rows; row++)
{
    bestPath.Add((row, cols - 1));
    best += chitons[row][cols - 1];
}

int current = 0;
List<(int row, int col)> path = new List<(int row, int col)>() { (0, 0) };
var exit = (row: rows - 1, col: cols - 1);
long checksSoFar = 0;
long outputsSoFar = 0;
Traverse(path, current);

stopWatch.Stop();

Console.WriteLine($"{outputsSoFar} * 100000000 + {checksSoFar} Best so far: {best}, Path; {string.Join("-",bestPath)}");
Console.WriteLine($"Result: {best} - Elapsed {stopWatch.Elapsed} ");

void Traverse(List<(int row, int col)> pathTaken, int risk)
{
    checksSoFar++;
    if(checksSoFar == 100000000)
    {
        checksSoFar = 0;
        outputsSoFar++;
        Console.WriteLine($"{outputsSoFar}: Best so far: {best}, Path; {string.Join("-",bestPath)} - Elapsed {stopWatch.Elapsed}");
    }
    var step = pathTaken.Last();

    if (step == exit)
    {
        //Made it to the end!
        if(risk < best){
            best = risk;
            bestPath = pathTaken;
        }
        Console.WriteLine($"{outputsSoFar} + {checksSoFar}: NEW Best: {best}, Path; {string.Join("-",bestPath)} - Elapsed {stopWatch.Elapsed}");
        return;
    }

    if(step.col % 2 == 0 ){
        var down = (row: step.row + 1, col: step.col);
        if (down.row < rows && !pathTaken.Contains(down))
        {
            int newRisk = risk + chitons[down.row][down.col];
            if (newRisk < best)
            {
                var downPath = pathTaken.ToList();
                downPath.Add(down);
                Traverse(downPath, newRisk);
            }
        }

        var right = (row: step.row , col: step.col + 1);
        if (right.col < cols && !pathTaken.Contains(right))
        {
            int newRisk = risk + chitons[right.row][right.col];
            if (newRisk < best)
            {
                var rightPath = pathTaken.ToList();
                rightPath.Add(right);
                Traverse(rightPath, newRisk);
            }
        }
    } else {

        var right = (row: step.row , col: step.col + 1);
        if (right.col < cols && !pathTaken.Contains(right))
        {
            int newRisk = risk + chitons[right.row][right.col];
            if (newRisk < best)
            {
                var rightPath = pathTaken.ToList();
                rightPath.Add(right);
                Traverse(rightPath, newRisk);
            }
        }

        var down = (row: step.row + 1, col: step.col);
        if (down.row < rows && !pathTaken.Contains(down))
        {
            int newRisk = risk + chitons[down.row][down.col];
            if (newRisk < best)
            {
                var downPath = pathTaken.ToList();
                downPath.Add(down);
                Traverse(downPath, newRisk);
            }
        }

    }

    var left = (row: step.row , col: step.col - 1);
    if (left.col >= 0 && !pathTaken.Contains(left))
    {
        int newRisk = risk + chitons[left.row][left.col];
        if (newRisk < best)
        {
            var leftPath = pathTaken.ToList();
            leftPath.Add(left);
            Traverse(leftPath, newRisk);
        }
    }

    var up = (row: step.row - 1, col: step.col);
    if (up.row >= 0 && !pathTaken.Contains(up))
    {
        int newRisk = risk + chitons[up.row][up.col];
        if (newRisk < best)
        {
            var upPath = pathTaken.ToList();
            upPath.Add(up);
            Traverse(upPath, newRisk);
        }
    }

    return;
}