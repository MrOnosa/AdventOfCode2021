using System.Diagnostics;

Console.WriteLine("Hello World!");
List<List<DumboOctopus>> dumbos = new List<List<DumboOctopus>>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    List<int> inputLine = new List<int>(line.Length);
    foreach (var c in line)
    {
        inputLine.Add(int.Parse(c.ToString()));
    }
    dumbos.Add(inputLine.Select(e => new DumboOctopus { Energy = e }).ToList());
}

        Print(dumbos);
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

int totalFlashes = 0;
for (int step = 0; step < 100; step++)
{
    //First, the energy level of each octopus increases by 1.
    for (int col = 0; col < 10; col++)
        for (int row = 0; row < 10; row++)
        {
            dumbos[row][col].Energy++;
        }
    Console.WriteLine("After energy "+(step +1));
    Print(dumbos);
    /* Then, any octopus with an energy level greater than 9 flashes. 
    This increases the energy level of all adjacent octopuses by 1, 
    including octopuses that are diagonally adjacent. 
    If this causes an octopus to have an energy level greater than 9, it also flashes. 
    This process continues as long as new octopuses keep having their energy level increased beyond 9.
    (An octopus can only flash at most once per step.)*/
    do
    {
        for (int col = 0; col < 10; col++)
            for (int row = 0; row < 10; row++)
            {
                if (dumbos[row][col].Energy > 9 && !dumbos[row][col].Flashes)
                {
                    totalFlashes++;
                    dumbos[row][col].Flashes = true;
                    for (int acol = col - 1; acol <= col + 1; acol++)
                    {
                        for (int arow = row - 1; arow <= row + 1; arow++)
                        {
                            if (arow >= 0 && arow < 10 && acol >= 0 && acol < 10
                                && (arow != row || acol != col))
                            {
                                dumbos[arow][acol].Energy++;
                            }
                        }
                    }
                }
            }
            
        Console.WriteLine("Flashing substep "+(step +1)+" Total falshes so far "+totalFlashes);
        Print(dumbos);
    } while (dumbos.Any(r => r.Any(c => c.Energy > 9 && !c.Flashes)));
    /* Finally, any octopus that flashed during this step has its energy level set to 0, 
    as it used all of its energy to flash. */
    for (int col = 0; col < 10; col++)
        for (int row = 0; row < 10; row++)
        {
            if(dumbos[row][col].Flashes)
                dumbos[row][col].Energy = 0;
            dumbos[row][col].Flashes = false;
        }
    Console.WriteLine("After step "+(step +1));
    Print(dumbos);
}


stopWatch.Stop();

Console.WriteLine($"Result: {totalFlashes} - Elapsed {stopWatch.Elapsed} ");

void Print(List<List<DumboOctopus>> dumbos){
    for (int row = 0; row < 10; row++)
    {
        Console.WriteLine($"{dumbos[row][0].Energy,2}{dumbos[row][1].Energy,2}{dumbos[row][2].Energy,2}{dumbos[row][3].Energy,2}{dumbos[row][4].Energy,2}{dumbos[row][5].Energy,2}{dumbos[row][6].Energy,2}{dumbos[row][7].Energy,2}{dumbos[row][8].Energy,2}{dumbos[row][9].Energy,2}");
    }
}

class DumboOctopus
{
    public int Energy { get; set; }
    public bool Flashes { get; set; }
}