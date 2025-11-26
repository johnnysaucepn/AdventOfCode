//Part1("example.txt");
Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    // Crude, might not perform well
    List<Coord> visited = new List<Coord>();

    var head = new Coord(0,0);
    var tail = new Coord(0,0);

    foreach (var line in lines)
    {
        Console.WriteLine($"Head is {head.X},{head.Y}");
        Console.WriteLine($"Tail is {tail.X},{tail.Y}");
        var move = ParseMove(line);
        Console.WriteLine($"Move {move.Delta.X},{move.Delta.Y} x {move.Count}");
        for (var i=0; i<move.Count; i++)
        {
            head = new Coord(head.X + move.Delta.X, head.Y + move.Delta.Y);
            Console.WriteLine($"Head is now {head.X},{head.Y}");
            tail = MoveKnot(head, tail);
            Console.WriteLine($"Tail is now {tail.X},{tail.Y}");
            Visit(visited, tail);
        }
    }

    DumpVisited(visited);

    Console.WriteLine("----Part 1 Answer----");

    Console.WriteLine(visited.Count);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    // Crude, might not perform well
    List<Coord> visited = new List<Coord>();

    var head = new Coord(0,0);
    var knot1 = new Coord(0,0);
    var knot2 = new Coord(0,0);
    var knot3 = new Coord(0,0);
    var knot4 = new Coord(0,0);
    var knot5 = new Coord(0,0);
    var knot6 = new Coord(0,0);
    var knot7 = new Coord(0,0);
    var knot8 = new Coord(0,0);
    var tail = new Coord(0,0);

    foreach (var line in lines)
    {
        var move = ParseMove(line);

        for (var i=0; i < move.Count; i++)
        {
            head = new Coord(head.X + move.Delta.X, head.Y + move.Delta.Y);
            
            knot1 = MoveKnot(head, knot1);
            knot2 = MoveKnot(knot1, knot2);
            knot3 = MoveKnot(knot2, knot3);
            knot4 = MoveKnot(knot3, knot4);
            knot5 = MoveKnot(knot4, knot5);
            knot6 = MoveKnot(knot5, knot6);
            knot7 = MoveKnot(knot6, knot7);
            knot8 = MoveKnot(knot7, knot8);

            tail = MoveKnot(knot8, tail);

            Visit(visited, tail);
        }
    }

    DumpVisited(visited);

    Console.WriteLine("----Part 2 Answer----");

    Console.WriteLine(visited.Count);
}

Move ParseMove(string line)
{
    var parts = line.Split(" ");
    var delta = parts[0] switch {
        "U" => new Delta(0,-1),
        "D" => new Delta(0,1),
        "L" => new Delta(-1,0),
        "R" => new Delta(1,0)
    };
    var count = Convert.ToInt32(parts[1]);

    return new Move(delta, count);
}

Coord MoveKnot(Coord parent, Coord currentKnot)
{
    var vertDiff = parent.Y - currentKnot.Y;
    var horizDiff = parent.X - currentKnot.X;

    // Don't move the knot
    if (Math.Abs(vertDiff) < 2 && Math.Abs(horizDiff) < 2) return currentKnot;

    var deltaX = horizDiff switch 
    {
        < 0 => -1,
        > 0 => 1,
        _ => 0
    };

    var deltaY = vertDiff switch 
    {
        < 0 => -1,
        > 0 => 1,
        _ => 0
    };

    return new Coord(currentKnot.X + deltaX, currentKnot.Y + deltaY);
}

void Visit(List<Coord> visited, Coord tail)
{
    // Crude, but might work
    var alreadyVisited = visited.Any(p => p.X == tail.X && p.Y == tail.Y);
    if (!alreadyVisited)
    {
        visited.Add(tail);
    }
}

void DumpVisited(List<Coord> visited)
{
    var minX = visited.Select(p => p.X).Min();
    var minY = visited.Select(p => p.Y).Min();
    var maxX = visited.Select(p => p.X).Max();
    var maxY = visited.Select(p => p.Y).Max();

    var width = maxX - minX + 1;
    var height = maxY - minY + 1;
    var matrix = new bool[width, height];

    foreach (var pos in visited)
    {
        matrix[pos.X - minX, pos.Y - minY] = true;
    }

    for (var y = 0; y < height; y++)
    {
        for (var x = 0; x < width; x++)
        {
            Console.Write(matrix[x,y] ? "#" : ".");
        }
        Console.WriteLine();
    }
}

struct Move
{
    public Delta Delta;
    public int Count;
    public Move(Delta delta, int count)
    {
        Delta = delta;
        Count = count;
    }
}

struct Delta
{
    public int X;
    public int Y;
    public Delta(int x, int y)
    {
        X = x;
        Y = y;
    }
}

struct Coord
{
    public int X;
    public int Y;
    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }
}
