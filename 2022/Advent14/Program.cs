//Part1("input.txt");
Part2("input.txt");

void Part1(string selectedInput)
{
    Console.WriteLine("----Part 1----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var segments = new List<Segment>();
    foreach (var line in lines)
    {
        segments.AddRange(Segment.Parse(line));
    }
    
    var sandOrigin = new Point(500, 0);
    var topo = new Topography(segments, sandOrigin);
    topo.DrawRock(segments);

    topo.Dump();
    Console.ReadKey(true);

    var disappeared = false;
    var settledCount = 0;
    do
    {
        var sand = new Sand(topo.SandOrigin);

        do
        {
            sand.Drop1(topo);
            if (sand.State == SandState.Settled)
            {
                settledCount++;
            }
        } while (sand.State != SandState.Settled && sand.State != SandState.Disappeared);
        if (sand.State == SandState.Settled)
        {
            if (settledCount % 100 == 0)
            {
                topo.Dump();
                Console.ReadKey(true);
            }
        }
        if (sand.State == SandState.Disappeared)
        {
            disappeared = true;
        }
    } while (!disappeared);

    topo.Dump();
    Console.WriteLine("----Part 1 Answer----");
    Console.WriteLine(settledCount);
}

void Part2(string selectedInput)
{
    Console.WriteLine("----Part 2----");
    var lines = File.ReadAllLines(selectedInput).ToList();

    var segments = new List<Segment>();
    foreach (var line in lines)
    {
        segments.AddRange(Segment.Parse(line));
    }

    var sandOrigin = new Point(500, 0);
    var topo = new Topography(segments, sandOrigin);
    topo.DrawRock(segments);
    topo.DrawRock(new List<Segment>{new Segment(0, topo.Height - 1, topo.Width -1, topo.Height -1)});

    topo.Dump();
    Console.ReadKey(true);

    var blocked = false;
    var settledCount = 0;
    do
    {
        var sand = new Sand(topo.SandOrigin);

        do
        {
            sand.Drop1(topo);
            if (sand.State == SandState.Settled || sand.State == SandState.Blocked)
            {
                settledCount++;
            }
        } while (sand.State != SandState.Settled && sand.State != SandState.Disappeared && sand.State != SandState.Blocked);
        if (sand.State == SandState.Settled)
        {
            if (settledCount % 1000 == 0)
            {
                topo.Dump();
                Console.ReadKey(true);
            }
        }
        if (sand.State == SandState.Blocked)
        {
            blocked = true;
        }
    } while (!blocked);

    topo.Dump();


    Console.WriteLine("----Part 2 Answer----");
    Console.WriteLine(settledCount);
}



