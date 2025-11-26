//Part1();
Part2();

void Part1()
{
    var lines = File.ReadAllLines("input.txt");

    var total = 0;

    foreach (var rucksack in lines)
    {
        var mid = rucksack.Length / 2;
        var first = rucksack[..mid];
        var second = rucksack[mid..];
        Console.WriteLine(first);
        Console.WriteLine(second);

        var common = first.Intersect(second).First();
        var priority = GetPriority(common);
        Console.WriteLine($"{common}={priority}");

        total += priority;
    }

Console.WriteLine(total);

}

void Part2()
{
    var lines = File.ReadAllLines("input.txt");
    
    var total = 0;

    var iter = lines.GetEnumerator();
    

    foreach (var group in SplitByLength(lines, 3))
    {
        var first = group[0];
        var second = group[1];
        var third = group[2];
        Console.WriteLine(first);
        Console.WriteLine(second);
        Console.WriteLine(third);

        var common = first.Intersect(second).Intersect(third).First();
        var priority = GetPriority(common);
        Console.WriteLine($"{common}={priority}");

        total += priority;
    }

Console.WriteLine(total);

}


static List<List<T>> SplitByLength<T>(IList<T> source, int length)
{
    return  source
        .Select((x, i) => new { Index = i, Value = x })
        .GroupBy(x => x.Index / length)
        .Select(x => x.Select(v => v.Value).ToList())
        .ToList();
}

int GetPriority(char common)
{
    if (common < 'a')
    {
        return common - 'A' + 27;
    }
    else
    {
        return common - 'a' + 1;
    }
}