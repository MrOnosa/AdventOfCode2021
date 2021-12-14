using System.Diagnostics;

Console.WriteLine("Hello World!");
List<Structure> structure = new List<Structure>();
List<(string rule, string element)> rules = new List<(string rule, string element)>();
foreach (string line /*Store text into string records*/ in System.IO.File.ReadLines(@".\..\puzzle-input.txt"))
{
    Console.WriteLine($"Input: {line}");
    if (!string.IsNullOrEmpty(line))
    {
        if (line.Contains(">"))
        {
            (string rule, string element) rule = (rule: new string(line.Take(2).ToArray()), element: line.Last().ToString());
            rules.Add(rule);
        }
        else
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                if (structure.Any(s => s.pair[0] == line[i] && s.pair[1] == line[i + 1]))
                {
                    var pair = structure.First(s => s.pair[0] == line[i] && s.pair[1] == line[i + 1]);
                    pair.count++;
                }
                else
                {
                    structure.Add(new Structure(pair: new string(new char[] { line[i], line[i + 1] }), count: 1));
                }
            }
            structure.Add(new Structure(pair: new string(new char[] { line.Last(), default }), count: 1));
        }
    }
}
LetterCount(structure);
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
for (int i = 0; i < 40; i++)
{
    List<Structure> newStructure = structure.Select(s => new Structure(s.pair, s.count)).ToList();
    foreach (var rule in rules)
    {
        if (structure.Any(s => s.pair[0] == rule.rule[0] && s.pair[1] == rule.rule[1]))
        {
            string tripplet = new string(new char[] { rule.rule[0], rule.element[0], rule.rule[1] });

            var broken = structure.First(s => s.pair[0] == rule.rule[0] && s.pair[1] == rule.rule[1]);
            var newBroken = newStructure.First(s => s.pair[0] == rule.rule[0] && s.pair[1] == rule.rule[1]);
            newBroken.count = newBroken.count - broken.count;

            if (newStructure.Any(s => s.pair[0] == tripplet[0] && s.pair[1] == tripplet[1]))
            {
                var pair = newStructure.First(s => s.pair[0] == tripplet[0] && s.pair[1] == tripplet[1]);
                pair.count += broken.count;
            }
            else
            {
                newStructure.Add(new Structure(pair: new string(new char[] { tripplet[0], tripplet[1] }), count: broken.count));
            }

            if (newStructure.Any(s => s.pair[0] == tripplet[1] && s.pair[1] == tripplet[2]))
            {
                var pair = newStructure.First(s => s.pair[0] == tripplet[1] && s.pair[1] == tripplet[2]);
                pair.count += broken.count;
            }
            else
            {
                newStructure.Add(new Structure(pair: new string(new char[] { tripplet[1], tripplet[2] }), count: broken.count));
            }
        }
    }
    structure = newStructure.Select(s => new Structure(s.pair, s.count)).ToList();
    LetterCount(structure);
}



stopWatch.Stop();

Console.WriteLine($"Result: - Elapsed {stopWatch.Elapsed} ");

void LetterCount(List<Structure> structure)
{
    List<Letter> letters = new List<Letter>();
    foreach (var item in structure)
    {
        if (letters.Any(l => l.letter == item.pair[0]))
        {
            var letter = letters.First(l => l.letter == item.pair[0]);
            letter.count += item.count;
        }
        else
        {
            letters.Add(new Letter { letter = item.pair[0], count = item.count });
        }
    }
    letters = letters.OrderBy(l => l.count).ToList();
    foreach (var letter in letters)
    {
        Console.WriteLine($"{letter.letter}: - {letter.count} ");
    }
    Console.WriteLine($"Difference: {letters.Last().count - letters.First().count}");
}

class Structure
{
    public Structure()
    {
        
    }
    public Structure(string pair, long count)
    {
        this.pair = pair;
        this.count = count;
    }
    public string pair { get; set; }
    public long count { get; set; }
}
class Letter
{
    public char letter { get; set; }
    public long count { get; set; }
}