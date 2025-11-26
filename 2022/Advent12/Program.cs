//Part1("input.txt");
Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var topo = new Topography(lines);
    var graph = new SearchGraph(topo, SearchGraph.CloseEnoughUp);
    var path = graph.Traverse(graph.Start, graph.End);
    var solution = graph.Backtrack(path).ToList();
    foreach (var node in solution)
    {
        Console.WriteLine($"[{node.X}, {node.Y}] = {node.Elevation}");
    }
    graph.Dump(solution);

    Console.WriteLine("----Part 1 Answer----");
    Console.WriteLine(solution.Count);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var topo = new Topography(lines);
    var graph = new SearchGraph(topo, SearchGraph.CloseEnoughDown);
    var path = graph.TraverseToLowest(graph.End);
    var solution = graph.Backtrack(path).ToList();
        graph.Dump(solution);

    Console.WriteLine("----Part 2 Answer----");
    Console.WriteLine(solution.Count);
}

