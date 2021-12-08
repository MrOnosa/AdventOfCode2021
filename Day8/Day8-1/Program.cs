using System.Diagnostics;
using System.Linq;

Console.WriteLine("Hello World!");
var displays = new []{
    "abcefg", //0
    "cf",     //1
    "acdeg",  //2
    "acdfg",  //3
    "bcdf",   //4
    "abdfg",  //5
    "abdefg", //6
    "acf",    //7
    "abcdefg",//8
    "abcdfg"  //9
    };
/*
2 digits - 1
3 digits - 7
4 digits - 4
5 digits - 2,3,5
6 digits - 0,6,9
7 digits - 8
*/
int[] d = new int[]{2,4,3,7};
var puzzels = new List<SignalPatternPuzzle>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    var puzzel = new SignalPatternPuzzle();
    Console.WriteLine($"Input: {line}");
    string uniqueSignalPatterns = line.Split('|')[0];
    string fourDigitOutputValue = line.Split('|')[1];

    puzzel.UniqueSignalPatterns = uniqueSignalPatterns.Split(' ').Select(s => s.Trim()).ToList();
    puzzel.OutputValues = fourDigitOutputValue.Split(' ').Select(s => s.Trim()).ToList();

    puzzels.Add(puzzel);
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
int count = 0;
foreach (var puzzel in puzzels)
{
    count += puzzel.OutputValues.Where(p => d.Contains(p.Length)).Count();
}
stopWatch.Stop();

Console.WriteLine($"Result: {count} - Elapsed {stopWatch.Elapsed} ");

class SignalPatternPuzzle
{
    public List<string> UniqueSignalPatterns {get; set;}
    public List<string> OutputValues {get; set;}
}