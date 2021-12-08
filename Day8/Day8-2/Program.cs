using System.Diagnostics;
using System.Linq;

Console.WriteLine("Hello World!");
var displays = new[]{
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
int[] d = new int[] { 2, 4, 3, 7 };
var puzzels = new List<SignalPatternPuzzle>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    var puzzel = new SignalPatternPuzzle();
    Console.WriteLine($"Input: {line}");
    string uniqueSignalPatterns = line.Split('|')[0];
    string fourDigitOutputValue = line.Split('|')[1];

    puzzel.UniqueSignalPatterns = uniqueSignalPatterns.Split(' ').Select(s => s.Trim()).ToList();
    puzzel.OutputValues = fourDigitOutputValue.Split(' ').Select(s => s.Trim()).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

    puzzels.Add(puzzel);
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
int count = 0;
int runningSum = 0;
foreach (var puzzel in puzzels)
{
    //puzzel.PrintPossibilities();
    //puzzel.PrintDisplay();
    //One
    string one = puzzel.UniqueSignalPatterns.First(p => p.Length == 2);
    puzzel.Possibilities[0] = puzzel.Possibilities[0].Where(p => p != one[0] && p != one[1]).ToList();
    puzzel.Possibilities[1] = puzzel.Possibilities[1].Where(p => p != one[0] && p != one[1]).ToList();
    puzzel.Possibilities[2] = puzzel.Possibilities[2].Where(p => p == one[0] || p == one[1]).ToList();
    puzzel.Possibilities[3] = puzzel.Possibilities[3].Where(p => p != one[0] && p != one[1]).ToList();
    puzzel.Possibilities[4] = puzzel.Possibilities[4].Where(p => p != one[0] && p != one[1]).ToList();
    puzzel.Possibilities[5] = puzzel.Possibilities[5].Where(p => p == one[0] || p == one[1]).ToList();
    puzzel.Possibilities[6] = puzzel.Possibilities[6].Where(p => p != one[0] && p != one[1]).ToList();
    puzzel.DeriveSegments();
    puzzel.PrintPossibilities();
    puzzel.PrintDisplay();
    //Seven    
    string seven = puzzel.UniqueSignalPatterns.First(p => p.Length == 3);
    puzzel.Possibilities[0] = puzzel.Possibilities[0].Where(p => p == seven[0] || p == seven[1] || p == seven[2]).ToList();
    puzzel.Possibilities[1] = puzzel.Possibilities[1].Where(p => p != seven[0] && p != seven[1] && p != seven[2]).ToList();
    puzzel.Possibilities[2] = puzzel.Possibilities[2].Where(p => p == seven[0] || p == seven[1] || p == seven[2]).ToList();
    puzzel.Possibilities[3] = puzzel.Possibilities[3].Where(p => p != seven[0] && p != seven[1] && p != seven[2]).ToList();
    puzzel.Possibilities[4] = puzzel.Possibilities[4].Where(p => p != seven[0] && p != seven[1] && p != seven[2]).ToList();
    puzzel.Possibilities[5] = puzzel.Possibilities[5].Where(p => p == seven[0] || p == seven[1] || p == seven[2]).ToList();
    puzzel.Possibilities[6] = puzzel.Possibilities[6].Where(p => p != seven[0] && p != seven[1] && p != seven[2]).ToList();
    puzzel.DeriveSegments();
    puzzel.PrintPossibilities();
    puzzel.PrintDisplay();
    //Four    
    string four = puzzel.UniqueSignalPatterns.First(p => p.Length == 4);
    Func<char, bool> exclude = p => p != four[0] && p != four[1] && p != four[2] && p != four[3];
    Func<char, bool> include = p => p == four[0] || p == four[1] || p == four[2] || p == four[3];
    puzzel.Possibilities[0] = puzzel.Possibilities[0].Where(exclude).ToList();
    puzzel.Possibilities[1] = puzzel.Possibilities[1].Where(include).ToList();
    puzzel.Possibilities[2] = puzzel.Possibilities[2].Where(include).ToList();
    puzzel.Possibilities[3] = puzzel.Possibilities[3].Where(include).ToList();
    puzzel.Possibilities[4] = puzzel.Possibilities[4].Where(exclude).ToList();
    puzzel.Possibilities[5] = puzzel.Possibilities[5].Where(include).ToList();
    puzzel.Possibilities[6] = puzzel.Possibilities[6].Where(exclude).ToList();
    puzzel.DeriveSegments();
    puzzel.PrintPossibilities();
    puzzel.PrintDisplay();
    //Two, Three, Five    
    // string two;
    // string three;
    // string five;
    List<string> twoThreeFive = puzzel.UniqueSignalPatterns.Where(p => p.Length == 5).ToList();
    List<string> zeroSixNine = puzzel.UniqueSignalPatterns.Where(p => p.Length == 6).ToList();
    //Two and Three have 4 letters in common
    //Two and Five have 3 letters in common
    //Three and five have 4 leters in common
    //Zero is missing segment 3, but 2,3,5,6 and 9 have segment 3
    //Nine
    //Nine has all the same letters as 4!
    string nine = zeroSixNine.Single(a => a.Contains(four[0]) && a.Contains(four[1]) && a.Contains(four[2]) && a.Contains(four[3]));
    Console.WriteLine($"Nine: {nine}");
    exclude = p => p != nine[0] && p != nine[1] && p != nine[2] && p != nine[3] && p != nine[4] && p != nine[5];
    include = p => p == nine[0] || p == nine[1] || p == nine[2] || p == nine[3] || p == nine[4] || p == nine[5];
    puzzel.Possibilities[0] = puzzel.Possibilities[0].Where(include).ToList();
    puzzel.Possibilities[1] = puzzel.Possibilities[1].Where(include).ToList();
    puzzel.Possibilities[2] = puzzel.Possibilities[2].Where(include).ToList();
    puzzel.Possibilities[3] = puzzel.Possibilities[3].Where(include).ToList();
    puzzel.Possibilities[4] = puzzel.Possibilities[4].Where(exclude).ToList();
    puzzel.Possibilities[5] = puzzel.Possibilities[5].Where(include).ToList();
    puzzel.Possibilities[6] = puzzel.Possibilities[6].Where(include).ToList();
    puzzel.DeriveSegments();
    puzzel.PrintPossibilities();
    puzzel.PrintDisplay();
    List<string> zeroSix = zeroSixNine.Where(p => p != nine).ToList(); ;
    //Zero
    //Zero has everything that 1 has
    string zero = zeroSix.Single(a => a.Contains(one[0]) && a.Contains(one[1]));
    Console.WriteLine($"Zero: {zero}");
    exclude = p => p != zero[0] && p != zero[1] && p != zero[2] && p != zero[3] && p != zero[4] && p != zero[5];
    include = p => p == zero[0] || p == zero[1] || p == zero[2] || p == zero[3] || p == zero[4] || p == zero[5];
    puzzel.Possibilities[0] = puzzel.Possibilities[0].Where(include).ToList();
    puzzel.Possibilities[1] = puzzel.Possibilities[1].Where(include).ToList();
    puzzel.Possibilities[2] = puzzel.Possibilities[2].Where(include).ToList();
    puzzel.Possibilities[3] = puzzel.Possibilities[3].Where(exclude).ToList();
    puzzel.Possibilities[4] = puzzel.Possibilities[4].Where(include).ToList();
    puzzel.Possibilities[5] = puzzel.Possibilities[5].Where(include).ToList();
    puzzel.Possibilities[6] = puzzel.Possibilities[6].Where(include).ToList();
    puzzel.DeriveSegments();
    puzzel.PrintPossibilities();
    puzzel.PrintDisplay();
    //Six
    string six = zeroSix.Single(a => a != zero);
    exclude = p => p != six[0] && p != six[1] && p != six[2] && p != six[3] && p != six[4] && p != six[5];
    include = p => p == six[0] || p == six[1] || p == six[2] || p == six[3] || p == six[4] || p == six[5];
    puzzel.Possibilities[0] = puzzel.Possibilities[0].Where(include).ToList();
    puzzel.Possibilities[1] = puzzel.Possibilities[1].Where(include).ToList();
    puzzel.Possibilities[2] = puzzel.Possibilities[2].Where(exclude).ToList();
    puzzel.Possibilities[3] = puzzel.Possibilities[3].Where(include).ToList();
    puzzel.Possibilities[4] = puzzel.Possibilities[4].Where(include).ToList();
    puzzel.Possibilities[5] = puzzel.Possibilities[5].Where(include).ToList();
    puzzel.Possibilities[6] = puzzel.Possibilities[6].Where(include).ToList();
    puzzel.DeriveSegments();
    puzzel.PrintPossibilities();
    puzzel.PrintDisplay();

    runningSum += puzzel.GetDisplayedValues().Sum();
    //Segment 1, could be 1 3 or 5
}
stopWatch.Stop();

Console.WriteLine($"Result: {runningSum} - Elapsed {stopWatch.Elapsed} ");

class SignalPatternPuzzle
{
    readonly List<char> LETTERS = new List<char>{
                'a',
                'b',
                'c',
                'd',
                'e',
                'f',
                'g'
            };
    public SignalPatternPuzzle()
    {
        Segments = new char[7];
        for (int i = 0; i < 7; i++)
        {
            Segments[i] = '?';
        }

        Possibilities = new List<List<char>>();
        for (int i = 0; i < 7; i++)
        {
            Possibilities.Add(LETTERS.ToList());
        }
    }
    public List<string> UniqueSignalPatterns { get; set; } = new List<string>();
    public List<string> OutputValues { get; set; } = new List<string>();

    public char[] Segments { get; set; }

    public List<List<char>> Possibilities { get; set; }

    public void DeriveSegments()
    {
        for (int i = 0; i < Segments.Length; i++)
        {
            if (Segments[i] == '?' && Possibilities[i].Count() == 1)
            {
                Segments[i] = Possibilities[i].First();
            }
        }
        if (Segments.Count(s => s == '?') == 1)
        {
            foreach (var letter in LETTERS)
            {
                if (!Segments.Contains(letter))
                {
                    for (int i = 0; i < Segments.Length; i++)
                    {
                        if (Segments[i] == '?')
                        {
                            Segments[i] = letter;
                        }
                    }
                    break;
                }
            }
        }
        for (int i = 0; i < Segments.Length; i++)
        {
            if (Segments[i] != '?' && Possibilities[i].Count() > 1)
            {
                Possibilities[i] = new List<char> { Segments[i] };
            }
        }
    }

    public void PrintPossibilities()
    {
        for (int i = 0; i < Possibilities.Count(); i++)
        {
            Console.WriteLine($"{i}: {(Possibilities[i].Contains('a') ? 'a' : '_')} {(Possibilities[i].Contains('b') ? 'b' : '_')} {(Possibilities[i].Contains('c') ? 'c' : '_')} {(Possibilities[i].Contains('d') ? 'd' : '_')} {(Possibilities[i].Contains('e') ? 'e' : '_')} {(Possibilities[i].Contains('f') ? 'f' : '_')} {(Possibilities[i].Contains('g') ? 'g' : '_')}");
        }
    }

    public void PrintDisplay()
    {
        var a = Segments[0];
        var b = Segments[1];
        var c = Segments[2];
        var d = Segments[3];
        var e = Segments[4];
        var f = Segments[5];
        var g = Segments[6];
        Console.WriteLine($" {a}{a}{a}{a} ");
        Console.WriteLine($"{b}    {c}");
        Console.WriteLine($"{b}    {c}");
        Console.WriteLine($" {d}{d}{d}{d} ");
        Console.WriteLine($"{e}    {f}");
        Console.WriteLine($"{e}    {f}");
        Console.WriteLine($" {g}{g}{g}{g} ");
    }

    public List<int> GetDisplayedValues()
    {
        if (Segments.Any(s => s == '?'))
        {
            PrintPossibilities();
            PrintDisplay();
            throw new Exception("Not ready to display");
        }
        List<int> values = new List<int>();
        List<string> showing = new List<string>();
        foreach (var output in OutputValues)
        {
            bool[] shown = new bool[7];
            for (int i = 0; i < 7; i++)
            {
                shown[i] = output.Contains(Segments[i]);
            }
            string showingDigit;
            if (shown[0] && shown[1] && shown[2] && !shown[3] && shown[4] && shown[5] && shown[6])
                showingDigit = "0";
            else if (!shown[0] && !shown[1] && shown[2] && !shown[3] && !shown[4] && shown[5] && !shown[6])
                showingDigit = "1";
            else if (shown[0] && !shown[1] && shown[2] && shown[3] && shown[4] && !shown[5] && shown[6])
                showingDigit = "2";
            else if (shown[0] && !shown[1] && shown[2] && shown[3] && !shown[4] && shown[5] && shown[6])
                showingDigit = "3";
            else if (!shown[0] && shown[1] && shown[2] && shown[3] && !shown[4] && shown[5] && !shown[6])
                showingDigit = "4";
            else if (shown[0] && shown[1] && !shown[2] && shown[3] && !shown[4] && shown[5] && shown[6])
                showingDigit = "5";
            else if (shown[0] && shown[1] && !shown[2] && shown[3] && shown[4] && shown[5] && shown[6])
                showingDigit = "6";
            else if (shown[0] && !shown[1] && shown[2] && !shown[3] && !shown[4] && shown[5] && !shown[6])
                showingDigit = "7";
            else if (shown[0] && shown[1] && shown[2] && shown[3] && shown[4] && shown[5] && shown[6])
                showingDigit = "8";
            else if (shown[0] && shown[1] && shown[2] && shown[3] && !shown[4] && shown[5] && shown[6])
                showingDigit = "9";
            else
            {
                PrintPossibilities();
                PrintDisplay();
                throw new Exception("Invalid display for "+output);
            }
            showing.Add(showingDigit);      
        }
        string fullDisplay = string.Join("",showing);
        Console.WriteLine($"Display: {fullDisplay}");   

        values.Add(int.Parse(fullDisplay));   
        return values;

    }
}