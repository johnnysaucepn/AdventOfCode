using System.Text.RegularExpressions;

Part1();
Part2();

void Part1()
{
    var lines = File.ReadAllLines("input.txt");

    var rows = ReadStackRows(lines);
    var stacks = ParseStacks(rows);
    Console.WriteLine(DumpStacks(stacks));

    foreach (var (count, from, to) in ReadInstructions(lines))
    {
        Move(stacks, count, from, to);
    }

    Console.WriteLine(DumpStacks(stacks));
}

void Part2()
{
    var lines = File.ReadAllLines("input.txt");

    var rows = ReadStackRows(lines);
    var stacks = ParseStacks(rows);
    Console.WriteLine(DumpStacks(stacks));

    foreach (var (count, from, to) in ReadInstructions(lines))
    {
        MoveGroup(stacks, count, from, to);
    }

    Console.WriteLine(DumpStacks(stacks));
}

string DumpStacks(List<Stack<char>> stacks)
{
    return new string(stacks.Select(x=>x.Peek()).ToArray());
}


void Move(List<Stack<char>> stacks, int count, int start, int end)
{
    for (var c=0; c<count; c++)
    {
        var crate = stacks[start-1].Pop();
        stacks[end-1].Push(crate);
    }
}

void MoveGroup(List<Stack<char>> stacks, int count, int start, int end)
{
    // Don't know if there's a fancy way to do a multiple Pop(), do it by hand for now
    List<char> group = new List<char>();
    for (var c=0; c<count; c++)
    {
        var crate = stacks[start-1].Pop();
        group.Add(crate);
    }

    group.Reverse();

    foreach (var crate in group)
    {
        stacks[end-1].Push(crate);
    }

    
}


List<string> ReadStackRows(string[] lines)
{
    var reversedLines = new Stack<string>();

    foreach (var line in lines)
    {
        if (line.StartsWith(" 1 ")) break; //"  1   2   3   4   5   6   7   8   9 "
        reversedLines.Push(line);
    }
    return reversedLines.ToList();
}

IEnumerable<(int count, int from, int to)> ReadInstructions(string[] lines)
{
    var regex = new Regex("^move ([0-9]+) from ([0-9]+) to ([0-9]+)$");
    foreach (var line in lines)
    {
        var match = regex.Match(line);
        if (match.Success)
        {
            var count = Convert.ToInt32(match.Groups[1].Value);
            var from = Convert.ToInt32(match.Groups[2].Value);
            var to = Convert.ToInt32(match.Groups[3].Value);
            //Console.WriteLine($"{count} x {from} => {to}");
            yield return (count, from, to);
        }
    }
}

List<Stack<char>> ParseStacks(List<string> rows)
{
    //var stacks = Enumerable.Repeat(new Stack<char>(), 9).ToList();
    var stacks = Enumerable.Range(0,9).Select(x=> new Stack<char>()).ToList();

    foreach (var row in rows)
    {
        var chunks = SplitByLength(row, 4);
        for (var i=0; i<chunks.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(new string(chunks[i].ToArray()))) continue;
            var crate = chunks[i][1];
            stacks[i].Push(crate);
        }
    }
    return stacks;
}

List<List<T>> SplitByLength<T>(IEnumerable<T> source, int length)
{
    return  source
        .Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / length)
        .Select(x => x.Select(v => v.Value).ToList())
        .ToList();
}