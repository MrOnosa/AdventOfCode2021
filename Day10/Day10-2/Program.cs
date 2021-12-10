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

var incompleteChunks = new List<Chunk>();
foreach (var line in lines)
{
    Chunk? chunkPointer = null;
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
                chunkPointer.Complete = true;
                chunkPointer = chunkPointer.Parent;
            } else {
                //Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                //points += 3;
                errorDetected = true;
            }
        }
        else if(symbol == ']')
        {
            if(chunkPointer?.Symbol == '['){
                chunkPointer.Complete = true;
                chunkPointer = chunkPointer.Parent;
            } else {
                //Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                //points += 57;
                errorDetected = true;
            }
        }
        else if(symbol == '}')
        {
            if(chunkPointer?.Symbol == '{'){
                chunkPointer.Complete = true;
                chunkPointer = chunkPointer.Parent;
            } else {
                //Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                //points += 1197;
                errorDetected = true;
            }
        }
        else if(symbol == '>')
        {
            if(chunkPointer?.Symbol == '<'){
                chunkPointer.Complete = true;
                chunkPointer = chunkPointer.Parent;
            } else {
                //Console.WriteLine($"Syntax error - Expected {CloseSymbols[OpenSymbols.IndexOf(chunkPointer?.Symbol ?? default)]} but found {symbol}");
                //points += 25137;
                errorDetected = true;
            }
        }
    }
    if(errorDetected == false && chunkPointer != null){
        //Get parent
        while(chunkPointer.Parent != null){
            chunkPointer = chunkPointer.Parent;
        }
        incompleteChunks.Add(chunkPointer);
    }
}

List<(Chunk chunk, string autocompletion, long score)> results = new List<(Chunk chunk, string autocompletion, long score)>();
foreach (var chunk in incompleteChunks)
{
    var autocompletion = TraverseLite("", chunk);
    long score = 0;
    foreach (var symbol in autocompletion)
    {
        score *= 5;
        score += CloseSymbols.IndexOf(symbol) + 1;
    }
    Console.WriteLine($"Score {score}, Autocompletion: {autocompletion}");

    results.Add((chunk, autocompletion, score));
}

stopWatch.Stop();

var r = results.Select(r => r.score).OrderBy(r => r).ToList();
Console.WriteLine($"Result: {r[results.Count()/2]}- Elapsed {stopWatch.Elapsed} ");

string TraverseLite(string autocompletion, Chunk chunk){
    foreach (var child in chunk.Children)
    {
        autocompletion += TraverseLite(autocompletion, child);
    }
    if(chunk.Complete){
        
    } else {
        autocompletion += CloseSymbols[OpenSymbols.IndexOf(chunk.Symbol)];
    }
    return autocompletion;
}


class Chunk
{
    public char Symbol { get; set; }
    public bool Complete { get; set; }
    public List<Chunk> Children { get; set; } = new List<Chunk>();
    public Chunk? Parent { get; set; } = null;
}