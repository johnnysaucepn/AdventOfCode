//Part1();
Part2();

void Part1()
{
    var lines = File.ReadAllLines("input.txt");

    var count = 0;
    foreach (var pair in lines)
    {
        var (firstSet, secondSet) = GetElfAssignments(pair);

        var hasSubSet = IsSuperset(firstSet, secondSet) || IsSuperset(secondSet, firstSet);

        Console.WriteLine($"{pair} = {hasSubSet}");
        if (hasSubSet) count++;
    }

    Console.WriteLine(count);
}

void Part2()
{
    var lines = File.ReadAllLines("input.txt");

    var count = 0;
    foreach (var pair in lines)
    {
        var (firstSet, secondSet) = GetElfAssignments(pair);

        var hasOverlap = HasOverlap(firstSet, secondSet);

        Console.WriteLine($"{pair} = {hasOverlap}");
        if (hasOverlap) count++;
    }

    Console.WriteLine(count);
}


bool IsSuperset(IList<int> x, IList<int> y)
{
    return x.Intersect(y).Count() == x.Count();
}

bool HasOverlap(IList<int> x, IList<int> y)
{
    return x.Intersect(y).Count() > 0;
}

List<int> GetRange(string rangeExpression)
{
    var fromTo = rangeExpression.Split("-");
    var from = Convert.ToInt32(fromTo[0]);
    var to = Convert.ToInt32(fromTo[1]);
    var range = Enumerable.Range(from, to-from+1).ToList();
    
    //Console.WriteLine($"{rangeExpression}={string.Join(",", range)}");
    return range;
}

(List<int> firstSet, List<int> secondSet) GetElfAssignments(string pair)
{
    var single = pair.Split(",");
    var first = single[0];
    var second = single[1];

    var firstSet = GetRange(first);
    var secondSet = GetRange(second);

    return (firstSet, secondSet);
}