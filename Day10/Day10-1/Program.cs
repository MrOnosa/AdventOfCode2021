using System.Diagnostics;

Console.WriteLine("Hello World!");
var lines = new List<string>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    lines.Add(line);
}
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

const string OpenSymbols = "([{<";
const string CloseSymbols = ")]}>";
int points = 0;

foreach (var line in lines)
{
    var chunk = new Chunk();
    Chunk? chunkPointer = chunk;
    bool errorDetected = false;
    for (int i = 0; i < line.Length && !errorDetected; i++)
    {
        char symbol = line[i];
        if (OpenSymbols.Contains(symbol))
        {
            var child = new Chunk() { Symbol = symbol, Parent = chunkPointer };
            chunkPointer?.Children.Add(child);
            chunkPointer = child;
        } 
        else if(symbol == ')')
        {
            if(chunkPointer?.Symbol == '('){
                chunkPointer = chunkPointer.Parent;
            } else {
                Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                points += 3;
                errorDetected = true;
            }
        }
        else if(symbol == ']')
        {
            if(chunkPointer?.Symbol == '['){
                chunkPointer = chunkPointer.Parent;
            } else {
                Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                points += 57;
                errorDetected = true;
            }
        }
        else if(symbol == '}')
        {
            if(chunkPointer?.Symbol == '{'){
                chunkPointer = chunkPointer.Parent;
            } else {
                Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                points += 1197;
                errorDetected = true;
            }
        }
        else if(symbol == '>')
        {
            if(chunkPointer?.Symbol == '<'){
                chunkPointer = chunkPointer.Parent;
            } else {
                Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                points += 25137;
                errorDetected = true;
            }
        }
    }
}

stopWatch.Stop();

Console.WriteLine($"Result: {points}- Elapsed {stopWatch.Elapsed} ");

class Chunk
{
    public char Symbol { get; set; }
    public List<Chunk> Children { get; set; } = new List<Chunk>();
    public Chunk? Parent { get; set; } = null;
}