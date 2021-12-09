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
List<int> lowPoints = new List<int>();
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
for (int col = 0; col < cols; col++)
    for (int row = 0; row < rows; row++)
    {
        Console.WriteLine($"Row {row} Col {col} value {input[row][col]}");
        if((row == 0 || input[row][col] < input[row-1][col]) 
        && (row + 1 == rows || input[row][col] < input[row+1][col])
        && (col == 0 || input[row][col] < input[row][col-1]) 
        && (col + 1 == cols || input[row][col] < input[row][col+1]))
        {
            lowPoints.Add(input[row][col]);
        }
    }

stopWatch.Stop();

Console.WriteLine($"Result: {lowPoints.Select(l => l + 1).Sum()}- Elapsed {stopWatch.Elapsed} ");
