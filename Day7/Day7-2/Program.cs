using System.Diagnostics;

Console.WriteLine("Hello World!");
List<int> crabs = new List<int>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    foreach (var number in line.Split(','))
    {
        crabs.Add(int.Parse(number));
    }
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
int bestPosition = -1;
int bestFuelCost = int.MaxValue;
int minHorizontalPosition = crabs.Min();
int maxHorizontalPosition = crabs.Max();
for (int i = minHorizontalPosition; i <= maxHorizontalPosition; i++)
{
    int fuelCost = 0;
    foreach (var crab in crabs)
    {
        if (crab < i)
        {
            for (int f = 0; f < i - crab; f++)
            {
                fuelCost += f + 1;
            }
        }
        else
        {
            for (int f = 0; f < crab - i; f++)
            {
                fuelCost += f + 1;
            }
        }
    }
    //Console.WriteLine($"Position {i} - Cost {fuelCost} - Elapsed {stopWatch.Elapsed} ");
    if (fuelCost < bestFuelCost)
    {
        bestFuelCost = fuelCost;
        bestPosition = i;
    }
}
stopWatch.Stop();

Console.WriteLine($"Total fuel cost: {bestFuelCost} at position {bestPosition} - Elapsed {stopWatch.Elapsed} ");
